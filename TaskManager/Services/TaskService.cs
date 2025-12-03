using System;
using System.Collections.Generic;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public void AddTask(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Task description cannot be empty.");
            }

            var task = new Task(0, description);
            _repository.Add(task);
        }

        public List<Task> GetAllTasks()
        {
            return _repository.GetAll();
        }

        public void CompleteTask(int taskNumber)
        {
            var tasks = _repository.GetAll();
            if (taskNumber < 1 || taskNumber > tasks.Count)
            {
                throw new ArgumentException($"Invalid task number. Please enter a number between 1 and {tasks.Count}.");
            }

            var task = tasks[taskNumber - 1];
            task.IsCompleted = true;
            _repository.Update(task);
        }
    }
}
