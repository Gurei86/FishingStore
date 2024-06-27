using FishingStore.Model;
using FishingStore.Model.Entities;
using FishingStore.Model.Parameters;
using FishingStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FishingStore.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly DataManager dataManager;
        public AuthorizationController(DataManager data)
        {
            dataManager = data;
        }
        // GET: AuthorizationController
        public ActionResult Registration()
        {
            return View(new RegViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (dataManager.Users.GetUserByEmail(model.Email) == null)
                {
                    var user = new Model.Entities.User
                    {
                        Email = model.Email,
                        FullName = model.FullName,
                        Password = model.Password,
                        RoleId = dataManager.Roles.GetRoleByName("User").Id
                    };
                    dataManager.Users.SaveUser(user);
                    UserParameters.CurrentUser = user;

                    return RedirectToAction("Index", "Home");
                }

            }
            return View(model);
        }
        // GET: AuthorizationController/Details/5
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = dataManager.Users.GetUserByEmail(model.Email);
                if ( user!= null)
                {
                    UserParameters.CurrentUser = user;
                    if(user.RoleId==1)
                        return RedirectToAction("Admin", "Admin");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
   


    }
}
