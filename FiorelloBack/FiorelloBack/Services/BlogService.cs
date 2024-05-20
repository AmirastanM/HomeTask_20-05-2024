using FiorelloBack.Data;
using FiorelloBack.Models;
using FiorelloBack.Services.Interfaces;
using FiorelloBack.ViewModels.Blog;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FiorelloBack.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        public BlogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogVM>> GetAllAsync(int? take = null)
        {
            IEnumerable<Blog> blogs;



            if (take is null)
            {
               blogs = await _context.Blogs.ToListAsync();
            }
            else
            {
               blogs = await _context.Blogs.Take((int)take).ToListAsync();
            }

            return blogs.Select(m => new BlogVM { Titel = m.Titel, Description = m.Description, Image = m.Image, CreatedDate = m.CreatedDate.ToString("MM.dd.yyyy") });
        

        }
    }
}
