using FishingStore.Model.Entities;
using FishingStore.ViewModels;

namespace FishingStore.Model
{
    public class CreationModel
    {
        /// <summary>
        /// Предмет
        /// </summary>
        public Item Item { get; set; }
        public EditItemViewModel ViewModel { get; set; }

    }
}
