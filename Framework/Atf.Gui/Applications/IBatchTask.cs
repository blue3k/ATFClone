//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

namespace Sce.Atf.Applications
{
    /// <summary>
    /// Batch Task interface
    /// </summary>
    public interface IBatchTask
    {
        // TODO: add priority

        // Run
        void Execute();
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