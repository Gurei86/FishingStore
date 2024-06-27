using FishingStore.Model;
using FishingStore.Model.Entities;
using FishingStore.Model.Parameters;
using FishingStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.IO;

namespace FishingStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataManager dataManager;
        IWebHostEnvironment _appEnvironment;
        public AdminController(DataManager dataManager,IWebHostEnvironment _appEnvironment)
        {
            this.dataManager = dataManager;
            this._appEnvironment = _appEnvironment;
        }
        // GET: AdminController
        public ActionResult Admin(int page=1)
        {
            var parameters = new ItemsParameters()
            {
                pagesCount = dataManager.Items.CountOfItems() / 9 + 1,
                PageNumber = page
            };
            var model = new CatalogModel
            {
                Items = dataManager.Items.GetItems(parameters).Result,
                Categories = dataManager.Categories.GetCategories().ToList().Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList(),
                Parameters=parameters
        };
            
            return View(model);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult EditItem(int id)
        {

            var model = new CreationModel
            {
                Item = id == default ? new Item() : dataManager.Items.GetItemById(id),
                ViewModel = new EditItemViewModel(),
            };
            model.ViewModel.Categories = dataManager.Categories.GetCategories().ToList().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            return View(model);
        }
            
        

        // POST: AdminController/Create
        [HttpPost]

        public async Task<IActionResult> EditItem(CreationModel model,string description,decimal price)
        {
            
            model.Item.Description=description;
            model.Item.Price = price;
            if (model.Item.Name != null)
            {
                
                    if (model.Item.Price != 0)
                    {

                        if (model.Item.Discount >= 0 || model.Item.Discount <= 100)
                        {
                            if (model.Item.TitleImagePath != null)
                            {
                                if (model.Item.CategoryId != 0)
                                {
                                if (dataManager.Items.GetItemByName(model.Item.Name) == null)
                                {
                                    dataManager.Items.SaveItem(model.Item);
                                    return RedirectToAction("Admin");
                                }
                                else if(model.Item.Id!= 0) 
                                {
                                    dataManager.Items.SaveItem(model.Item);
                                    return RedirectToAction("Admin");
                                }
                                }
                            }
                        }

                    }
                
            }
                
                
            model.ViewModel.Categories= dataManager.Categories.GetCategories().ToList().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            return View(model);
        }
        //ИЗМЕНИТЬ ПОЛЕ ЦЕНА, КОРЗИНА И ОДИНОЧКА
        [HttpPost]
            public IActionResult DeleteItem(int id)
        {
            dataManager.Items.DeleteItem(id);
            return RedirectToAction("Admin");
        }
    }
}
