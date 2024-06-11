using Microsoft.CodeAnalysis;
using OnlineShop_ASP.NET.Data;
using OnlineShop_ASP.NET.Models;
using OnlineShop_ASP.NET.Models.Order;
using OnlineShop_ASP.NET.Models.Orders;
using OnlineShop_ASP.NET.Models.Products;
using OnlineShop_ASP.NET.Services.Admin;
using OnlineShop_ASP.NET.Services.Brands;
using OnlineShop_ASP.NET.Services.Categories;
using OnlineShop_ASP.NET.Services.Products;
using OnlineShopCourseWork.Models;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Tls;
using System.Security.Cryptography;

namespace OnlineShop_ASP.NET.Services.Order
{
    public class OrderService : IOrderService
    {
        private ApplicationDbContext data;
        private IBrandService brandService;
        private ICategoryService categoryService;
        private IProductService productService;
        private IAdminService adminService;
		
		public OrderService(ApplicationDbContext data, IBrandService brandService, ICategoryService categoryServices, IProductService productService, IAdminService adminService)
        {
            this.data = data;
            this.brandService = brandService;
            this.categoryService = categoryServices;
            this.productService = productService;
            this.adminService = adminService;

		}
        public int AddOrder(OrdersInputViewModel model)
        {
            if (model == null)
            {
                return -1;
            }

            if (model.BuyerName == null || model.BuyerPhoneNumber == null || model.BuyerEmail == null || model.BuyerAddress == null)
            {
                return -1;
            }
            Orders test = this.data.Orders
							.Where(p => p.OrderName == model.OrderName)
							.FirstOrDefault();

			if (test == null) {
                
                Orders AddOrde = new Orders()
                {
                    BuyerName = model.BuyerName,
                    BuyerPhoneNumber = model.BuyerPhoneNumber,
                    BuyerEmail = model.BuyerEmail,
                    BuyerAddress = model.BuyerAddress,
                    OrderName = model.OrderName,
                    OrderPrice = 0,
                    UserId = model.UserId,
                    IsPaid = false,
                    IsConfirmed = false
                };




                this.data.Add(AddOrde);
                this.data.SaveChanges();

                return AddOrde.Id;
            }
            return model.Id;

		}
        public int AddProductsToAnOrder(int OrderId, int product_Id, int WantedQwantity)
        {
            if (OrderId == 0 || product_Id == 0 || WantedQwantity <= 0)
            {
                return -1;
            }

            

            var AddedOrder = this.data.Orders.Find(OrderId);

            Product? Model1 = this.data.Products
                            .Where(p => p.Id == product_Id)
                            .FirstOrDefault();

            if (Model1.Quantity < WantedQwantity && Model1.Quantity <= 0)
            {
                return AddedOrder.Id;
            }
            else
            {
                if (AddedOrder == null)
                {
                    return -1;
                }
                if (AddedOrder.Id <= 0)
                {
                    return -1;
                }
                if (AddedOrder.BuyerName == null || AddedOrder.BuyerPhoneNumber == null || AddedOrder.BuyerEmail == null || AddedOrder.BuyerAddress == null)
                {
                    return -1;
                }



                AddedOrder.OrderDate = DateTime.Now;
                if (AddedOrder.OrderPrice < 0)
                {
                    AddedOrder.OrderPrice = 0;
                }



                OrderdProducts test0 = this.data.OrderdProducts
                               .Where(p => p.ProductId == product_Id)
                               .FirstOrDefault();






                AddedOrder.OrderPrice = AddedOrder.OrderPrice + (Model1.Price * WantedQwantity);



                this.data.Orders.Update(AddedOrder);



                if (test0 != null)
                {

                    test0.OrderedQuantity += WantedQwantity;


                    if (test0.OrderId == 0)
                    {
                        return -1;
                    }

                    this.data.OrderdProducts.Update(test0);



                    this.data.SaveChanges();
                }
                else
                {
                    OrderdProducts Item = new OrderdProducts()
                    {

                        OrderId = AddedOrder.Id,
                        Price = Model1.Price,
                        OrderedProduct = Model1,
                        ProductId = Model1.Id,
                        ImageUrl = Model1.ImageURL,


                    };


                    Item.OrderedQuantity = WantedQwantity;


                    if (Item.OrderId == 0)
                    {
                        return -1;
                    }

                    this.data.Add(Item);


                    this.data.SaveChanges();
                }

                return AddedOrder.Id;
            }
        }

        public OrderdProducts Find_OrderdProduct(int OPId)
        {
            OrderdProducts ProductADD = this.data.OrderdProducts
                            .Where(p => p.Id == OPId)
                            .FirstOrDefault();
            return ProductADD;
        }
        public OrderdProducts Find_OrderdProductbyOrderid(int ORId)
        {
            OrderdProducts ProductADD = this.data.OrderdProducts
                            .Where(p => p.OrderId == ORId)
                            .FirstOrDefault();
            return ProductADD;
        }
        public string Filler1(int id )
        {

			var temp = this.data.Orders
						.Where(p => p.Id == id)
						.FirstOrDefault();
			
			return temp.UserId;
        }
		public List<OrderdProducts> Find_OrderdProductsbyOrderid(int ORId)
		{
			List<OrderdProducts> ProductADD = this.data.OrderdProducts
							.Where(p => p.OrderId == ORId).ToList();

			return ProductADD;
		}
		public int RemoveProductsFromAnOrder(int OrderId, int OPId,int Qwan)
        {
            var AddedOrder = GetOrderById(OrderId);
            OrderdProducts OrderdProduct = Find_OrderdProduct(OPId);
            AddedOrder.OrderPrice = AddedOrder.OrderPrice - (OrderdProduct.Price * Qwan);


			if (AddedOrder == null)
            {
                return -1;
            }
            if (AddedOrder.Id <= 0)
            {
                return -1;
            }
            if (AddedOrder.BuyerName == null || AddedOrder.BuyerPhoneNumber == null || AddedOrder.BuyerEmail == null)
            {
                return -1;
            }
            if (OrderdProduct == null)
            {
                return -1;
            }
            if (AddedOrder.Id != OrderdProduct.OrderId)
            {
                return -1;
            }


            this.data.Orders.Update(AddedOrder);
			this.data.Remove(OrderdProduct);
            this.data.SaveChanges();

            return OrderId;

        }


        public int RemoveOrder(int OrderId)
        {
            var AddedOrder = GetOrderById(OrderId);

            if (AddedOrder == null)
            {
                return -1;
            }
            if (AddedOrder.Id <= 0)
            {
                return -1;
            }
            if (AddedOrder.BuyerName == null || AddedOrder.BuyerPhoneNumber == null || AddedOrder.BuyerEmail == null || AddedOrder.BuyerAddress == null)
            {
                return -1;
            }

            this.data.Remove(AddedOrder);


            List<OrderdProducts> ProductADD = this.data.OrderdProducts
                            .Where(p => p.OrderId == AddedOrder.Id).ToList();

            int count = ProductADD.Count;

            for (int i = 0; i < count; i++)
            {

                this.data.Remove(ProductADD[i]);

            }



            this.data.SaveChanges();

            return AddedOrder.Id;
        }
        public List<Orders> GetAllOrdersFromDatabase()
              => this.data.Orders.Select(c => new Orders
              {
                  OrderName = c.OrderName,
                  BuyerAddress = c.BuyerAddress,
                  BuyerName = c.BuyerName,
                  BuyerPhoneNumber = c.BuyerPhoneNumber,
                  OrderPrice = c.OrderPrice,
                  Id = c.Id,
                  UserId = c.UserId,
                  OrderDate = c.OrderDate,
                  ConfirmDate = c.ConfirmDate,
                  IsConfirmed = c.IsConfirmed


              }).ToList();
        public List<Orders> GetAllOrdersNamesFromDatabase()
              => this.data.Orders.Select(c => new Orders
              {
                  OrderName = c.OrderName,

              }).ToList();

        public Orders GetOrderById(int id)
        {

            return this.data.Orders
                        .Where(p => p.Id == id)
                        .FirstOrDefault();

        }

        public void WasChanged(int Id)
        {
            var result = GetOrderById(Id);
            
            if (result.IsConfirmed == true)
            {
                result.IsConfirmed = false;
            }
            else
            {
                if (result.IsPaid == true )
                {
                    List<OrderdProducts> Allprodinord = Find_OrderdProductsbyOrderid(Id);
                    if (Allprodinord != null)
                    {
                        for (int i = 0; i < Allprodinord.Count; i++)
                        {

                            Product temp = this.data.Products.Where(p => p.Id == Allprodinord[i].ProductId).FirstOrDefault();
                            if (temp.Quantity >= Allprodinord[i].OrderedQuantity)
                            {temp.Quantity = temp.Quantity - Allprodinord[i].OrderedQuantity;
                                
                            }
                            else
                            {
                                temp.Quantity = 0;

                            }

						}
                    }

                }
                result.IsConfirmed = true;
            }

            result.ConfirmDate = DateTime.UtcNow;


            this.data.SaveChanges();
        }
        //--------------------------------------------
		public void Orderpay(int Id)
		{
			var result = GetOrderById(Id);
			if (result.IsPaid == true)
			{
				result.IsPaid = false;
			}
			else
			{
				result.IsPaid = true;
			}
          
			result.OrderDate = DateTime.UtcNow;


			this.data.SaveChanges();
		}
        //---------------------------------------------

		public List<Orders> AllUserOrders(string Userid)
        {
           
            var OrdersByUser = this.data.Orders
                            .Where(p => p.UserId == Userid).ToList();
            return OrdersByUser;

        }
        public List<OrderdProducts> AllUserProducts(int Orderid)
        {

            var OrdersByUser = this.data.OrderdProducts
                            .Where(p => p.OrderId == Orderid).ToList();
            return OrdersByUser;

        }


        public void UpdateOrderQuantity(int Productid, int newQuantity)
        {
            OrderdProducts chage = this.Find_OrderdProduct(Productid);
            
            Orders chagePrice = this.data.Orders
                .Where(p => p.Id == chage.OrderId).FirstOrDefault();
            if (newQuantity > chage.OrderedQuantity) {
                chagePrice.OrderPrice = chagePrice.OrderPrice + (chage.Price * (newQuantity - chage.OrderedQuantity));
            }
            else
            {
                chagePrice.OrderPrice = chagePrice.OrderPrice - (chage.Price * ( chage.OrderedQuantity - newQuantity));

			}

            chage.OrderedQuantity = newQuantity;
			this.data.OrderdProducts.Update(chage);
			this.data.Orders.Update(chagePrice);
			this.data.SaveChanges();

		}




    }


}
