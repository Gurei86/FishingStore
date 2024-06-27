using FishingStore.Migrations;
using FishingStore.Model.Repos.Abstract;
using FishingStore.Model.Repos.EFs;

namespace FishingStore.ViewModels
{
    public class CartViewModel
    {
        public ICartRepo Cart { get; set; }
        public String Error { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
