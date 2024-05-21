using System.ComponentModel.DataAnnotations;

namespace FiorelloBack.ViewModels.Categories
{
    public class CategoryEditVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
