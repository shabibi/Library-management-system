using LIBRARY.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.Include(u=>u.Borrows).ToList();
        }

        public User GetUserByName(string UserName)
        {
            return _context.Users
                    .FirstOrDefault(e => e.UName == UserName);
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUserByName(string Name)
        {
            var user = _context.Users.FirstOrDefault(c => c.UName == Name);
            if (user != null)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }

        public void DeleteUserById(int uid)
        {
            var user = _context.Users.Find(uid);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
        public int countByGender(Gender gender)
        {
            return _context.Users.Count(u => u.gender == gender);
        }

        public int borrowedCopies(int uid)
        {
            int notReturnedBook = 0;
            var user = _context.Users.Find( uid);
            if (user != null)
            {
                notReturnedBook = user.Borrows.Count(b => b.IsReturned != true);
            }
            return notReturnedBook;
        }


    }
}
