using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FishingStore.ViewModels
{
    public class EditItemViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Заполните это поле")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Неверный формат данных")]
        [DataType("Деньги",ErrorMessage ="Неверный формат данных")]
        public decimal Price { get; set; }
        [AllowNull]
        public int Discount { get; set; }
        
        
        public string Image { get; set; }
       
        [Required(ErrorMessage ="Не выбрана категория")]
        public List<SelectListItem> Categories { get; set; }

    }
}
