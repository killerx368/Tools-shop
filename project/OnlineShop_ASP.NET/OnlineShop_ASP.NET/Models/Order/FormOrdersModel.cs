using OnlineShopCourseWork.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop_ASP.NET.Models.Order
{
	public class FormOrdersModel
	{
		
		public int Id { get; set; }
		public string OrderName { get; set; }
		public DateTime? OrderDate { get; set; }
		public DateTime? ConfirmDate { get; set; }
		public decimal OrderPrice { get; set; }
		public decimal TotalPrice{ get; set; }
		public int OrderQuantity { get; set; }

		[Required]
		[StringLength(maximumLength: 15)]
		public string BuyerPhoneNumber { get; set; }
		[Required]
		[StringLength(maximumLength: 150)]

		public string? BuyerName { get; set; }

		[Required]
		[StringLength(maximumLength: 300)]
		public string BuyerAddress { get; set; }
		[Required]
		public string UserId { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }
		public bool IsConfirmed { get; set; }
	
}
}
