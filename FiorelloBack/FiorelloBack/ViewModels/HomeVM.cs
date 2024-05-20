using FiorelloBack.Models;
using FiorelloBack.ViewModels.Blog;

namespace FiorelloBack.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SliderInfo SliderInfo { get; set; }
        public IEnumerable<BlogVM> Blogs { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
