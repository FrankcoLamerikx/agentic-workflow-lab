using System;
using System.IO;
using TaskManager.Data;
using TaskManager.Presentation;
using TaskManager.Services;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskFilePath = Path.Combine(Environment.CurrentDirectory, "tasks.txt");
            var repository = new FileTaskRepository(taskFilePath);
            var taskService = new TaskService(repository);
            var parser = new CommandParser();

            if (!parser.Parse(args))
            {
                Console.WriteLine("Usage: TaskManager <command> [arguments]");
                Console.WriteLine("Commands:");
                Console.WriteLine("  add \"<description>\"  - Add a new task");
                Console.WriteLine("  list                 - Show all tasks");
                return;
            }

            try
            {
                switch (parser.Command)
                {
                    case "add":
                        if (string.IsNullOrEmpty(parser.Argument))
                        {
                            Console.WriteLine("Error: Task description is required.");
                            Console.WriteLine("Usage: TaskManager add \"<description>\"");
                            return;
                        }
                        taskService.AddTask(parser.Argument);
                        Console.WriteLine("Task added successfully.");
                        break;

                    case "list":
                        var tasks = taskService.GetAllTasks();
                        if (tasks.Count == 0)
                        {
                            Console.WriteLine("No tasks found.");
                        }
                        else
                        {
                            for (int i = 0; i < tasks.Count; i++)
                            {
                                var task = tasks[i];
                                var status = task.IsCompleted ? "[✓]" : "[ ]";
                                Console.WriteLine($"{i + 1}. {status} {task.Description}");
                            }
                        }
                        break;

                    default:
                        Console.WriteLine($"Unknown command: {parser.Command}");
                        Console.WriteLine("Available commands: add, list");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
