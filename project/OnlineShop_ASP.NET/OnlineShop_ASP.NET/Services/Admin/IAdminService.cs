using Microsoft.AspNetCore.Identity;
using OnlineShop_ASP.NET.Models.User;

namespace OnlineShop_ASP.NET.Services.Admin
{
    public interface IAdminService
    {
		public List<UserInputViewModel> GetAllUserNames();
		public IdentityUser GetUserById(string id);
		public void Delete(string Id);
        public IdentityUser? GetUserByName(string name);

    }
}
