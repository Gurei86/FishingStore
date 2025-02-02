﻿using FishingStore.Model.Entities;
using FishingStore.Model.Parameters;
using FishingStore.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FishingStore.Model
{
    public class CatalogModel
    {
        public List<Item> Items { get; set; }
        /// <summary>
        /// Каттегории
        /// </summary>
        public List<SelectListItem> Categories { get; set; }
        /// <summary>
        /// Параметры
        /// </summary>
        public ItemsParameters Parameters { get; set; }
    }
}
