using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using OnlineShop_ASP.NET.Data;
using OnlineShop_ASP.NET.Models.User;

namespace OnlineShop_ASP.NET.Services.Admin

{
	public class AdminService : IAdminService
	{
		private ApplicationDbContext data;

		public AdminService(ApplicationDbContext data)
		{
			this.data = data;
		}

		public List<UserInputViewModel> GetAllUserNames()
			  => this.data.Users.Select(c => new UserInputViewModel
			  {
				  UserId = c.Id,
				  UserName = c.UserName,
				  UserEmail = c.Email,

			  }).ToList();



		public IdentityUser? GetUserById(string id)
		{
			return this.data.Users
						.Where(p => p.Id == id)
						.FirstOrDefault();

		}
        public IdentityUser? GetUserByName(string name)
        {
            return this.data.Users
                        .Where(p => p.UserName == name)
                        .FirstOrDefault();

        }

        public IdentityUser? GetOrdersOfUserById(string id)
		{
			return this.data.Users

						.Where(p => p.Id == id)
						.FirstOrDefault();

		}


		
		public void Delete(string Id)
		{
			var result = GetUserById(Id);
			if (result == null) { return; }
			result.UserName = "DELETED";
			this.data.SaveChanges();
			this.data.Users.Remove(result);
			this.data.SaveChanges();

		}
	}
}
