using Synchroniser.Libraries;

namespace Synchroniser.Changes
{
    /// <summary>
    /// Adds a track
    /// </summary>
    internal class AddTrack : IChange
    {
        /// <summary>
        /// Creates a new AddTrack
        /// </summary>
        /// <param name="track">Track to add</param>
        public AddTrack(ITrack track)
        {
            track_ = track;
        }

        /// <inheritdoc />
        public ChangeTypes Type
        {
            get
            {
                return ChangeTypes.kAddTrack;
            }
        }

        /// <inheritdoc />
        public void Run(ILibrary library)
        {
            var track = library.Find(track_);

            if (track != null)
            {
                throw new InvalidChangeException("Track '" + track_ + "' already exists in library");
            }
            else
            {
                library.Add(track_);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "<Add> " + track_;
        }

        /// <summary>
        /// Track to add
        /// </summary>
        private ITrack track_;
    }
}
