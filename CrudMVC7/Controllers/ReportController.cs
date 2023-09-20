using CrudMVC7.Data;
using CrudMVC7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Previewer;
using System.Diagnostics;
using System.Globalization;

namespace CrudMVC7.Controllers
{
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;

        // _context is the access to the database
        private readonly ApplicationDbContext _context;

        public ReportController(ILogger<ReportController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index2()
        {
            // Here I send all the contact table data
            return View(await _context.Immunizations.ToListAsync());
        }

        [HttpPost]
        public string Generate(DateTime desde, DateTime hasta, string estado)
        {
            var vacunas = _context.ImmunizationPatients.Where(v => v.ScheduledDate >= desde & v.ScheduledDate <= hasta & v.State == estado).Include(v => v.Contact).Include(v => v.Immunization).ToList();

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(15);
                    // page content
                    //page.Header().Height(50).Background(Colors.Cyan.Medium);
                    page.Header().ShowOnce().Row(row =>
                    {
                        byte[] imageData = System.IO.File.ReadAllBytes("wwwroot/images/CSLobo.png");

                        row.ConstantItem(70).Image(imageData);
                        row.ConstantItem(300).Column(col =>
                        {
                            col.Item().Text("");
                            col.Item().AlignCenter().Text("Centro de Salud Lobo - Tahuantinsuyo").Bold().FontSize(14).FontFamily("Times New Roman");
                            col.Item().AlignCenter().Text("Jirón Los Rosales N° 337").FontSize(10).FontFamily("Times New Roman");
                            col.Item().AlignCenter().Text("Kimbiri - Cuzco").FontSize(10).FontFamily("Times New Roman"); ;
                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("");
                            col.Item().Border(1).BorderColor("3FE8E3").Background("3FE8E3").AlignCenter().Text("REPORTE 001").FontColor(Colors.White).FontFamily("Times New Roman"); ;
                            col.Item().Border(1).BorderColor("3FE8E3").AlignCenter().Text("Desde:       " + desde.ToString("dd/MM/yyyy")).FontSize(10).FontFamily("Times New Roman");
                            col.Item().Border(1).BorderColor("3FE8E3").AlignCenter().Text("Hasta:       " + hasta.ToString("dd/MM/yyyy")).FontSize(10).FontFamily("Times New Roman");

                        });


                    });

                    page.Content().PaddingVertical(15).Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text("ESTE ES UN REPORTE DE LAS VACUNAS Y CONTROLES ADMINISTRADOS POR EL CENTRO DE SALUD LOBO - TAHUANTINSUYO EN EL DISTRITO DE KIMBIRI - CUZCO")
                            .Bold().FontFamily("Times New Roman");
                            col.Item().BorderHorizontal(1).BorderColor("3FE8E3").Height(2);

                            col.Item().PaddingVertical(10).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(4);
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(2);

                                });

                                table.Header(header =>
                                {
                                    header.Cell().Background("#3FE8E3").Padding(2).Text("N°").FontColor(Colors.White).FontFamily("Times New Roman");
                                    header.Cell().Background("#3FE8E3").Padding(2).Text("Paciente").FontColor(Colors.White).FontFamily("Times New Roman");
                                    header.Cell().Background("#3FE8E3").Padding(2).Text("Dirección").FontColor(Colors.White).FontFamily("Times New Roman");
                                    header.Cell().Background("#3FE8E3").Padding(2).Text("Vacuna").FontColor(Colors.White).FontFamily("Times New Roman");
                                    header.Cell().Background("#3FE8E3").Padding(2).Text("Fecha Prevista").FontColor(Colors.White).FontFamily("Times New Roman");
                                    header.Cell().Background("#3FE8E3").Padding(2).Text("Estado").FontColor(Colors.White).FontFamily("Times New Roman");
                                });

                                int cont = 1;
                                foreach (var patient in vacunas)
                                {
                                    string nomCompleto = patient.Contact.LastName + " " + patient.Contact.MotherLastName + ", " + textInfo.ToTitleCase(patient.Contact.Name.ToLower());
                                    string vacuna = patient.Immunization.Name + " - " + patient.Immunization.Description;
                                    
                                    table.Cell().BorderBottom(1).BorderColor("3FE8E3").Padding(2).Text(cont).FontSize(10).FontFamily("Times New Roman");
                                    table.Cell().BorderBottom(1).BorderColor("3FE8E3").Padding(2).Text(nomCompleto).FontSize(10).FontFamily("Times New Roman");
                                    table.Cell().BorderBottom(1).BorderColor("3FE8E3").Padding(2).Text(textInfo.ToTitleCase(patient.Contact.Direccion.ToLower())).FontSize(10).FontFamily("Times New Roman");
                                    table.Cell().BorderBottom(1).BorderColor("3FE8E3").Padding(2).Text(vacuna).FontSize(10).FontFamily("Times New Roman");
                                    table.Cell().BorderBottom(1).BorderColor("3FE8E3").Padding(2).Text(patient.ScheduledDate.ToString("dd/MM/yyyy")).FontSize(10).FontFamily("Times New Roman");
                                    table.Cell().BorderBottom(1).BorderColor("3FE8E3").Padding(2).Text(patient.State).FontSize(10).FontFamily("Times New Roman");

                                    cont++;
                                }

                            });
                        });


                    });

                    page.Footer().AlignCenter().Text(numertation =>
                    {
                        numertation.Span("Página ").FontSize(11).FontFamily("Times New Roman");
                        numertation.CurrentPageNumber().FontSize(11).FontFamily("Times New Roman");
                        numertation.Span(" de ").FontSize(11).FontFamily("Times New Roman");
                        numertation.TotalPages().FontSize(11).FontFamily("Times New Roman");

                    });


                });
            }).ShowInPreviewer();

            return (desde + " - " + hasta + " - "+ estado);
        }

        public IActionResult Index()
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