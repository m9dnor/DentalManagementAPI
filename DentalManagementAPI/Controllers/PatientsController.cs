using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DentalManagementAPI.Data;
using DentalManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DentalManagementAPI.Controllers
{
    [Route("[controller]")]
    public class PatientsController : Controller
    {
        private readonly DentalManagementContext _context;

        public PatientsController(DentalManagementContext context)
        {
            _context = context;
        }

        // GET: Patients
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, string appointmentDate, string sortOrder)
        {
            var patients = _context.Patients.AsQueryable();

            // 🔍 Search by Name or Email
            if (!string.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(p => p.PatientName.Contains(searchString) || p.Email.Contains(searchString));
            }

            // 📅 Filter by Appointment Date
            if (!string.IsNullOrEmpty(appointmentDate))
            {
                if (DateTime.TryParse(appointmentDate, out DateTime date))
                {
                    patients = patients.Where(p => p.AppointmentDate.Date == date);
                }
            }

            // 🔄 Sorting Options
            switch (sortOrder)
            {
                case "name_asc":
                    patients = patients.OrderBy(p => p.PatientName);
                    break;
                case "name_desc":
                    patients = patients.OrderByDescending(p => p.PatientName);
                    break;
                case "date_asc":
                    patients = patients.OrderBy(p => p.AppointmentDate);
                    break;
                case "date_desc":
                    patients = patients.OrderByDescending(p => p.AppointmentDate);
                    break;
                default:
                    patients = patients.OrderBy(p => p.PatientName);
                    break;
            }

            return View(await patients.ToListAsync());
        }

        // GET: Patients/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient); // Returns the details view of a specific patient
        }

        // GET: Patients/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(); // Returns the view for creating a new patient
        }

        // POST: Patients/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirects to the patient list
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient); // Returns the edit view for a specific patient
        }

        // POST: Patients/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Patient patient)
        {
            if (id != patient.PatientID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index)); // Redirects to the patient list
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient); // Returns the delete confirmation view
        }

        // POST: Patients/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index)); // Redirects to the patient list
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.PatientID == id);
        }
    }
}
