using FiorelloBack.Models;
using Microsoft.EntityFrameworkCore;

namespace FiorelloBack.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDeleted);


            modelBuilder.Entity<Blog>().HasData(
                new Blog
                {
                    Id = 1,
                    Titel = "Titel1",
                    Description = "Reshadin blogu",
                    Image = "blog-feature-img-1.jpg",
                    CreatedDate = DateTime.Now

                },
                new Blog
                {
                    Id = 2,
                    Titel = "Titel2",
                    Description = "Ilgarin blogu",
                    Image = "blog-feature-img-3.jpg",
                    CreatedDate = DateTime.Now

                },
                new Blog
                {
                    Id = 3,
                    Titel = "Titel3",
                    Description = "Hacixanin blogu",
                    Image = "blog-feature-img-4.jpg",
                    CreatedDate = DateTime.Now

                });
       
        }
    }
}
