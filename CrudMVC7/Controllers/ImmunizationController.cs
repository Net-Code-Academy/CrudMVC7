using CrudMVC7.Data;
using CrudMVC7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CrudMVC7.Controllers
{
    public class ImmunizationController : Controller
    {
        private readonly ILogger<ImmunizationController> _logger;

        // _context is the access to the database
        private readonly ApplicationDbContext _context;

        public ImmunizationController(ILogger<ImmunizationController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Here I send all the contact table data
            return View(await _context.Immunizations.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var immunizationPatien = _context.ImmunizationPatients.Where(ip =>ip.Id == id).Include(ip => ip.Contact);

            if (immunizationPatien == null)
            {
                return NotFound();
            }

            return View(immunizationPatien);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id)
        {
            var immunization = _context.ImmunizationPatients.Where(i => i.Id == id).FirstOrDefault();
            if (immunization != null)
            {
                immunization.ApplicationDate = DateTime.Now;
                immunization.State = "vacunado";
                _context.ImmunizationPatients.Update(immunization);
                await _context.SaveChangesAsync();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}