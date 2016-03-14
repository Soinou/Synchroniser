using iTunesLib;
using Synchroniser.Libraries.Base;
using System.Collections.Generic;

namespace Synchroniser.Libraries.ITunes
{
    /// <summary>
    /// iTunes implementation of a playlist
    /// </summary>
    internal class ITunesPlaylist : PlaylistBase
    {
        /// <summary>
        /// Creates a new ITunesPlaylist
        /// </summary>
        /// <param name="playlist">iTunes playlist</param>
        public ITunesPlaylist(IITUserPlaylist playlist)
            : base(playlist.Name)
        {
            playlist_ = playlist;
            tracks_ = new List<ITunesTrack>();

            foreach (IITTrack track in playlist_.Tracks)
            {
                if (track.Kind == ITTrackKind.ITTrackKindFile)
                {
                    var song = new ITunesTrack(track as IITFileOrCDTrack);

                    // Delete songs with empty path (Errors)
                    if (string.IsNullOrEmpty(song.Path))
                    {
                        song.Delete();
                    }
                    else
                    {
                        tracks_.Add(song);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the playlist tracks
        /// </summary>
        public override IEnumerable<ITrack> Tracks
        {
            get
            {
                return tracks_.ConvertAll(track => (ITrack)track);
            }
        }

        /// <inheritdoc />
        public override void Add(ITrack track)
        {
            var itunes_track = track as ITunesTrack;

            if (itunes_track != null)
            {
                var new_track = playlist_.AddTrack(itunes_track.Track) as IITFileOrCDTrack;

                tracks_.Add(new ITunesTrack(new_track));
            }
        }

        /// <summary>
        /// Deletes the playlist
        /// </summary>
        public void Delete()
        {
            playlist_.Delete();
        }

        /// <inheritdoc />
        public override ITrack Find(ITrack track)
        {
            for (int i = 0; i < tracks_.Count; i++)
            {
                var item = tracks_[i];

                if (item.Equals(track))
                {
                    return item;
                }
            }

            return null;
        }

        /// <inheritdoc />
        public override void Remove(ITrack track)
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

        /// <summary>
        /// iTunes playlist
        /// </summary>
        private IITUserPlaylist playlist_;

        /// <summary>
        /// Track list
        /// </summary>
        private List<ITunesTrack> tracks_;
    }
}
