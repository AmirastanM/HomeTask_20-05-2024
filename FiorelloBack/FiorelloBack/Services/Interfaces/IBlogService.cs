using FiorelloBack.Models;
using FiorelloBack.ViewModels.Blog;

namespace FiorelloBack.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogVM>> GetAllAsync(int? take = null);
    }
}
