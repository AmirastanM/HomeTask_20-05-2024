using System.ComponentModel.DataAnnotations;

namespace FiorelloBack.ViewModels.Categories
{
    public class CategoryCreateVM
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }    
    }
}
