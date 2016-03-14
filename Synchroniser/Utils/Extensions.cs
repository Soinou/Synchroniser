using Synchroniser.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Synchroniser.Utils
{
    /// <summary>
    /// Sugary extensions to collections
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Returns a list of common objects between two playlist collections
        /// 
        /// Will return a list of tuples with both playlists
        /// </summary>
        /// <param name="source">Source collection</param>
        /// <param name="second">Second collection</param>
        /// <returns>List of common objects</returns>
        public static IEnumerable<Tuple<IPlaylist, IPlaylist>> Common(this IEnumerable<IPlaylist> source, IEnumerable<IPlaylist> second)
        {
            var union = new List<Tuple<IPlaylist, IPlaylist>>();

            foreach (var left in source)
            {
                foreach (var right in second)
                {
                    if (left.Equals(right))
                        union.Add(new Tuple<IPlaylist, IPlaylist>(left, right));
                }
            }

            return union;
        }

        /// <summary>
        /// Find a playlist in a playlist collection by value
        /// </summary>
        /// <param name="source">Source collection</param>
        /// <param name="value">Value to search for</param>
        /// <returns>Value in the source collection</returns>
        public static IPlaylist Find(this IEnumerable<IPlaylist> source, IPlaylist value)
        {
            return source.SingleOrDefault(item => item.Equals(value));
        }

        /// <summary>
        /// Find song in a song collection by value
        /// </summary>
        /// <param name="source">Source collection</param>
        /// <param name="value">Value to search for</param>
        /// <returns>Value in the source collection</returns>
        public static ITrack Find(this IEnumerable<ITrack> source, ITrack value)
        {
            return source.SingleOrDefault(item => item.Equals(value));
        }

        /// <summary>
        /// Returns a string and replaces eventual null values by empty string
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>New string (Either string or empty string if the string was null)</returns>
        public static string NotNull(this string source)
        {
            if (source == null)
                return "";
            else
                return source;
        }

        /// <summary>
        /// Flatten all the tracks in a list of playlists
        /// </summary>
        /// <param name="source">Source collection</param>
        /// <returns>List of tracks in this collection</returns>
        public static IEnumerable<ITrack> Tracks(this IEnumerable<IPlaylist> source)
        {
            return source.SelectMany(playlist => playlist.Tracks).Distinct();
        }
    }
}
