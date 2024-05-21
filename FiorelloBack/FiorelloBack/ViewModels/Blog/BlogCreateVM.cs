using System.ComponentModel.DataAnnotations;

namespace FiorelloBack.ViewModels.Blog
{
    public class BLogCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

    }
}
