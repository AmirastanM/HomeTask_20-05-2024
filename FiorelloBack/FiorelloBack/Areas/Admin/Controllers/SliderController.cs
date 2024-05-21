using FiorelloBack.Data;
using FiorelloBack.Extentions;
using FiorelloBack.ViewModels.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FiorelloBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, 
                               IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sliders = await _context.Sliders.Select(m => new SliderVM {Id = m.Id, Image = m.Image })
                                                .ToListAsync();
            return View(sliders);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
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

            await _context.Sliders.AddAsync(new Models.Slider { Image = fileName });
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var deletedSlider = await _context.Sliders.FindAsync(id);

            if (deletedSlider is null)  return NotFound();

            string path = _env.GenerateFilePath("img", deletedSlider.Image);

            path.DeleteFileFromLocal();

            _context.Sliders.Remove(deletedSlider);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit (int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _context.Sliders.FindAsync(id);

            if (slider is null) return NotFound();


            return View(new SliderEditVM { Image = slider.Image});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {
            if (id is null) return BadRequest();

            var slider = await _context.Sliders.FindAsync(id);

            if (slider is null) return NotFound();

            if (request.NewImage is null) return RedirectToAction(nameof(Index));

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Input can't be accept");
                request.Image = slider.Image;
                return View(request);
            }

            if (!(request.NewImage.CheckFileSize(200)))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200 KB");
                request.Image = slider.Image;
                return View(request);
            }

            string oldPath = _env.GenerateFilePath("img", slider.Image);

            oldPath.DeleteFileFromLocal();

            string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

            string newPath = _env.GenerateFilePath("img", fileName);

            await request.NewImage.SaveFileToLocalAsync(newPath);

            slider.Image = fileName;

            await _context.SaveChangesAsync();

           return RedirectToAction(nameof(Index));
        }                   
        
    }
}
