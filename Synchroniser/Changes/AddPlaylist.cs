using Synchroniser.Libraries;

namespace Synchroniser.Changes
{
    /// <summary>
    /// Adds a playlist
    /// </summary>
    internal class AddPlaylist : IChange
    {
        /// <summary>
        /// Creates a new AddPlaylist
        /// </summary>
        /// <param name="playlist">Playlist to add</param>
        public AddPlaylist(IPlaylist playlist)
        {
            playlist_ = playlist;
        }

        /// <inheritdoc />
        public ChangeTypes Type
        {
            get
            {
                return ChangeTypes.kAddPlaylist;
            }
        }

        /// <inheritdoc />
        public void Run(ILibrary library)
        {
            var playlist = library.Find(playlist_);

            if (playlist != null)
            {
                throw new InvalidChangeException("Playlist '" + playlist_ + "' already exists in library");
            }
            else
            {
                library.Add(playlist_);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "<Add> " + playlist_;
        }

        /// <summary>
        /// Playlist to add
        /// </summary>
        private IPlaylist playlist_;
    }
}
