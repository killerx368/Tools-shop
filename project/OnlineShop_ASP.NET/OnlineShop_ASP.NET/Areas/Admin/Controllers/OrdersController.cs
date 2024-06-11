using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Mysqlx.Crud;
using MySqlX.XDevAPI.Common;
using OnlineShop_ASP.NET.Extensions;
using OnlineShop_ASP.NET.Models;
using OnlineShop_ASP.NET.Models.Order;
using OnlineShop_ASP.NET.Models.Orders;
using OnlineShop_ASP.NET.Models.Products;
using OnlineShop_ASP.NET.Services.Admin;
using OnlineShop_ASP.NET.Services.Order;
using OnlineShopCourseWork.Models;


namespace OnlineShop_ASP.NET.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class OrdersController : Controller
	{
		private readonly IOrderService OrderService;
        private readonly IAdminService AdminService;
        public OrdersController(IOrderService OrderService, IAdminService AdminService)
		{
			this.OrderService = OrderService;
            this.AdminService = AdminService;

        }
        public IActionResult RemoveProduct(int id,int Prid,int Qwan)
        {


            var work = OrderService.RemoveProductsFromAnOrder(id, Prid, Qwan);
			
			return RedirectToAction("Get");
		}

		[HttpGet]
		public IActionResult Get()
		{

			List<Orders> result = this.OrderService.GetAllOrdersFromDatabase();

			return View(result);
		}



		[HttpGet]
		public IActionResult Change(int Id)
		{
			this.OrderService.WasChanged(Id);

			return RedirectToAction("Get");
		}


		[HttpGet]
		public IActionResult Delete(int Id)
		{
			OrderService.RemoveOrder(Id);

			return RedirectToAction("Get");
		}
        [HttpPost]
        public IActionResult AddTheProductToTheOrder (int orderIndex,int product_Id, int WantedQwantity)
        {
            string idFieldName = "id_" + orderIndex;

            // Get the id value from the form data
            int order_id = int.Parse(Request.Form[idFieldName]);
            OrderService.AddProductsToAnOrder( order_id, product_Id, WantedQwantity);
            return RedirectToAction("Details", "Product", new {id = product_Id});
           




        }
        [HttpPost]
        public IActionResult UpdateQuan(int Productid, int newQuantity)
        {
            OrderService.UpdateOrderQuantity(Productid, newQuantity);


			return RedirectToAction("Get");


        }

		[HttpPost]
        public IActionResult AddOrder(int Id, OrdersInputViewModel OrdersInput)
        {
            OrdersInputViewModel model = new OrdersInputViewModel();

            if (Id <= 0)
            {
                return RedirectToAction("CustomError");
            }


            model.OrderdProductId = Id;
            model.UserId = this.User.GetId();
            model.BuyerName = OrdersInput.BuyerName;
            model.BuyerPhoneNumber = OrdersInput.BuyerPhoneNumber;
            model.OrderName = OrdersInput.OrderName;
            model.OrderPrice = OrdersInput.OrderPrice;
            model.BuyerAddress = OrdersInput.BuyerAddress;
            model.BuyerEmail = OrdersInput.BuyerEmail;
            


            if (ModelState.IsValid == false)
            {
                return View("CustomError");
            }
            var result = OrderService.AddOrder(model);

            if (result == -1)
            {
                return View("CustomError");
            }


            return RedirectToAction("Details", "Product", new { id = Id });



        }
        

    }
}
