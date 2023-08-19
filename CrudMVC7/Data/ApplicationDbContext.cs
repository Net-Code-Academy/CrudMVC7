using CrudMVC7.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudMVC7.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        // Here are all the models

        DbSet<Contact> Contacts { get; set; }

    }
}
