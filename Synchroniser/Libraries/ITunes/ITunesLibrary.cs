using iTunesLib;
using Synchroniser.Utils;
using System;
using System.Collections.Generic;

namespace Synchroniser.Libraries.ITunes
{
    /// <summary>
    /// Represents an iTunes library
    /// </summary>
    internal class ITunesLibrary : ILibrary
    {
        /// <summary>
        /// Creates a new ITunesLibrary
        /// </summary>
        public ITunesLibrary()
        {
            app_ = new iTunesApp();
            library_ = app_.LibraryPlaylist;
            playlists_ = new List<ITunesPlaylist>();
            tracks_ = new List<ITunesTrack>();
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
                return tracks_.ConvertAll(track => (ITrack)track);
            }
        }

        /// <inheritdoc />
        public void Add(IPlaylist playlist)
        {
            if (Find(playlist) == null)
            {
                var new_playlist = app_.CreatePlaylist(playlist.Name) as IITUserPlaylist;

                playlists_.Add(new ITunesPlaylist(new_playlist));
            }
        }

        /// <inheritdoc />
        public void Add(ITrack track)
        {
            if (Find(track) == null)
            {
                var status = library_.AddFile(track.Path);

                while (status.InProgress) ;

                var new_track = status.Tracks[1] as IITFileOrCDTrack;

                tracks_.Add(new ITunesTrack(new_track));
            }
        }

        /// <inheritdoc />
        public ITrack Find(ITrack track)
        {
            return tracks_.Find(track);
        }

        /// <inheritdoc />
        public IPlaylist Find(IPlaylist playlist)
        {
            return playlists_.Find(playlist);
        }

        /// <summary>
        /// Loads the library
        /// </summary>
        /// <param name="on_progress">Progress callback</param>
        public void Load(Action<float, float, string> on_progress)
        {
            var tracks = library_.Tracks;
            var playlists = app_.LibrarySource.Playlists;

            var current = 0;
            var total = tracks.Count + playlists.Count;

            foreach (IITTrack item in tracks)
            {
                on_progress(current++, total, "Loading iTunes track '" + item.Name + "'");

                if (item.Kind == ITTrackKind.ITTrackKindFile)
                {
                    var track = new ITunesTrack(item as IITFileOrCDTrack);

                    // Delete songs with empty path (Errors)
                    if (string.IsNullOrEmpty(track.Path))
                    {
                        track.Delete();
                    }
                    else
                    {
                        tracks_.Add(track);
                    }
                }
            }

            foreach (IITPlaylist item in playlists)
            {
                on_progress(current++, total, "Loading iTunes playlist '" + item.Name + "'");

                if (item.Kind == ITPlaylistKind.ITPlaylistKindUser)
                {
                    var user = item as IITUserPlaylist;

                    if (user.SpecialKind == ITUserPlaylistSpecialKind.ITUserPlaylistSpecialKindNone
                        && !user.Smart)
                    {
                        playlists_.Add(new ITunesPlaylist(user));
                    }
                }
            }
        }

        /// <inheritdoc />
        public void Remove(ITrack track)
        {
            for (int i = 0; i < tracks_.Count; i++)
            {
                var item = tracks_[i];

                if (item.Equals(track))
                {
                    item.Delete();
                    tracks_.RemoveAt(i);
                    return;
                }
            }
        }

        /// <inheritdoc />
        public void Remove(IPlaylist playlist)
        {
            for (int i = 0; i < playlists_.Count; i++)
            {
                var item = playlists_[i];

                if (item.Equals(playlist))
                {
                    item.Delete();
                    playlists_.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Internal iTunes app
        /// </summary>
        private iTunesApp app_;

        /// <summary>
        /// Itunes playlist
        /// </summary>
        private IITLibraryPlaylist library_;

        /// <summary>
        /// List of playlists
        /// </summary>
        private List<ITunesPlaylist> playlists_;

        /// <summary>
        /// List of songs
        /// </summary>
        private List<ITunesTrack> tracks_;
    }
}
