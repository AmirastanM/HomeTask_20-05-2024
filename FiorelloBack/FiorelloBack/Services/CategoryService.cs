using FiorelloBack.Data;
using FiorelloBack.Models;
using FiorelloBack.Services.Interfaces;
using FiorelloBack.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;

namespace FiorelloBack.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Categories.AnyAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<CategoryProductVM>> GetAllWhithProductCountAsync()
        {
            IEnumerable<Category> categories = await _context.Categories.Include(m => m.Products)
                                                                        .OrderByDescending(m => m.Id)                                                                        
                                                                        .ToListAsync();

            return categories.Select(m => new CategoryProductVM
            {
                Id = m.Id,
                CategoryName = m.Name,
                CreateDate = m.CreatedDate.ToString("MM.dd.yyyy"),
                ProductCount = m.Products.Count
            });
        }


        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<CategoryProductVM>> GetCategoryDetailWhithProductsAsync()
        {
            var categories = await _context.Categories
                                     .Include(c => c.Products)
                                         .ThenInclude(p => p.ProductImages)
                                     .ToListAsync();

            return categories.SelectMany(category => category.Products.Select(product => new CategoryProductVM
            {
                Id = category.Id,
                CategoryName = category.Name,
                ProductName = product.Name             
            }));
        }

    }
}
