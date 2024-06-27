using System;
using System.Collections.Generic;

namespace FishingStore.Model.Entities
{
    public partial class Category
    {
        public Category()
        {
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Item> Items { get; set; }
    }
}
