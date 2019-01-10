//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System.ComponentModel.Composition;

namespace Sce.Atf.Applications
{
    /// <summary>
    /// Batch Task interface. mainly interface with a bit of automation.
    /// </summary>
    public abstract class IBatchTask
    {
        public IBatchTask(IBatchTaskManager manager)
        {
            manager.RegisterBatchTask(this);
        }

        // TODO: add priority

        /// <summary>
        /// Execute the task
        /// </summary>
        public abstract void Execute();
    }



    /// <summary>
    /// Interface for Batch task collection
    /// </summary>
    public interface IBatchTaskManager
    {
        // Batch task
        void RegisterBatchTask(IBatchTask task);

        // Execute all registered batch task
        void ExecuteBatchTask();
    }
}