using Microsoft.EntityFrameworkCore;
using Mission_08Team3_10.Models;
using TaskModel = Mission_08Team3_10.Models.Task;

namespace Mission_08Team3_10.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly Mission08Context _context;

        public TaskRepository(Mission08Context context)
        {
            _context = context;
        }

        public IEnumerable<TaskModel> GetIncompleteTasks()
        {
            return _context.Tasks
                .Include(t => t.Category)
                .Where(t => t.Completed == false)
                .ToList();
        }

        public TaskModel? GetTaskById(int id)
        {
            return _context.Tasks
                .Include(t => t.Category)
                .FirstOrDefault(t => t.TaskId == id);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public void AddTask(TaskModel task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(TaskModel task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }

        public void MarkComplete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                task.Completed = true;
                _context.Tasks.Update(task);
                _context.SaveChanges();
            }
        }
    }
}
