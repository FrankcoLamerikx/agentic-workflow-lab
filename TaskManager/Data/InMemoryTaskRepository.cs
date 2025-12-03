using System.Collections.Generic;
using System.Linq;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private readonly List<Task> _tasks;
        private int _nextId;

        public InMemoryTaskRepository()
        {
            _tasks = new List<Task>();
            _nextId = 1;
        }

        public void Add(Task task)
        {
            task.Id = _nextId++;
            _tasks.Add(task);
        }

        public List<Task> GetAll()
        {
            return _tasks;
        }

        public Task GetById(int id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }
    }
}
