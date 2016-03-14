using System;
using System.Collections.Generic;

namespace Synchroniser.Libraries
{
    /// <summary>
    /// Represents a music library playlist
    /// </summary>
    public interface IPlaylist : IEquatable<IPlaylist>
    {
        /// <summary>
        /// Playlist name
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Playlist tracks
        /// </summary>
        IEnumerable<ITrack> Tracks
        {
            get;
        }

        /// <summary>
        /// Adds a track to the playlist
        /// </summary>
        /// <param name="track">Track to add</param>
        void Add(ITrack track);

        /// <summary>
        /// Finds a track in the playlist
        /// </summary>
        /// <param name="track">Track to find</param>
        /// <returns>The track in the playlist</returns>
        ITrack Find(ITrack track);

        /// <summary>
        /// Removes a track from the playlist
        /// </summary>
        /// <param name="track">Track to remove</param>
        void Remove(ITrack track);
    }
}
