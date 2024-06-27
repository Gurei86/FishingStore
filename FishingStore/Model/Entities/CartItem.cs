namespace FishingStore.Model.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public string ItemCartId { get; set; }
        public int Quantity { get; set; } = 1;// Количество добавленных товаров
        public decimal TotalPrice => Item.Price * Quantity; // Общая стоимость товаров
    }
}
