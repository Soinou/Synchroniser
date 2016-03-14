using MusicBeePlugin;
using Synchroniser.Changes;
using Synchroniser.Libraries.ITunes;
using Synchroniser.Libraries.MusicBee;
using Synchroniser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synchroniser.Libraries.Synchronisers
{
    /// <summary>
    /// Helper to synchronise MusicBee and iTunes libraries
    /// </summary>
    internal class ITunesSynchroniser : ISynchroniser, IDisposable
    {
        /// <summary>
        /// Creates a new ITunesSynchroniser
        /// </summary>
        /// <param name="api">MusicBee api interface</param>
        public ITunesSynchroniser(Plugin.MusicBeeApiInterface api)
        {
            bee_ = new MusicBeeLibrary(api);
            itunes_ = new ITunesLibrary();
            changes_ = new Queue<IChange>();
        }

        /// <inheritdoc />
        public IEnumerable<IChange> Changes
        {
            get
            {
                return changes_;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
        }

        /// <inheritdoc />
        public Task Load(Action<float, string> on_progress)
        {
            return Task.Run(() =>
            {
                on_progress(0, "Loading libraries");

                bee_.Load((current, total, message) =>
                {
                    float progress = current * 20.0f / total;

                    on_progress(progress, message);
                });

                itunes_.Load((current, total, message) =>
                {
                    float progress = 20.0f + current * 20.0f / total;

                    on_progress(progress, message);
                });

                on_progress(40, "Retrieving content");

                // Get all the playlists
                var bee_playlists = bee_.Playlists;
                var itunes_playlists = itunes_.Playlists;

                // Playlists that are actually both in MusicBee and iTunes
                var common_playlists = bee_playlists.Union(itunes_playlists);

                // Playlists that are not in iTunes
                var playlists_to_add = bee_playlists.Except(itunes_playlists);

                // Playlists that are not in MusicBee
                var playlists_to_remove = itunes_playlists.Except(bee_playlists);

                // All the songs in MusicBee playlists
                var bee_songs = bee_playlists.Tracks();

                // All the songs in iTunes
                var itunes_songs = itunes_.Tracks;

                on_progress(50, "Checking missing tracks");

                // Songs we need to add to iTunes
                foreach (var song in bee_songs.Except(itunes_songs))
                {
                    changes_.Enqueue(new AddTrack(song));
                }

                on_progress(60, "Checking missing playlists");

                // Playlists we need to add to iTunes
                foreach (var playlist in bee_playlists.Except(itunes_playlists))
                {
                    changes_.Enqueue(new AddPlaylist(playlist));

                    // Songs we need to add to this playlist
                    foreach (var song in playlist.Tracks)
                    {
                        changes_.Enqueue(new AddTrackToPlaylist(playlist, song));
                    }
                }

                on_progress(70, "Checking existing playlists changes");

                // Common playlists between MusicBee and iTunes
                foreach (var playlists in bee_playlists.Common(itunes_playlists))
                {
                    var playlist = playlists.Item1;

                    // MusicBee playlist songs
                    var first_songs = playlist.Tracks;

                    // iTunes playlist songs
                    var second_songs = playlists.Item2.Tracks;

                    // Add all songs that need to be added
                    foreach (var song in first_songs.Except(second_songs))
                    {
                        changes_.Enqueue(new AddTrackToPlaylist(playlist, song));
                    }

                    // Remove all songs that need to be removed
                    foreach (var song in second_songs.Except(first_songs))
                    {
                        changes_.Enqueue(new RemoveTrackFromPlaylist(playlist, song));
                    }
                }

                on_progress(80, "Checking unused playlists");

                // Playlists we need to remove from iTunes
                foreach (var playlist in itunes_playlists.Except(bee_playlists))
                {
                    changes_.Enqueue(new RemovePlaylist(playlist));
                }

                on_progress(90, "Checking unused tracks");

                // Songs we need to remove from iTunes
                foreach (var song in itunes_songs.Except(bee_songs))
                {
                    changes_.Enqueue(new RemoveTrack(song));
                }

                on_progress(100, "Done");
            });
        }

        /// <inheritdoc />
        public Task Synchronise(Action<float, string> on_progress)
        {
            return Task.Run(() =>
            {
                float total = changes_.Count;
                float current = 0.0f;

                while (changes_.Count > 0)
                {
                    var change = changes_.Dequeue();

                    on_progress((current / total) * 100.0f, change.ToString());

                    change.Run(itunes_);

                    current++;
                }
            });
        }

        /// <inheritdoc />
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed_)
            {
                if (disposing)
                {
                    bee_ = null;
                    itunes_ = null;
                }

                disposed_ = true;
            }
        }

        /// <summary>
        /// MusicBee library
        /// </summary>
        private MusicBeeLibrary bee_;

        /// <summary>
        /// Queued changes
        /// </summary>
        private Queue<IChange> changes_;

        /// <summary>
        /// If the resources are disposed
        /// </summary>
        private bool disposed_;

        /// <summary>
        /// iTunes library
        /// </summary>
        private ITunesLibrary itunes_;
    }
}
