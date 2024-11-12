using LIBRARY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.Repositories
{
    public class AdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Admin> GetAllAdmin()
        {
            return _context.Admins.ToList();
        }

        public IEnumerable<Admin> GetAdminByName(string admin)
        {
            return _context.Admins
                    .Where(e => e.AName == admin)
                   .ToList();
        }

        public void InsertAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }

        public void UpdateAdminByName(string Name)
        {
            var admin = _context.Admins.FirstOrDefault(c => c.AName == Name);
            if (admin != null)
            {
                _context.Admins.Update(admin);
                _context.SaveChanges();
            }
        }

        public void DeleteAdminById(int aid)
        {
            var admin = _context.Admins.Find(aid);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                _context.SaveChanges();
            }
        }

    }
}
