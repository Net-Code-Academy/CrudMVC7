using CrudMVC7.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace CrudMVC7.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        // Here are all the models

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Immunization> Immunizations { get; set; }

        public DbSet<ImmunizationPatient> ImmunizationPatients{ get; set; }

    }
}
