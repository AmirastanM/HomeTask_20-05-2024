using System.ComponentModel.DataAnnotations;

namespace FiorelloBack.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
