using System;

namespace Synchroniser.Libraries
{
    /// <summary>
    /// Different types of changes
    /// </summary>
    public enum ChangeTypes
    {
        kAddPlaylist,
        kRemovePlaylist,
        kAddTrack,
        kRemoveTrack,
        kAddTrackToPlaylist,
        kRemoveTrackFromPlaylist
    }

    /// <summary>
    /// Represents a synchronisation change
    /// </summary>
    public interface IChange
    {
        /// <summary>
        /// Type of the change
        /// </summary>
        ChangeTypes Type
        {
            get;
        }

        /// <summary>
        /// Runs the change
        /// </summary>
        /// <param name="library">Library on which to perform changes</param>
        void Run(ILibrary library);
    }

    /// <summary>
    /// Exception thrown when a change is not valid
    /// </summary>
    public class InvalidChangeException : Exception
    {
        /// <summary>
        /// Creates a new InvalidChangeException
        /// </summary>
        /// <param name="message">Message</param>
        public InvalidChangeException(string message)
            : base(message)
        { }
    }
}
