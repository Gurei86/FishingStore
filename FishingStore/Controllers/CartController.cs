using FishingStore.Migrations;
using FishingStore.Model;
using FishingStore.Model.Repos.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using FishingStore.ViewModels;
using FishingStore.Model.Parameters;
using FishingStore.Model.Repos.EFs;
using FishingStore.Model.Entities;

namespace FishingStore.Controllers
{
    public class CartController : Controller
    {

        private readonly DataManager dataManager;


        public CartController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public IActionResult Cart()
        {
            if (UserParameters.CurrentUser == null)
                return RedirectToAction("Login", "Authorization");
            var items = dataManager.Carts.GetItems();
            dataManager.Carts.CartItems = items;

            var obj = new CartViewModel
            {
                Cart = dataManager.Carts,
            };
           obj.TotalPrice= obj.Cart.CartItems.Sum(x => x.Item.Price);
            return View(obj);
        }
        public RedirectToActionResult AddToCart(int id)
        {
            if (UserParameters.CurrentUser == null)
                return RedirectToAction("Login", "Authorization");



            var item = dataManager.Items.GetItemById(id);
            if (item != null)
            {
                dataManager.Carts.AddItem(item);
            }
            return RedirectToAction("Catalog","Home");
        }
        public RedirectToActionResult DeleteAll()
        {
            dataManager.Carts.DeleteAll();
            return RedirectToAction("Cart");
        }
        public RedirectToActionResult Delete(int id)
        {
            var item = dataManager.Items.GetItemById(id);
            if (item != null)
                dataManager.Carts.DeleteFrom(item);
            return RedirectToAction("Cart");
        }

        

        public ViewResult Error()
        {
            return View();
        }
        public RedirectToActionResult Back()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
