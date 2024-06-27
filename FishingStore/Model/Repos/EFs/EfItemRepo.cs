using FishingStore.Model.Entities;
using FishingStore.Model.Parameters;
using FishingStore.Model.Repos.Abstract;
using Microsoft.EntityFrameworkCore;


namespace FishingStore.Model.Repos.EFs
{
    public class EfItemRepo:IItemRepo
    {
        private readonly FishingStoreContext context;
        public EfItemRepo(FishingStoreContext context)
        {
            this.context = context;
        }

        public void DeleteItem(int id)
        {
            context.Items.Remove(new Item() { Id = id });
            context.SaveChanges();
        }

        public Item GetItemByCategory(string category)
        {
            int id = context.Categories.FirstOrDefault(x => x.Name == category).Id;
            return context.Items.FirstOrDefault(x => x.CategoryId == id);
        }

        public Item GetItemById(int id)
        {
            return context.Items.FirstOrDefault(x => x.Id == id);
        }

        public Item GetItemByName(string name)
        {
            return context.Items.FirstOrDefault(x => x.Name == name);
        }

        public Task<List<Item>> GetItems(ItemsParameters itemsParameters)
        {
            if (context.Items == null)
                return null;

            IQueryable<Item> list = context.Items.Skip((itemsParameters.PageNumber
                - 1) * itemsParameters.PageSize).Take(itemsParameters.PageSize);

            if (!string.IsNullOrEmpty(itemsParameters.nameFilter))
            {
                list = list.Where(x => x.Name.Contains(itemsParameters.nameFilter));
            }
            if (itemsParameters.minPriceFilter.HasValue && itemsParameters.minPriceFilter > 0)
            {
                list = list.Where(x => x.Price >= itemsParameters.minPriceFilter);
            }
            if (itemsParameters.maxPriceFilter.HasValue && itemsParameters.maxPriceFilter > 0)
            {
                list = list.Where(x => x.Price <= itemsParameters.maxPriceFilter);
            }

            if (itemsParameters.categoryFilter!=0)
            {
                list = list.Where(x => x.CategoryId == itemsParameters.categoryFilter);
                
            }

            else
                list = list.Include(x => x.Category);

            if (itemsParameters.isAsc)
            {
                return list.OrderBy(x => EF.Property<object>(x, itemsParameters.sortField.ToString()))
                    .ToListAsync();
            }
            return list.OrderByDescending(x => EF.Property<object>(x, itemsParameters.sortField.ToString()))
                    .ToListAsync();
        }
        public int CountOfItems()
        {
            return context.Items.Count();
        }

        public void SaveItem(Item item)
        {
            if (item.Id == default(int)) 
            { context.Entry(item).State = EntityState.Added; }
            else 
            { context.Entry(item).State = EntityState.Modified; }
            context.SaveChanges();
        }
    }
}
