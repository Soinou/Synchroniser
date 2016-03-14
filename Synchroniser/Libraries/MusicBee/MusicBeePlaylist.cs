using Synchroniser.Libraries.Base;
using Synchroniser.Utils;
using MusicBeePlugin;
using System;
using System.Collections.Generic;

namespace Synchroniser.Libraries.MusicBee
{
    /// <summary>
    /// MusicBee implementation of a playlist
    /// </summary>
    internal class MusicBeePlaylist : PlaylistBase
    {
        /// <summary>
        /// Creates a new MusicBeePlaylist
        /// </summary>
        /// <param name="url">Playlist url</param>
        /// <param name="api">MusicBee api interface</param>
        public MusicBeePlaylist(string url, Plugin.MusicBeeApiInterface api)
            : base(api.Playlist_GetName(url))
        {
            tracks_ = new List<ITrack>();
            url_ = url;

            // Get playlist songs
            api.Playlist_QueryFiles(url_);

            string path = api.Playlist_QueryGetNextFile();

            while (path != null)
            {
                tracks_.Add(new MusicBeeTrack(path, api));

                path = api.Playlist_QueryGetNextFile();
            }
        }

        /// <inheritdoc />
        public override IEnumerable<ITrack> Tracks
        {
            get
            {
                return tracks_;
            }
        }

        /// <summary>
        /// Playlist url
        /// </summary>
        public string Url
        {
            get
            {
                return url_;
            }
        }

        /// <inheritdoc />
        public override void Add(ITrack track)
        {
            throw new InvalidOperationException("MusicBee library can't be modified");
        }

        /// <inheritdoc />
        public override ITrack Find(ITrack track)
        {
            return tracks_.Find(track);
        }

        /// <inheritdoc />
        public override void Remove(ITrack track)
        {
            throw new InvalidOperationException("MusicBee library can't be modified");
        }

        /// <summary>
        /// Track list
        /// </summary>
        private List<ITrack> tracks_;

        /// <summary>
        /// Playlist url
        /// </summary>
        private string url_;
    }
}
