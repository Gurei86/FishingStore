using System;
using System.Collections.Generic;

namespace FishingStore.Model.Entities
{
    public partial class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string TitleImagePath { get; set; } = null!;
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
    }
}
