using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FishingStore.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Неверный email")]
        [EmailAddress]
        public string Email { get; set; }
       

        [Required(ErrorMessage = "Неверный пароль")]
        public string Password { get; set; }

    }
}
