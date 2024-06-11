using Microsoft.AspNetCore.Identity;
namespace OnlineShopCourseWork.Models
{
	
	public class OrderdProducts
	{
		public int Id { get; set; }
		public decimal Price { get; set; }
		public Product OrderedProduct { get; set; }
		public int ProductId { get; set; }
		public string ImageUrl { get; set; }
		public int OrderId { get; set; }
		public int OrderedQuantity { get; set; }
	}
}
