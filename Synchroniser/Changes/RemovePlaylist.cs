using Synchroniser.Libraries;

namespace Synchroniser.Changes
{
    /// <summary>
    /// Removes a playlist
    /// </summary>
    internal class RemovePlaylist : IChange
    {
        /// <summary>
        /// Creates a new RemovePlaylist
        /// </summary>
        /// <param name="playlist">Playlist to remove</param>
        public RemovePlaylist(IPlaylist playlist)
        {
            playlist_ = playlist;
        }

        /// <inheritdoc />
        public ChangeTypes Type
        {
            get
            {
                return ChangeTypes.kRemovePlaylist;
            }
        }

        /// <inheritdoc />
        public void Run(ILibrary library)
        {
            var playlist = library.Find(playlist_);

            if (playlist == null)
            {
                throw new InvalidChangeException("Playlist '" + playlist_ + "' does not exist in library");
            }
            else
            {
                library.Remove(playlist_);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "<Remove> " + playlist_;
        }

        /// <summary>
        /// Playlist to remove
        /// </summary>
        private IPlaylist playlist_;
    }
}
