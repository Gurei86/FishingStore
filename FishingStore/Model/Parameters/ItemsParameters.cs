using FishingStore.Model.Enums;

namespace FishingStore.Model.Parameters
{
    public class ItemsParameters
    {
        
        const int maxPageSize = 30;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 9;
        public int pagesCount { get; set; }
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
        public FieldsName sortField { get; set; } = 0;
        public bool isAsc { get; set; } = true;
        public string? nameFilter { get; set; }
        public int? minPriceFilter { get; set; }
        public int? maxPriceFilter { get; set; }
        public int categoryFilter { get; set; } = 0;
    }
}
