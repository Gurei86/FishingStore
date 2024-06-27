using FishingStore.Model.Entities;
using FishingStore.Model.Repos.EFs;

namespace FishingStore.Model.Repos.Abstract
{
    public interface ICartRepo
    {
        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public void AddItem(Item item);
        public List<CartItem> GetItems();
        public void DeleteFrom(Item item);
        public void DeleteAll();
        public static ICartRepo GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<FishingStoreContext>();
            string cartId = session.GetString("cartId") ?? Guid.NewGuid().ToString();

            session.SetString("cartId", cartId);
            return new EfCartRepo(context) { CartId = cartId };
        }
    }
}
