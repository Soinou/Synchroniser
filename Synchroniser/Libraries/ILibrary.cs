using System.Collections.Generic;

namespace Synchroniser.Libraries
{
    /// <summary>
    /// Represents a music library
    /// </summary>
    public interface ILibrary
    {
        /// <summary>
        /// Gets the list of playlists in the library
        /// </summary>
        IEnumerable<IPlaylist> Playlists
        {
            get;
        }

        /// <summary>
        /// Gets the list of tracks in the library
        /// </summary>
        IEnumerable<ITrack> Tracks
        {
            get;
        }

        /// <summary>
        /// Adds a playlist to the library
        /// </summary>
        /// <param name="playlist">Playlist to add</param>
        void Add(IPlaylist playlist);

        /// <summary>
        /// Adds a track to the library
        /// </summary>
        /// <param name="track">Track to add</param>
        void Add(ITrack track);

        /// <summary>
        /// Finds a playlist in the library
        /// </summary>
        /// <param name="playlist">Playlist to find</param>
        /// <returns>Playlist in the library</returns>
        IPlaylist Find(IPlaylist playlist);

        /// <summary>
        /// Finds a track in the library
        /// </summary>
        /// <param name="track">Track to find</param>
        /// <returns>Track in the library</returns>
        ITrack Find(ITrack track);

        /// <summary>
        /// Removes a playlist from the library
        /// </summary>
        /// <param name="playlist">Playlist to remove</param>
        void Remove(IPlaylist playlist);

        /// <summary>
        /// Removes a track from the library
        /// </summary>
        /// <param name="track">Track to remove</param>
        void Remove(ITrack track);
    }
}
