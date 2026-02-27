// MVC framework for handling HTTP requests and returning views
using Microsoft.AspNetCore.Mvc;
// SelectList for dropdown population
using Microsoft.AspNetCore.Mvc.Rendering;
// Entity Framework - needed for .Include(), FindAsync, SaveChanges
using Microsoft.EntityFrameworkCore;
// Task and Category models
using Mission_08Team3_10.Models;
// Alias to avoid conflict with System.Threading.Tasks.Task
using TaskModel = Mission_08Team3_10.Models.Task;

namespace Mission_08Team3_10.Controllers
{
    /// <summary>
    /// Controller actions for Quadrants View and CRUD functionality.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly Mission08Context _context;

        public HomeController(Mission08Context context)
        {
            _context = context;
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
            var tasks = _context.Tasks
                .Include(t => t.Category)
                .Where(t => t.Completed == false)
                .ToList();

            return View(tasks);
        }

        // ============================
        // CREATE
        // ============================

        /// <summary>
        /// GET: Show form to create a new task.
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories =
                new SelectList(_context.Categories,
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
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction("Quadrants");
            }

            // Repopulate dropdown if validation fails
            ViewBag.Categories =
                new SelectList(_context.Categories,
                               "CategoryId",
                               "CategoryName",
                               task.CategoryId);

            return View(task);
        }

        // ============================
        // EDIT
        // ============================

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.TaskId == id);

            if (task == null)
                return NotFound();

            ViewBag.Categories =
                new SelectList(_context.Categories,
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
                _context.Tasks.Update(task);
                _context.SaveChanges();
                return RedirectToAction("Quadrants");
            }

            ViewBag.Categories =
                new SelectList(_context.Categories,
                               "CategoryId",
                               "CategoryName",
                               task.CategoryId);

            return View(task);
        }

        // ============================
        // DELETE
        // ============================

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.TaskId == id);

            if (task == null)
                return NotFound();

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction("Quadrants");
        }

        // ============================
        // COMPLETE
        // ============================

        [HttpGet]
        public async Task<IActionResult> Complete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            task.Completed = true;
            _context.Tasks.Update(task);
            _context.SaveChanges();
            return RedirectToAction("Quadrants");
        }
    }
}