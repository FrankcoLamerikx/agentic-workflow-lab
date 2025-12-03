using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class FileTaskRepository : ITaskRepository
    {
        private readonly string _filePath;
        private readonly List<Task> _tasks;
        private int _nextId;

        public FileTaskRepository(string filePath)
        {
            _filePath = filePath;
            _tasks = new List<Task>();
            _nextId = 1;
            LoadTasks();
        }

        public void Add(Task task)
        {
            task.Id = _nextId++;
            _tasks.Add(task);
            SaveTasks();
        }

        public List<Task> GetAll()
        {
            return _tasks;
        }

        public Task GetById(int id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

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
