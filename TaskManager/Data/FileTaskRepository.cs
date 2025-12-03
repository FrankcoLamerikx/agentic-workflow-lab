using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaskManager.Models;

namespace TaskManager.Data
{
    /// <summary>
    /// File-based implementation of the task repository that persists tasks to a text file.
    /// This repository uses a simple pipe-delimited format for storing task data.
    /// </summary>
    /// <remarks>
    /// Tasks are stored in the format: Id|Description|IsCompleted
    /// The repository maintains an in-memory cache of tasks and synchronizes with the file on each operation.
    /// </remarks>
    public sealed class FileTaskRepository : ITaskRepository
    {
        private readonly string _filePath;
        private readonly List<Task> _tasks;
        private int _nextId;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTaskRepository"/> class.
        /// </summary>
        /// <param name="filePath">The path to the file where tasks will be stored.</param>
        /// <remarks>
        /// If the file exists, tasks will be loaded from it. If the file doesn't exist or is corrupted,
        /// the repository will start with an empty task list.
        /// </remarks>
        public FileTaskRepository(string filePath)
        {
            _filePath = filePath;
            _tasks = new List<Task>();
            _nextId = 1;
            LoadTasks();
        }

        /// <summary>
        /// Adds a new task to the repository and persists it to the file.
        /// </summary>
        /// <param name="task">The task to add. The Id property will be automatically assigned.</param>
        /// <exception cref="InvalidOperationException">Thrown when the task cannot be saved to the file.</exception>
        public void Add(Task task)
        {
            task.Id = _nextId++;
            _tasks.Add(task);
            SaveTasks();
        }

        /// <summary>
        /// Retrieves all tasks from the repository.
        /// </summary>
        /// <returns>A list of all tasks, both completed and incomplete.</returns>
        public List<Task> GetAll()
        {
            return _tasks;
        }

        /// <summary>
        /// Retrieves a task by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        /// <returns>The task with the specified ID, or null if not found.</returns>
        public Task GetById(int id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Updates an existing task in the repository and persists the changes to the file.
        /// </summary>
        /// <param name="task">The task with updated information. The Id must match an existing task.</param>
        /// <exception cref="InvalidOperationException">Thrown when the updated task cannot be saved to the file.</exception>
        /// <remarks>
        /// If no task with the specified ID exists, the method silently returns without making changes.
        /// </remarks>
        public void Update(Task task)
        {
            var existingTask = GetById(task.Id);
            if (existingTask != null)
            {
                existingTask.Description = task.Description;
                existingTask.IsCompleted = task.IsCompleted;
                SaveTasks();
            }
        }

        /// <summary>
        /// Loads tasks from the file into memory.
        /// </summary>
        /// <remarks>
        /// If the file doesn't exist or is corrupted, the repository will start with an empty task list.
        /// The method silently handles file read errors by resetting to an empty state.
        /// </remarks>
        private void LoadTasks()
        {
            if (!File.Exists(_filePath))
            {
                return;
            }

            try
            {
                var lines = File.ReadAllLines(_filePath);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        var id = int.Parse(parts[0]);
                        var description = parts[1];
                        var isCompleted = bool.Parse(parts[2]);

                        var task = new Task(id, description)
                        {
                            IsCompleted = isCompleted
                        };
                        _tasks.Add(task);

                        if (id >= _nextId)
                        {
                            _nextId = id + 1;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // If file is corrupted, start fresh
                _tasks.Clear();
                _nextId = 1;
            }
        }

        /// <summary>
        /// Saves all tasks from memory to the file.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when tasks cannot be written to the file.</exception>
        private void SaveTasks()
        {
            try
            {
                var lines = _tasks.Select(t => $"{t.Id}|{t.Description}|{t.IsCompleted}");
                File.WriteAllLines(_filePath, lines);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to save tasks to file.", ex);
            }
        }
    }
}
