using Synchroniser.Utils;

namespace Synchroniser.Libraries.Base
{
    /// <summary>
    /// Base implementation of the ITrack interface
    /// </summary>
    internal abstract class TrackBase : ITrack
    {
        /// <summary>
        /// Creates a new TrackBase
        /// </summary>
        /// <param name="path">Track path</param>
        /// <param name="artist">Track artist</param>
        /// <param name="title">Track title</param>
        /// <param name="album">Track album</param>
        public TrackBase(string path, string artist, string title, string album)
        {
            artist_ = artist.NotNull().Trim();
            title_ = title.NotNull().Trim();
            path_ = System.IO.Path.GetFullPath(path).ToLowerInvariant();
            album_ = album_.NotNull().Trim();
        }

        /// <inheritdoc />
        public string Album
        {
            get
            {
                return album_;
            }
        }

        /// <inheritdoc />
        public string Artist
        {
            get
            {
                return artist_;
            }
        }

        /// <inheritdoc />
        public string Path
        {
            get
            {
                return path_;
            }
        }

        /// <inheritdoc />
        public string Title
        {
            get
            {
                return title_;
            }
        }

        /// <inheritdoc />
        public bool Equals(ITrack other)
        {
            return string.Equals(path_, other.Path);
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            if (other is ITrack)
                return Equals(other as ITrack);
            else
                return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return path_.GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            string artist = Artist;
            string title = Title;

            if (string.IsNullOrEmpty(artist))
            {
                return title;
            }
            else if (string.IsNullOrEmpty(title))
            {
                return artist;
            }
            else
            {
                return artist + " - " + title;
            }
        }

        /// <summary>
        /// Track album
        /// </summary>
        private string album_;

        /// <summary>
        /// Track artist
        /// </summary>
        private string artist_;

        /// <summary>
        /// Track path
        /// </summary>
        private string path_;

        /// <summary>
        /// Track title
        /// </summary>
        private string title_;
    }
}
