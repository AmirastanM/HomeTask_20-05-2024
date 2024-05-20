namespace FiorelloBack.Models
{
    public class Blog : BaseEntity
    {
        public string Titel { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
