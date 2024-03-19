using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Controllers.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    
    public class LeadController : Controller
    {
        private readonly UserContext _context;

        public LeadController(UserContext context)
        {
            _context = context;
        }

        // GET: Lead
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            if (_context.NewUsers == null)
            {
                // Log the issue or handle it as appropriate
                // You can redirect to an error page or return a view with an error message
                // Example: return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return View(await _context.NewUsers.ToListAsync());
        }

        // GET: Lead/Details/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NewUsers == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.NewUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (salesLeadEntity == null)
            {
                return NotFound();
            }

            return View(salesLeadEntity);
        }

        // GET: Lead/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Lead/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseName,CourseDuration,CourseFees")] SalesLeadEntity salesLeadEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesLeadEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(salesLeadEntity);
        }

        // GET: Lead/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NewUsers == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.NewUsers.FindAsync(id);
            if (salesLeadEntity == null)
            {
                return NotFound();
            }

            return View(salesLeadEntity);
        }
        [Authorize(Roles = "Admin")]
        // POST: Lead/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseName,CourseDuration,CourseFees")] SalesLeadEntity salesLeadEntity)
        {
            if (id != salesLeadEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesLeadEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesLeadEntityExists(salesLeadEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(salesLeadEntity);
        }

        // GET: Lead/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NewUsers == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.NewUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (salesLeadEntity == null)
            {
                return NotFound();
            }

            return View(salesLeadEntity);
        }
        [Authorize(Roles = "Admin")]
        // POST: Lead/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NewUsers == null)
            {
                // Log the issue or handle it as appropriate
                // You can redirect to an error page or return a view with an error message
                // Example: return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var salesLeadEntity = await _context.NewUsers.FindAsync(id);
            if (salesLeadEntity != null)
            {
                _context.NewUsers.Remove(salesLeadEntity);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SalesLeadEntityExists(int id)
        {
            return (_context.NewUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        // POST: Lead/Select
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Select(int selectedCourseId)
        {
            // Handle the selected course ID here
            // You can use the selectedCourseId to perform any additional actions
            // For example, display a message or add the selected course to a list

            TempData["Message"] = "Course Added Successfully"; // Use TempData to pass a message to the next request

            return RedirectToAction(nameof(Index));
        }

    }

}
