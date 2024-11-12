using LIBRARY.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            options.UseSqlServer(" Data Source=(local); Initial Catalog= Library_System ; Integrated Security=true; TrustServerCertificate=True ");
        }

        public DbSet <Admin> Admins { get; set; }
        public DbSet <User> Users { get; set; }
        public DbSet <Book> Books { get; set; }
        public DbSet <Category> Categories { get; set; }
        public DbSet <Borrow> Borrows { get; set; }


    }
}
