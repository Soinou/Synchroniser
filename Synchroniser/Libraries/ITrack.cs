using System;

namespace Synchroniser.Libraries
{
    /// <summary>
    /// Represents a music library track
    /// </summary>
    public interface ITrack : IEquatable<ITrack>
    {
        /// <summary>
        /// Track album
        /// </summary>
        string Album
        {
            get;
        }

        /// <summary>
        /// Track artist
        /// </summary>
        string Artist
        {
            get;
        }

        /// <summary>
        /// Track path
        /// </summary>
        string Path
        {
            get;
        }

        /// <summary>
        /// Track title
        /// </summary>
        string Title
        {
            get;
        }
    }
}
