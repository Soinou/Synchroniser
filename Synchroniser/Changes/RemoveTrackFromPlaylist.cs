using Synchroniser.Libraries;

namespace Synchroniser.Changes
{
    /// <summary>
    /// Removes a track from a playlist
    /// </summary>
    internal class RemoveTrackFromPlaylist : IChange
    {
        /// <summary>
        /// Creates a new RemoveTrackFromPlaylist
        /// </summary>
        /// <param name="playlist">Playlist to remove from</param>
        /// <param name="track">Track to remove</param>
        public RemoveTrackFromPlaylist(IPlaylist playlist, ITrack track)
        {
            playlist_ = playlist;
            track_ = track;
        }

        /// <inheritdoc />
        public ChangeTypes Type
        {
            get
            {
                return ChangeTypes.kRemoveTrackFromPlaylist;
            }
        }

        /// <inheritdoc />
        public void Run(ILibrary library)
        {
            var track = library.Find(track_);
            var playlist = library.Find(playlist_);

            if (track == null)
            {
                throw new InvalidChangeException("Track '" + track_ + "' does not exist in library");
            }
            else if (playlist == null)
            {
                throw new InvalidChangeException("Playlist '" + playlist_ + "' does not exist in library");
            }
            else
            {
                playlist.Remove(track);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "<Remove> " + track_ + " <From> " + playlist_;
        }

        /// <summary>
        /// Playlist to remove from
        /// </summary>
        private IPlaylist playlist_;

        /// <summary>
        /// Track to remove
        /// </summary>
        private ITrack track_;
    }
}
