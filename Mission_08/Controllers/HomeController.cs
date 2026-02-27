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
    /// Controller actions for #3 scope: Quadrants View and Update/Delete/Complete functionality.
    /// </summary>
    public class HomeController : Controller
    {
        // DbContext for database access (Repository Pattern is #1/#4 responsibility)
        private readonly Mission08Context _context;

        public HomeController(Mission08Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Default landing page. (Index is #4's responsibility; keeping minimal for routing.)
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays all incomplete tasks organized into the four Covey quadrants.
        /// Assignment: "Only display tasks that have not been completed."
        /// </summary>
        public IActionResult Quadrants()
        {
            // .Include(t => t.Category) - eager-load Category so Quadrants view can display CategoryName
            // .Where(t => t.Completed == false) - assignment says only show NOT completed tasks
            var tasks = _context.Tasks
                .Include(t => t.Category)
                .Where(t => t.Completed == false)
                .ToList();

            return View(tasks);
        }

        /// <summary>
        /// GET: Show the form to edit an existing task. (Update ability - #3 scope)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.TaskId == id);
            if (task == null)
                return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(task);
        }

        /// <summary>
        /// POST: Process the submitted changes and update the task. (Update ability - #3 scope)
        /// </summary>
        [HttpPost]
        public IActionResult Edit(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Update(task);
                _context.SaveChanges();
                return RedirectToAction("Quadrants");
            }
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(task);
        }

        /// <summary>
        /// GET: Show confirmation page before deleting. (Delete ability - #3 scope)
        /// </summary>
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

        /// <summary>
        /// POST: Delete the task after user confirms. (Delete ability - #3 scope)
        /// </summary>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction("Quadrants");
        }

        /// <summary>
        /// GET: Mark a task as completed. (Mark as Completed - #3 scope)
        /// </summary>
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
