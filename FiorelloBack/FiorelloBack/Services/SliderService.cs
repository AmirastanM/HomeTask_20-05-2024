using FiorelloBack.Data;
using FiorelloBack.Models;
using FiorelloBack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloBack.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        public SliderService(AppDbContext context)
        {
            _context = context;
            
        }

        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            return await _context.Sliders.Where(m => !m.SoftDeleted).ToListAsync();
        }

        public async Task<SliderInfo> GetSliderInfoAsync()
        {
            return await _context.SliderInfos.Where(m=>!m.SoftDeleted).FirstOrDefaultAsync();
        }
    }
}
