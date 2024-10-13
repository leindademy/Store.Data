namespace Store.Repository.Basket.Models
{
    public class CustomerBasket
    {
        public string?Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public string? PaymentIntentId { get; set; } //stripe payment 
        public string? ClientSecret { get; set; } // stripe payment 

    }
}
