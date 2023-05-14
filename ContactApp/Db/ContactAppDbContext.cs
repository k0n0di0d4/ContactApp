using ContactApp.Models;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace ContactApp.Db
{
    // database context which is used to configure database
    public class ContactAppDbContext : DbContext
    {
        public ContactAppDbContext(DbContextOptions<ContactAppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Contactsdb;Trusted_Connection=true;TrustServerCertificate=true;");
        }


        public DbSet<User> Users { get; set; }
    }
}
