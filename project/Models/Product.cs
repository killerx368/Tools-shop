﻿namespace OnlineShopCourseWork.Models
{
    public sealed class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity{ get; set; }
        public int CategoryId { get; set; }
        public int BrandId{ get; set; }
    }
}
