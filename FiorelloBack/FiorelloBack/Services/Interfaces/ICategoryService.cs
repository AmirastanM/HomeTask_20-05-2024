using FiorelloBack.Models;
using FiorelloBack.ViewModels.Categories;

namespace FiorelloBack.Services.Interfaces
{
    public interface ICategoryService
    {

        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<CategoryProductVM>> GetAllWhithProductCountAsync();
        Task<Category> GetByIdAsync(int id);
        Task<bool> ExistAsync(string name);
        Task CreateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<IEnumerable<CategoryProductVM>> GetCategoryDetailWhithProductsAsync();




    }
}
