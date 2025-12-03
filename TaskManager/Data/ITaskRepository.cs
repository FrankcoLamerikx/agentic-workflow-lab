using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Data
{
    public interface ITaskRepository
    {
        void Add(Task task);
        List<Task> GetAll();
        Task GetById(int id);
    }
}
