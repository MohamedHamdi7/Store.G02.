namespace Domain.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string  pictureUrl { get; set; }
        public int Quantity { get; set; }

    }
}