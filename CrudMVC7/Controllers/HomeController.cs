using CrudMVC7.Data;
using CrudMVC7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CrudMVC7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // _context is the access to the database
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Here I send all the contact table data
            return View(await _context.Contacts.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                // Esta es la modificacion de vacunas
                var vacunas = _context.Immunizations.ToList();

                foreach (var vacuna in vacunas)
                {
                    var immunization = new ImmunizationPatient
                    {
                        ContactId = contact.ContactId,
                        ImmunizationId = vacuna.ImmunizationId,
                        ScheduledDate = contact.BirthDate.AddMonths(vacuna.Period),
                        State = "pendiente"
                    };

                    _context.ImmunizationPatients.Add(immunization);
                }

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
            var contact = _context.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Update(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var inmunizationsInfo = _context.Contacts.Where(c => c.ContactId == id).Include(p => p.ImmunizationPatients).ThenInclude(ip => ip.Immunization).ToList();
            return View(inmunizationsInfo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
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