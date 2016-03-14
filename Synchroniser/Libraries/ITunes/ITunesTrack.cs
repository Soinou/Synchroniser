using iTunesLib;
using Synchroniser.Libraries.Base;

namespace Synchroniser.Libraries.ITunes
{
    /// <summary>
    /// iTunes implementation of a track
    /// </summary>
    internal class ITunesTrack : TrackBase
    {
        /// <summary>
        /// Creates a new ITunesTrack
        /// </summary>
        /// <param name="track">iTunes track</param>
        public ITunesTrack(IITFileOrCDTrack track)
            : base(track.Location, track.Artist, track.Name, track.Album)
        {
            track_ = track;
        }

        /// <summary>
        /// iTunes track
        /// </summary>
        public IITFileOrCDTrack Track
        {
            get
            {
                return track_;
            }
        }

        /// <summary>
        /// Deletes the track
        /// </summary>
        public void Delete()
        {
            track_.Delete();
        }

        /// <summary>
        /// iTunes track
        /// </summary>
        private IITFileOrCDTrack track_;
    }
}
