using FishingStore.Model.Repos.Abstract;

namespace FishingStore.Model
{
    public class DataManager
    {
        public IItemRepo Items { get; set; }
        public IUserRepo Users { get; set; }
        public IRoleRepo Roles { get; set; }
        public ICategoryRepo Categories { get; set; }
        public ICartRepo Carts { get; set; }
        public DataManager(IItemRepo itemRepos, IUserRepo userRepos, IRoleRepo roleRepos, ICategoryRepo categoryRepos,ICartRepo cartRepo)
        {
            Items = itemRepos;
            Users = userRepos;
            Roles = roleRepos;
            Categories = categoryRepos;
            Carts = cartRepo;
        }
    }
}
