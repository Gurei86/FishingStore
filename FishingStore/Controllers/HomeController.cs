using FishingStore.Model;
using FishingStore.Model.Parameters;
using FishingStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using FishingStore.Model.Entities;
using FishingStore.Model.Enums;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace FishingStore.Controllers
{
    public class HomeController : Controller
    {
        public readonly DataManager dataManager;
        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        // GET: HomeController
        public ActionResult Index()
           
        {
            return View(dataManager.Items.GetItems(new Model.Parameters.ItemsParameters()
            {
                PageSize=5
            }).Result);
        }

        // GET: HomeController/Details/5
        public ActionResult Catalog(int page=1)
        {
            var parameters = new ItemsParameters()
            {
                pagesCount = dataManager.Items.CountOfItems() / 9 + 1,
                PageNumber= page
            };
          
            var model = new CatalogModel
            {
                Items = dataManager.Items.GetItems(parameters).Result,
                Categories= dataManager.Categories.GetCategories().ToList().Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList(),
            Parameters =parameters
            };
            return View(model);
        }
        [HttpPost]
        //,string nameFilter, string minPriceFilter, string maxPriceFilter, string categoryFilter
        public ActionResult Catalog(CatalogModel model, string nameFilter, int minPriceFilter, int maxPriceFilter, string sortParam)
        {
            var parameters = new ItemsParameters();
            if (nameFilter != null)
                parameters.nameFilter = nameFilter;
            if (minPriceFilter >0)
            {
                parameters.minPriceFilter = minPriceFilter;
                if(maxPriceFilter >0)
                {
                    if(minPriceFilter <=maxPriceFilter)
                    {
                        parameters.maxPriceFilter = maxPriceFilter;
                    }
                    
                }
                
            }
            if(model.Parameters!=null)
            if (model.Parameters.categoryFilter!=0)
                parameters.categoryFilter = model.Parameters.categoryFilter;
            switch (sortParam)
            {
                case "2":
                    parameters.isAsc = false; 
                    break;
                    case "3":
                    parameters.isAsc = false;
                    parameters.sortField = FieldsName.Price;
                    break; 
                case "4":
                    parameters.sortField= FieldsName.Price;
                    break;
                default:
                    break;
            }
            model = new CatalogModel
            {
                Items = dataManager.Items.GetItems(parameters).Result,
                Categories = dataManager.Categories.GetCategories().ToList().Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList(),
            Parameters =parameters
            };
            return View(model);
        }

        public IActionResult Item(int id)
        {
            if (id == default)
                return Error();
            var model = new ItemModel()
            {
                Item = dataManager.Items.GetItemById(id),

            };
            model.Items = dataManager.Items.GetItems(new ItemsParameters()
            {
                categoryFilter = model.Item.CategoryId
            }).Result.ToList();
            return View(model);
        }

        public IActionResult Profile()
        {
            if (UserParameters.CurrentUser == null)
                return RedirectToAction("Login", "Authorization");
            return View(UserParameters.CurrentUser);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
