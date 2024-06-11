using System.ComponentModel.DataAnnotations;

namespace OnlineShopCourseWork.Models
{
    public sealed class Category
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
    }
}
