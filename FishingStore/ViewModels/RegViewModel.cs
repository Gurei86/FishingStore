using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FishingStore.ViewModels
{
    public class RegViewModel
    {
        [Required(ErrorMessage = "Неверный email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Неверное имя")]
        
        public string FullName { get; set; }

        [Required(ErrorMessage = "Некорректный пароль")]
        public string Password { get; set; }

    }
}
