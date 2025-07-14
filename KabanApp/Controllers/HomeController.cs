using Microsoft.AspNetCore.Mvc;
using KabanApp.Data;
using KabanApp.Models;

namespace KabanApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tasks = _context.Tasks.ToList();
            return View(tasks);
        }
    }
}
