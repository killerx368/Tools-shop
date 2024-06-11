using Microsoft.AspNetCore.Identity;

namespace OnlineShopCourseWork.Models
{
    public sealed class Orders
    {
        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public decimal OrderPrice { get; set; }
        public int OrderQuantity { get; set; }

        public string UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
