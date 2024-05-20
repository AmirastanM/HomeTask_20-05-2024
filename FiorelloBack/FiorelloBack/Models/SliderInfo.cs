using System.ComponentModel.DataAnnotations;

namespace FiorelloBack.Models
{
    public class SliderInfo : BaseEntity
    {
        [Required]
        [StringLength(200)] 
        public string Titel { get; set; }
        [Required]
        [StringLength(400)]
        public string Description { get; set; }
        public string Image { get; set; }

    }
}
