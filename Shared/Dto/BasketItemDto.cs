using System.ComponentModel.DataAnnotations;

namespace Shared.Dto
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        [Range(0,double.MaxValue)]
        public decimal Price { get; set; }
        public string pictureUrl { get; set; }

        [Range(1,99)]
        public int Quantity { get; set; }

    }
}