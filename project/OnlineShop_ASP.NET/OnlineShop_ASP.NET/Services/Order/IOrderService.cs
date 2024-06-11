using Microsoft.Identity.Client;
using OnlineShop_ASP.NET.Models.Order;
using OnlineShop_ASP.NET.Models.Orders;
using OnlineShop_ASP.NET.Models.Products;
using OnlineShopCourseWork.Models;
using System.Net;

namespace OnlineShop_ASP.NET.Services.Order
{
	public interface IOrderService
	{
		int AddOrder(OrdersInputViewModel model);
		public List<Orders> GetAllOrdersFromDatabase();
		public Orders GetOrderById(int id);
		public void WasChanged(int Id);
		public int AddProductsToAnOrder(int OrderId, int product_Id, int WantedQwantity);
		public OrderdProducts Find_OrderdProduct(int OPId);
		public int RemoveOrder(int OrderId);
		public List<Orders> AllUserOrders(string Userid);
		public List<Orders> GetAllOrdersNamesFromDatabase();
		public List<OrderdProducts> AllUserProducts(int Orderid);
		public OrderdProducts Find_OrderdProductbyOrderid(int ORId);
		public int RemoveProductsFromAnOrder(int OrderId, int OPId,int Qwan);
		public void UpdateOrderQuantity(int Productid, int newQuantity);
		public void Orderpay(int Id);
		public List<OrderdProducts> Find_OrderdProductsbyOrderid(int ORId);
		public string Filler1(int id);
	}
}
