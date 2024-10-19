using E_Commerce.DataAccess;
using E_Commerce.Entites.Models;
using E_commerce.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DataAccess.Implementation
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var CategoryInDb = _context.categories.FirstOrDefault(x => x.Id == category.Id);
            if (CategoryInDb != null)
            {
                CategoryInDb.Name = category.Name;
                CategoryInDb.Description = category.Description;
                CategoryInDb.CreatedTime = DateTime.Now;
            }
        }
    }
}
