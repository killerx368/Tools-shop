namespace OnlineShop_ASP.NET.Models;

using OnlineShop_ASP.NET.Models.Order;
using OnlineShop_ASP.NET.Models.Orders;
using OnlineShop_ASP.NET.Models.Products;

public class Details_OrdersInputViewModel
    {

     public  OrdersInputViewModel OrdersInput { get; set; }
    public  DetailsInputModel DetailsInput { get;  set ; }

    public OrderdProductInputViewModel OrderdProductInput { get; set; }



}

