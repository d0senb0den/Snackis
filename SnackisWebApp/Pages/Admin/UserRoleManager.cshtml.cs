using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SnackisWebApp.Areas.Identity.Data;

namespace SnackisWebApp.Pages.Admin
{
    public class UserRoleManagerModel : PageModel
    {
        public class UserRole
        {
            public string UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public IEnumerable<string> Roles { get; set; }

        }
        public class ManageUserRole
        {
            public string RoleId { get; set; }
            public string RoleName { get; set; }
            public bool Selected { get; set; }
        }

        public List<UserRole> userRoles { get; set; } = new();

        private readonly UserManager<SnackisUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRoleManagerModel(UserManager<SnackisUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            foreach (SnackisUser user in users)
            {
                var thisUser = new UserRole();
                thisUser.UserId = user.Id;
                thisUser.Email = user.Email;
                thisUser.FirstName = user.FirstName;
                thisUser.LastName = user.LastName;
                thisUser.UserName = user.UserName;
                thisUser.Roles = await GetUserRoles(user);
                userRoles.Add(thisUser);
            }
            return Page();
        }
        private async Task<List<string>> GetUserRoles(SnackisUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
    }
}
