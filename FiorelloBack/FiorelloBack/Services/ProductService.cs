using FiorelloBack.Data;
using FiorelloBack.Models;
using FiorelloBack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloBack.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _contex;
        public ProductService(AppDbContext context)
        {
            _contex = context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _contex.Products.Include(m=>m.ProductImages).ToListAsync();   
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _contex.Products.Where(m=>m.Id == id)
                                         .Include(m=>m.Category)
                                         .Include(m=>m.ProductImages)
                                         .FirstOrDefaultAsync();
        }
    }
}
