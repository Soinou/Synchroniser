using Synchroniser.Libraries;

namespace Synchroniser.Changes
{
    /// <summary>
    /// Removes a track
    /// </summary>
    internal class RemoveTrack : IChange
    {
        /// <summary>
        /// Creates a new RemoveTrack
        /// </summary>
        /// <param name="track">Track to remove</param>
        public RemoveTrack(ITrack track)
        {
            track_ = track;
        }

        /// <inheritdoc />
        public ChangeTypes Type
        {
            get
            {
                return ChangeTypes.kRemoveTrack;
            }
        }

        /// <inheritdoc />
        public void Run(ILibrary library)
        {
            var track = library.Find(track_);

            if (track == null)
            {
                throw new InvalidChangeException("Track '" + track_ + "' does not exist in library");
            }
            else
            {
                library.Remove(track_);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "<Remove> " + track_;
        }

        /// <summary>
        /// Track to remove
        /// </summary>
        private ITrack track_;
    }
}
