using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission_08Team3_10.Repositories;
using TaskModel = Mission_08Team3_10.Models.Task;

namespace Mission_08Team3_10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskRepository _repo;

        public HomeController(ITaskRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Quadrants()
        {
            var tasks = _repo.GetIncompleteTasks();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _repo.GetTaskById(id);
            if (task == null)
                return NotFound();

            ViewBag.Categories = new SelectList(_repo.GetCategories(), "CategoryId", "CategoryName");
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateTask(task);
                return RedirectToAction("Quadrants");
            }
            ViewBag.Categories = new SelectList(_repo.GetCategories(), "CategoryId", "CategoryName");
            return View(task);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var task = _repo.GetTaskById(id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteTask(id);
            return RedirectToAction("Quadrants");
        }

        [HttpGet]
        public IActionResult Complete(int id)
        {
            _repo.MarkComplete(id);
            return RedirectToAction("Quadrants");
        }
    }
}
