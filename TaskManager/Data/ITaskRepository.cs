using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Data
{
    /// <summary>
    /// Defines methods for managing tasks in the repository.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Adds a new task to the repository.
        /// </summary>
        /// <param name="task">The task to add.</param>
        void Add(Task task);

        /// <summary>
        /// Retrieves all tasks from the repository.
        /// </summary>
        /// <returns>A list of all tasks.</returns>
        List<Task> GetAll();

        /// <summary>
        /// Retrieves a task by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the task.</param>
        /// <returns>The task with the specified id, or null if not found.</returns>
        Task GetById(int id);

        /// <summary>
        /// Updates an existing task in the repository.
        /// </summary>
        /// <param name="task">The task to update.</param>
        void Update(Task task);
    }
}
