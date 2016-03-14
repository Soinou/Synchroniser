using Synchroniser.Changes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synchroniser.Libraries
{
    /// <summary>
    /// Represents a library synchroniser
    /// </summary>
    public interface ISynchroniser
    {
        /// <summary>
        /// Gets the list of changes the synchroniser needs to make
        /// </summary>
        IEnumerable<IChange> Changes
        {
            get;
        }

        /// <summary>
        /// Loads the changes the synchroniser needs to make
        /// 
        /// Callback will be given values from 0 to 100 as well as the current load step
        /// </summary>
        /// <param name="on_progress">Progress callback</param>
        /// <returns>The list of changes (async)</returns>
        Task Load(Action<float, string> on_progress);

        /// <summary>
        /// Perform the queued changes
        /// 
        /// Callback will be given values from 0 to 100 as well as the currently running change
        /// </summary>
        /// <param name="on_progress">Progress callback</param>
        /// <returns>An awaitable task</returns>
        Task Synchronise(Action<float, string> on_progress);
    }
}
