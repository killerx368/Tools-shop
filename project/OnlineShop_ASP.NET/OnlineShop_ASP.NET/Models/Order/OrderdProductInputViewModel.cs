namespace OnlineShop_ASP.NET.Models.Order
{
    public class OrderdProductInputViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int OrderId { get; set; }

        public int OrderedQuantity { get; set; }


        
    }
}
