﻿using LIBRARY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.Repositories
{
    public class BookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public IEnumerable<Book> GetBookByName(string title)
        {
            return _context.Books
                    .Where(e => e.BTitle == title)
                   .ToList();
        }

        public void InsertBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBookByName(string title)
        {
            var book = _context.Books.FirstOrDefault(c => c.BTitle == title);
            if (book != null)
            {
                _context.Books.Update(book);
                _context.SaveChanges();
            }
        }

        public void DeleteBookById(int bid)
        {
            var book = _context.Books.Find(bid);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public double GetTotalPrice()
        {
            return _context.Books.Sum(b => b.Price );
        }

        public double getMaxPrice()
        {
            return _context.Books.Max(b => b.Price);
        }

        public int getTotalBorrowedBooks()
        {
            return _context.Books.Sum(b => b.BorrowedCopies);
        }

        public int getTotalBooksPerCategoryName( string Catg_name )
        {
            return _context.Books
                            .Where(b => b.Category.CName == Catg_name)
                            .Count();
        }
    }
}