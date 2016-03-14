using Synchroniser.Libraries.Base;
using MusicBeePlugin;

namespace Synchroniser.Libraries.MusicBee
{
    /// <summary>
    /// MusicBee implementation of a track
    /// </summary>
    internal class MusicBeeTrack : TrackBase
    {
        /// <summary>
        /// creates a new MusicBeeTrack
        /// </summary>
        /// <param name="path">Track path</param>
        /// <param name="api">MusicBee api interface</param>
        public MusicBeeTrack(string path, Plugin.MusicBeeApiInterface api)
            : base(path,
                  api.Library_GetFileTag(path, Plugin.MetaDataType.Artist),
                  api.Library_GetFileTag(path, Plugin.MetaDataType.TrackTitle),
                  api.Library_GetFileTag(path, Plugin.MetaDataType.Album))
        { }
    }
}
