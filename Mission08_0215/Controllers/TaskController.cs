using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_0215.Models;

namespace Mission08_0215.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _repo;

        // Repository is injected via constructor - no direct DbContext access here
        public TaskController(ITaskRepository repo)
        {
            _repo = repo;
        }

        // GET: /Task/Index - landing page (redirects to Quadrants)
        public IActionResult Index()
        {
            return RedirectToAction("Quadrants");
        }

        // GET: /Task/Quadrants - displays all incomplete tasks laid out by quadrant
        public IActionResult Quadrants()
        {
            // Only show tasks that have NOT been completed
            var tasks = _repo.GetAllTasks()
                             .Where(t => !t.Completed)
                             .ToList();
            return View(tasks);
        }

        // GET: /Task/Create - blank form to add a new task
        public IActionResult Create()
        {
            ViewBag.Categories = _repo.GetAllCategories();
            return View();
        }

        // POST: /Task/Create - receives form data and adds the new task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _repo.AddTask(task);
                return RedirectToAction("Quadrants");
            }

            // If validation failed, reload categories and show the form again with errors
            ViewBag.Categories = _repo.GetAllCategories();
            return View(task);
        }

        // GET: /Task/Edit/{id} - pre-filled edit form
        public IActionResult Edit(int id)
        {
            var task = _repo.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _repo.GetAllCategories();
            return View(task);
        }

        // POST: /Task/Edit/{id} - saves edited task back to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TaskItem task)
        {
            if (id != task.TaskItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repo.UpdateTask(task);
                return RedirectToAction("Quadrants");
            }

            ViewBag.Categories = _repo.GetAllCategories();
            return View(task);
        }

        // POST: /Task/MarkComplete/{id} - marks a task as completed and hides it from quadrants
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkComplete(int id)
        {
            var task = _repo.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            task.Completed = true;
            _repo.UpdateTask(task);

            return RedirectToAction("Quadrants");
        }

        // GET: /Task/Delete/{id} - confirmation page before deleting
        public IActionResult Delete(int id)
        {
            var task = _repo.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: /Task/Delete/{id} - permanently deletes the task
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteTask(id);
            return RedirectToAction("Quadrants");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
