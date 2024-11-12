using LIBRARY.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.Repositories
{
    public class BorrowRepository
    {
        private readonly ApplicationDbContext _context;

        public BorrowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Borrow> GetAll()
        {
            return _context.Borrows.ToList();
        }

        public IEnumerable<Borrow> GetBorrowBookByName(string BookName)
        {
            return _context.Borrows
                    .Include(e => e.Book)
                    .Where(e => e.Book.BTitle == BookName)
                    .ToList();
        }

        public void Insert(Borrow borrow)
        {
            _context.Borrows.Add(borrow);
            _context.SaveChanges();
        }

        public void UpdateBorrowingByBookName(string Name)
        {
            var borrow = _context.Borrows.
                        Include(e => e.Book)
                        .FirstOrDefault(c => c.Book.BTitle == Name);
            if (borrow != null)
            {
                _context.Borrows.Update(borrow);
                _context.SaveChanges();
            }
        }

        public void DeleteBorrowingById(int bid,int uid)
        {
            var borrow = _context.Borrows.Find(bid,uid);
            if (borrow != null)
            {
                _context.Borrows.Remove(borrow);
                _context.SaveChanges();
            }
        }


    }
}
