using FishingStore.Model.Entities;
using FishingStore.Model.Parameters;

namespace FishingStore.Model.Repos.Abstract
{
    public interface IItemRepo
    {
        Task<List<Item>> GetItems(ItemsParameters itemsParameters);
        Item GetItemById(int id);
        Item GetItemByName(string name);
        Item GetItemByCategory(string category);
        void SaveItem(Item item);
        void DeleteItem(int id);
        public int CountOfItems();


    }
}
