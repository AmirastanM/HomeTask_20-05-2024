namespace FiorelloBack.ViewModels.Blog
{
    public class BlogEditVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
       
    }
}
