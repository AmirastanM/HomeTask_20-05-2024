using FiorelloBack.Models;

namespace FiorelloBack.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<Slider>> GetAllAsync();
        Task<SliderInfo> GetSliderInfoAsync();

    }
}
