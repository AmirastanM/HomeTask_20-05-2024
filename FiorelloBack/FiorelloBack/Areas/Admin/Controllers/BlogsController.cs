using FiorelloBack.Data;
using FiorelloBack.Extentions;
using FiorelloBack.Models;
using FiorelloBack.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorelloBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogsController(AppDbContext context,
                               IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blogs = await _context.Blogs.Select(m => new BlogVM { Titel = m.Titel, Image = m.Image, Description = m.Description, CreatedDate = m.CreatedDate.ToString() })
                                            .ToListAsync();
            return View(blogs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLogCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Input can't be accept");
                return View();
            }

            if (!(request.Image.CheckFileSize(200)))
            {
                ModelState.AddModelError("Image", "Image size must be max 200 KB");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;

            string path = Path.Combine(_env.WebRootPath, "img", fileName);

            await request.Image.SaveFileToLocalAsync(path);

            var newBlog = new Blog
            {
                Titel = request.Title,
                Description = request.Description,
                Image = fileName
            };

            await _context.Blogs.AddAsync(newBlog);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var deletedBlog = await _context.Blogs.FindAsync(id);

            if (deletedBlog == null)
            {
                return NotFound();
            }


            if (!string.IsNullOrEmpty(deletedBlog.Image))
            {
                string path = _env.GenerateFilePath("img", deletedBlog.Image);
                path.DeleteFileFromLocal();
            }

            _context.Blogs.Remove(deletedBlog);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var blogs = await _context.Blogs.FindAsync(id);

            if (blogs == null)
            {
                return NotFound();
            }

            
            var viewModel = new BlogEditVM
            {
                Title = blogs.Titel,
                Description = blogs.Description,
                Image = blogs.Image
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BlogEditVM request)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var blogs = await _context.Blogs.FindAsync(id);

            if (blogs == null)
            {
                return NotFound();
            }

            if (request.NewImage == null)
            {
                ModelState.AddModelError("NewImage", "Please upload an image");
                return View(request);
            }

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Invalid file type");
                return View(request);
            }

            if (!request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200 KB");
                return View(request);
            }

            
            string oldImagePath = _env.GenerateFilePath("img", blogs.Image);
            oldImagePath.DeleteFileFromLocal();

            
            string newFileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
            string newImagePath = _env.GenerateFilePath("img", newFileName);
            await request.NewImage.SaveFileToLocalAsync(newImagePath);

            
            blogs.Titel = request.Title;
            blogs.Description = request.Description;
            blogs.Image = newFileName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
