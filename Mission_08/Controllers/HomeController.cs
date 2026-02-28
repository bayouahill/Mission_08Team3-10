// MVC framework for handling HTTP requests and returning views
using Microsoft.AspNetCore.Mvc;
// SelectList for dropdown population
using Microsoft.AspNetCore.Mvc.Rendering;
// Task and Category models
using Mission_08Team3_10.Models;
// Repository pattern for data access
using Mission_08Team3_10.Repositories;
// Alias to avoid conflict with System.Threading.Tasks.Task
using TaskModel = Mission_08Team3_10.Models.Task;

namespace Mission_08Team3_10.Controllers
{
    /// <summary>
    /// Controller actions for Quadrants View and CRUD functionality.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ITaskRepository _repo;

        public HomeController(ITaskRepository repo)
        {
            _repo = repo;
        }

        // ----------------------------
        // INDEX
        // ----------------------------
        public IActionResult Index()
        {
            return View();
        }

        // ----------------------------
        // QUADRANTS
        // ----------------------------
        public IActionResult Quadrants()
        {
            var tasks = _repo.GetIncompleteTasks();
            return View(tasks);
        }

        // ============================
        // CREATE - checking to see if this stays will this change
        // ============================

        /// <summary>
        /// GET: Show form to create a new task.
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories =
                new SelectList(_repo.GetCategories(),
                               "CategoryId",
                               "CategoryName");

            return View(new TaskModel());
        }

        /// <summary>
        /// POST: Add new task to database.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                _repo.AddTask(task);
                return RedirectToAction("Quadrants");
            }

            // Repopulate dropdown if validation fails
            ViewBag.Categories =
                new SelectList(_repo.GetCategories(),
                               "CategoryId",
                               "CategoryName",
                               task.CategoryId);

            return View(task);
        }

        // ============================
        // EDIT
        // ============================

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _repo.GetTaskById(id);

            if (task == null)
                return NotFound();

            ViewBag.Categories =
                new SelectList(_repo.GetCategories(),
                               "CategoryId",
                               "CategoryName",
                               task.CategoryId);

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateTask(task);
                return RedirectToAction("Quadrants");
            }

            ViewBag.Categories =
                new SelectList(_repo.GetCategories(),
                               "CategoryId",
                               "CategoryName",
                               task.CategoryId);

            return View(task);
        }

        // ============================
        // DELETE
        // ============================

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var task = _repo.GetTaskById(id);

            if (task == null)
                return NotFound();

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _repo.GetTaskById(id);

            if (task == null)
                return NotFound();

            _repo.DeleteTask(id);
            return RedirectToAction("Quadrants");
        }

        // ============================
        // COMPLETE
        // ============================

        [HttpGet]
        public IActionResult Complete(int id)
        {
            var task = _repo.GetTaskById(id);

            if (task == null)
                return NotFound();

            _repo.MarkComplete(id);
            return RedirectToAction("Quadrants");
        }
    }
}