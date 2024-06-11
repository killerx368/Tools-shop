using OnlineShopCourseWork.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop_ASP.NET.Models.Orders
{
	public class OrdersInputViewModel
	{

		public int Id { get; set; }
		public int? OrderdProductId { get; set; }
	    public decimal OrderPrice { get; set; }
		[Required]
		[StringLength(maximumLength: 300)]
		public string BuyerAddress { get; set; }
		[Required]
		[StringLength(maximumLength: 9)]
		public string BuyerPhoneNumber { get; set; }
        [Required]
		[StringLength(maximumLength:50)]
		public string BuyerEmail { get; set; }
		[Required]
		[StringLength(maximumLength: 150)]
		public string? BuyerName { get; set; }
		public string? UserId { get; set; }
        public string OrderName { get; set; }

    }
}
