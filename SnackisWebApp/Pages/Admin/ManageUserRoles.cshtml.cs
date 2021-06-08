using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Areas.Identity.Data;

namespace SnackisWebApp.Pages.Admin
{
    public class ManageUserRolesModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ManageUserRolesModel(UserManager<SnackisUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public class ManageUserRole
        {
            public string RoleId { get; set; }
            public string RoleName { get; set; }
            public bool Selected { get; set; }
        }
        public string ErrorMessage { get; set; }
        public string UserName { get; set; }

        [BindProperty]
        public List<ManageUserRole> ManageRoles { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            var user2 = await _userManager.FindByIdAsync(userId);
            if (user2 == null)
            {
                ErrorMessage = $"User with ID = {userId} cannot be found";
                return NotFound(ErrorMessage);
            }
            user2.UserName = UserName;
            foreach (var role in _roleManager.Roles)
            {
                var userRoles = new ManageUserRole
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user2, role.Name))
                {
                    userRoles.Selected = true;
                }

                ManageRoles.Add(userRoles);
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);
            if (user == null)
            {
                return Page();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return Page();
            }
            result = await _userManager.AddToRolesAsync(user, ManageRoles.Where(r => r.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return Page();
            }
            return RedirectToPage("UserRoleManager");
        }
    }
}
