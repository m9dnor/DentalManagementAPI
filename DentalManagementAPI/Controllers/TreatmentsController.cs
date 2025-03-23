using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DentalManagementAPI.Data;
using DentalManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DentalManagementAPI.Controllers
{
    [Route("[controller]")]
    public class TreatmentsController : Controller
    {
        private readonly DentalManagementContext _context;

        public TreatmentsController(DentalManagementContext context)
        {
            _context = context;
        }

        // GET: Treatments
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var treatments = await _context.Treatments.ToListAsync();
            return View(treatments); // Returns the list view of treatments
        }

        // GET: Treatments/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var treatment = await _context.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return NotFound();
            }

            return View(treatment); // Returns the details view of a specific treatment
        }

        // GET: Treatments/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(); // Returns the view for creating a new treatment
        }

        // POST: Treatments/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treatment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirects to the treatment list
            }
            return View(treatment);
        }

        // GET: Treatments/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var treatment = await _context.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return NotFound();
            }
            return View(treatment); // Returns the edit view for a specific treatment
        }

        // POST: Treatments/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Treatment treatment)
        {
            if (id != treatment.TreatmentID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treatment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreatmentExists(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index)); // Redirects to the treatment list
            }
            return View(treatment);
        }

        // GET: treatments/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var treatment = await _context.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return NotFound();
            }

            return View(treatment); // Returns the delete confirmation view
        }

        // POST: Treatments/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var treatment = await _context.Treatments.FindAsync(id);
            if (treatment != null)
            {
                _context.Treatments.Remove(treatment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index)); // Redirects to the treatment list
        }

        private bool TreatmentExists(int id)
        {
            return _context.Treatments.Any(e => e.TreatmentID == id);
        }
    }
}
