using Synchroniser.Libraries;

namespace Synchroniser.Changes
{
    /// <summary>
    /// Adds a track to a playlist
    /// </summary>
    internal class AddTrackToPlaylist : IChange
    {
        /// <summary>
        /// Creates a new AddTrackToPlaylist
        /// </summary>
        /// <param name="playlist">Playlist to add to</param>
        /// <param name="track">Track to add</param>
        public AddTrackToPlaylist(IPlaylist playlist, ITrack track)
        {
            playlist_ = playlist;
            track_ = track;
        }

        /// <inheritdoc />
        public ChangeTypes Type
        {
            get
            {
                return ChangeTypes.kAddTrackToPlaylist;
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
                playlist.Add(track);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "<Add> " + track_ + " <To> " + playlist_;
        }

        /// <summary>
        /// Playlist to add to
        /// </summary>
        private IPlaylist playlist_;

        /// <summary>
        /// Track to add
        /// </summary>
        private ITrack track_;
    }
}
