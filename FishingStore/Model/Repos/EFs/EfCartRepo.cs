using FishingStore.Migrations;
using FishingStore.Model.Entities;
using FishingStore.Model.Repos.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FishingStore.Model.Repos.EFs
{
    public class EfCartRepo:ICartRepo
    {
        private readonly FishingStoreContext context;
        public EfCartRepo(FishingStoreContext context)
        {
            this.context = context;
        }
        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; }//доделать корзину, потестить

        public static ICartRepo GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<FishingStoreContext>();
            string cartId = session.GetString("cartId") ?? Guid.NewGuid().ToString();

            session.SetString("cartId", cartId);
            return new EfCartRepo(context) { CartId = cartId };
        }
        public void AddItem(Item item)
        {
            this.context.CartItems.Add(new CartItem
            {
                ItemCartId= CartId,
                Item = item,
            });
            context.SaveChanges();
        }
        public List<CartItem> GetItems()
        {
            return context.CartItems.Where(x => x.ItemCartId == CartId).Include(i => i.Item).ToList();
        }
        public void DeleteFrom(Item item)
        {
            context.CartItems.Remove(context.CartItems.FirstOrDefault(c => c.Item == item && c.ItemCartId == CartId));
            context.SaveChanges();
        }
        public void DeleteAll()
        {
            context.CartItems.RemoveRange(context.CartItems);
            context.SaveChanges();
        }
    }
}
