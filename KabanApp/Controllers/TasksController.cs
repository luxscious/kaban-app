using Microsoft.AspNetCore.Mvc;
using KabanApp.Data;
using KabanApp.Models;

namespace KabanApp.Controllers
{
    /// <summary>
    /// Controller responsible for managing TaskItem CRUD operations
    /// Handles HTTP requests for creating, reading, updating, and deleting tasks
    /// </summary>
    public class TasksController : Controller
    {
        // Database context for accessing task data
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor - Dependency injection of database context
        /// </summary>
        /// <param name="context">Entity Framework database context</param>
        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: /Tasks
        /// Displays all tasks in the system
        /// </summary>
        /// <returns>View with list of all tasks</returns>
        public IActionResult Index()
        {
            // Retrieve all tasks from database and convert to list
            var tasks = _context.Tasks.ToList();
            // Return view with tasks as model data
            return View(tasks);
        }

        /// <summary>
        /// GET: /Tasks/Create
        /// Displays the form for creating a new task
        /// </summary>
        /// <returns>Empty create form view</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: /Tasks/Create
        /// Processes the submitted create task form
        /// </summary>
        /// <param name="task">TaskItem object from form submission</param>
        /// <returns>Redirect to Index on success, or returns form with validation errors</returns>
        [HttpPost] // Only responds to POST requests
        [ValidateAntiForgeryToken] // Prevents CSRF attacks
        public IActionResult Create(TaskItem task)
        {
            // Check if model passes validation rules
            if (ModelState.IsValid)
            {
                // Add new task to database context
                _context.Tasks.Add(task);
                // Save changes to database
                _context.SaveChanges();
                // Redirect to task list page after successful creation
                return RedirectToAction(nameof(Index));
            }
            // If validation fails, return form with error messages
            return View(task);
        }

        /// <summary>
        /// GET: /Tasks/Edit/{id}
        /// Displays the edit form for a specific task
        /// </summary>
        /// <param name="id">ID of the task to edit</param>
        /// <returns>Edit form view with task data, or NotFound if task doesn't exist</returns>
        public IActionResult Edit(int id)
        {
            // Find task by ID in database
            var task = _context.Tasks.Find(id);
            // Return 404 if task not found
            if (task == null)
            {
                return NotFound();
            }
            // Return edit form pre-populated with task data
            return View(task);
        }

        /// <summary>
        /// POST: /Tasks/Edit
        /// Processes the submitted edit task form
        /// </summary>
        /// <param name="task">Updated TaskItem object from form submission</param>
        /// <returns>Redirect to Index on success, or returns form with validation errors</returns>
        [HttpPost] // Only responds to POST requests
        [ValidateAntiForgeryToken] // Prevents CSRF attacks
        public IActionResult Edit(TaskItem task)
        {
            // Check if model passes validation rules
            if (ModelState.IsValid)
            {
                // Mark task as modified in database context
                _context.Tasks.Update(task);
                // Save changes to database
                _context.SaveChanges();
                // Redirect to task list page after successful update
                return RedirectToAction(nameof(Index));
            }
            // If validation fails, return form with error messages
            return View(task);
        }

        /// <summary>
        /// GET: /Tasks/ConfirmDelete/{id}
        /// Displays confirmation page before deleting a task
        /// Shows "Are you sure you want to delete?" message
        /// </summary>
        /// <param name="id">ID of the task to delete</param>
        /// <returns>Confirmation view with task details, or NotFound if task doesn't exist</returns>
        public IActionResult ConfirmDelete(int id)
        {
            // Find task by ID in database
            var task = _context.Tasks.Find(id);
            // Return 404 if task not found
            if (task == null)
            {
                return NotFound();
            }
            // Return confirmation view with task details
            return View(task);
        }

        /// <summary>
        /// POST: /Tasks/Delete
        /// Actually deletes the task from the database
        /// </summary>
        /// <param name="id">ID of the task to delete</param>
        /// <returns>Redirect to Index page</returns>
        [HttpPost] // Only responds to POST requests
        [ValidateAntiForgeryToken] // Prevents CSRF attacks
        public IActionResult Delete(int id)
        {
            // Find task by ID in database
            var task = _context.Tasks.Find(id);
            // Only proceed if task exists
            if (task != null)
            {
                // Remove task from database context
                _context.Tasks.Remove(task);
                // Save changes to database
                _context.SaveChanges();
            }
            // Always redirect to task list page (even if task wasn't found)
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// POST: /Tasks/UpdateStatus
        /// Updates the status of a task via AJAX request
        /// </summary>
        /// <param name="request">Request containing task ID and new status</param>
        /// <returns>JSON response indicating success or failure</returns>
        [HttpPost]
        public IActionResult UpdateStatus([FromBody] UpdateStatusRequest request)
        {
            try
            {
                // Find task by ID in database
                var task = _context.Tasks.Find(request.TaskId);

                if (task == null)
                {
                    return NotFound(new { message = "Task not found" });
                }

                // Parse the status string to enum
                if (Enum.TryParse<Models.TaskStatus>(request.Status, out Models.TaskStatus newStatus))
                {
                    // Update the task status
                    task.Status = newStatus;

                    // Save changes to database
                    _context.SaveChanges();

                    return Ok(new
                    {
                        message = "Task status updated successfully",
                        taskId = task.Id,
                        newStatus = newStatus.ToString()
                    });
                }
                else
                {
                    return BadRequest(new { message = "Invalid status value" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the task status", error = ex.Message });
            }
        }
    }
}

/// <summary>
/// Request model for updating task status
/// </summary>
public class UpdateStatusRequest
{
    public int TaskId { get; set; }
    public required string Status { get; set; }
}