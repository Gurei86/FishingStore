using FishingStore.Model.Entities;

namespace FishingStore.Model.Repos.Abstract
{
    public interface ICategoryRepo
    {
        IQueryable<Category> GetCategories();
        Category GetCategoryById(int id);
        Category GetCategoryByName(string name);
        void SaveCategory(Category category);
        void DeleteCategory(int id);
        
    }
}
