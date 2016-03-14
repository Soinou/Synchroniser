using Synchroniser.Utils;
using System;
using System.Collections.Generic;

namespace Synchroniser.Libraries.Base
{
    /// <summary>
    /// Base implementation of a playlist
    /// </summary>
    internal abstract class PlaylistBase : IPlaylist
    {
        /// <summary>
        /// Creates a new PlaylistBase
        /// </summary>
        /// <param name="name">Playlist name</param>
        public PlaylistBase(string name)
        {
            name_ = name.NotNull().Trim();
        }

        /// <inheritdoc />
        public string Name
        {
            get
            {
                return name_;
            }
        }

        /// <inheritdoc />
        public abstract IEnumerable<ITrack> Tracks
        {
            get;
        }

        /// <inheritdoc />
        public abstract void Add(ITrack track);

        /// <inheritdoc />
        public bool Equals(IPlaylist other)
        {
            return string.Equals(name_, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            if (other is IPlaylist)
                return Equals(other as IPlaylist);
            else
                return false;
        }

        /// <inheritdoc />
        public abstract ITrack Find(ITrack track);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return name_.GetHashCode();
        }

        /// <inheritdoc />
        public abstract void Remove(ITrack track);

        /// <inheritdoc />
        public override string ToString()
        {
            return name_;
        }

        /// <summary>
        /// Playlist name
        /// </summary>
        private string name_;
    }
}
