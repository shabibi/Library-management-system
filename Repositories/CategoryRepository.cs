using LIBRARY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.Repositories
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetAllCategory()
        {
            return _context.Categories.ToList();
        }

        public IEnumerable<Category> GetCategoryByName(string CategoryName)
        {
            return _context.Categories
                    .Where(e => e.CName == CategoryName)
                   .ToList();
        }

        public void InsertCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategoryByName(string Name)
        {
            var category =_context.Categories.FirstOrDefault(c => c.CName == Name);
            if (category != null)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
            }
        }

        public void DeleteCategoryById(int cid)
        {
            var category = _context.Categories.Find(cid);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

    }
}
