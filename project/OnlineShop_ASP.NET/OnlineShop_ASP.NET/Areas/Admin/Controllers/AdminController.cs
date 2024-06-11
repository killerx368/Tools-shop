using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop_ASP.NET.Models.Order;
using OnlineShop_ASP.NET.Models.User;
using OnlineShop_ASP.NET.Services.Admin;
using OnlineShopCourseWork.Models;

namespace OnlineShop_ASP.NET.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
		private readonly IAdminService adminService;
		public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IAdminService adminService )
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
           this.adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateRolesandUsers()
        {
            bool x = await this.roleManager.RoleExistsAsync("Admin");
            if (!x)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                await this.roleManager.CreateAsync(role);
                var user = new IdentityUser();
                user.UserName = "pepo@abv.bg";
             //   user.UserName = "Admin";
                user.Email = "pepo@abv.bg";
                string userPWD = "Admin.1";


               
              // IdentityResult chkUser = await userManager.CreateAsync(user, userPWD);
                IdentityUser user1 = this.userManager.Users.FirstOrDefault(e => e.Email == "pepo@abv.bg");


                //Add default User to Role Admin    
                if (user1 != null)
                {
                   IdentityResult result1 = await this.userManager.AddToRoleAsync(user1, role.Name);
                }
             }

            // creating Creating Manager role     
            x = await this.roleManager.RoleExistsAsync("Manager");
            if (!x)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Manager";
                await this.roleManager.CreateAsync(role);
            }

            
            return Redirect("https://localhost:7280/");
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
			List<UserInputViewModel> result = this.adminService.GetAllUserNames();



			return View(result);
        }
		[HttpGet]
		public IActionResult Delete(string Id)
        {

			adminService.Delete(Id);
			return RedirectToAction("GetUsers");

		}


	}
}
