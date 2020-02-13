namespace Shop.Domain.Models
{
    public class CartProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public string PhotoUrl { get; set; }
    }
}
