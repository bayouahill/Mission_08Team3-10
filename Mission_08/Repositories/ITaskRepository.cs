using Mission_08Team3_10.Models;
using TaskModel = Mission_08Team3_10.Models.Task;

namespace Mission_08Team3_10.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskModel> GetIncompleteTasks();
        TaskModel? GetTaskById(int id);
        IEnumerable<Category> GetCategories();
        void AddTask(TaskModel task);
        void UpdateTask(TaskModel task);
        void DeleteTask(int id);
        void MarkComplete(int id);
    }
}
