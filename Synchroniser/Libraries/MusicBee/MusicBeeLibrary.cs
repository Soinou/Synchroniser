using Synchroniser.Utils;
using MusicBeePlugin;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Synchroniser.Libraries.MusicBee
{
    /// <summary>
    /// MusicBee interface wrapper
    /// </summary>
    internal class MusicBeeLibrary : ILibrary
    {
        /// <summary>
        /// Creates a new MusicBee wrapper
        /// </summary>
        /// <param name="api">MusicBee interface</param>
        public MusicBeeLibrary(Plugin.MusicBeeApiInterface api)
        {
            api_ = api;
            playlists_ = new List<MusicBeePlaylist>();
            tracks_ = null;
        }

        /// <inheritdoc />
        public IEnumerable<IPlaylist> Playlists
        {
            get
            {
                return playlists_.ConvertAll(playlist => (IPlaylist)playlist);
            }
        }

        /// <inheritdoc />
        public IEnumerable<ITrack> Tracks
        {
            get
            {
                return tracks_;
            }
        }

        /// <inheritdoc />
        public void Add(IPlaylist playlist)
        {
            throw new InvalidOperationException("MusicBee library can't be modified");
        }

        /// <inheritdoc />
        public void Add(ITrack track)
        {
            throw new InvalidOperationException("MusicBee library can't be modified");
        }

        /// <inheritdoc />
        public IPlaylist Find(IPlaylist playlist)
        {
            return playlists_.Find(playlist);
        }

        /// <inheritdoc />
        public ITrack Find(ITrack track)
        {
            return tracks_.Find(track);
        }

        /// <summary>
        /// Loads the MusicBee library
        /// </summary>
        /// <param name="on_playlist">Progress callback</param>
        public void Load(Action<float, float, string> on_playlist)
        {
            if (!api_.Playlist_QueryPlaylists())
                return;

            var urls = new List<string>();

            string current = api_.Playlist_QueryGetNextPlaylist();

            while (current != null)
            {
                urls.Add(current);

                current = api_.Playlist_QueryGetNextPlaylist();
            }

            var total = urls.Count;

            for (int i = 0; i < total; i++)
            {
                var url = urls[i];

                // Get the playlist name
                string name = api_.Playlist_GetName(url);

                on_playlist(i, total, "Loading MusicBee playlist '" + name + "'");

                // Add this playlist to our list of playlists
                playlists_.Add(new MusicBeePlaylist(url, api_));
            }

            tracks_ = playlists_.SelectMany(playlist => playlist.Tracks).Distinct();
        }

        /// <inheritdoc />
        public void Remove(IPlaylist playlist)
        {
            throw new InvalidOperationException("MusicBee library can't be modified");
        }

        /// <inheritdoc />
        public void Remove(ITrack track)
        {
            throw new InvalidOperationException("MusicBee library can't be modified");
        }

        /// <summary>
        /// MusicBee api interface
        /// </summary>
        private Plugin.MusicBeeApiInterface api_;

        /// <summary>
        /// Library playlists
        /// </summary>
        private List<MusicBeePlaylist> playlists_;

        /// <summary>
        /// Library tracks
        /// </summary>
        private IEnumerable<ITrack> tracks_;
    }
}
