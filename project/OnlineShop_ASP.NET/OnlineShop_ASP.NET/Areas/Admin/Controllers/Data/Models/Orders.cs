using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopCourseWork.Models
{
	public class Orders
	{
		public int Id { get; set; }
		public string OrderName { get; set; }
		public DateTime? OrderDate { get; set; }
		public DateTime? ConfirmDate { get; set; }
		public decimal OrderPrice { get; set; }
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
		[Required]
		public string BuyerEmail { get; set; }
		public bool IsConfirmed { get; set; }
		public bool IsPaid { get; set; }
	}
}
