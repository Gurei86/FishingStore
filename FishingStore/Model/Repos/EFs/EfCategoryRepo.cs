using FishingStore.Model.Entities;
using FishingStore.Model.Repos.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FishingStore.Model.Repos.EFs
{
    public class EfCategoryRepo:ICategoryRepo
    {
        private readonly FishingStoreContext context;
        public EfCategoryRepo(FishingStoreContext context)
        {
            this.context = context;
        }

        public void SaveCategory(Category category)
        {
            if (category.Id == default(int))
            { context.Entry(category).State = EntityState.Added; }
            else
            { context.Entry(category).State = EntityState.Modified; }
            context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            context.Categories.Remove(new Category() { Id = id });
            context.SaveChanges();
        }

        public IQueryable<Category> GetCategories()
        {
            return context.Categories;
        }

        public Category GetCategoryById(int id)
        {
            return context.Categories.FirstOrDefault(x => x.Id == id);
        }

        public Category GetCategoryByName(string name)
        {
            return context.Categories.FirstOrDefault(x => x.Name == name);
        }

        
    }
}
