using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SnackisWebApp.Areas.Identity.Data;
using SnackisWebApp.Gateway;
using SnackisWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly CategoryGateway _categoryGateway;
        private readonly SubCategoryGateway _subCategoryGateway;
        private readonly PostGateway _postGateway;
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<Post> Posts { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid SelectedSubCategoryId { get; set; }

        public IndexModel(UserManager<SnackisUser> userManager, CategoryGateway categoryGateway, SubCategoryGateway subCategoryGateway, PostGateway postGateway)
        {
            _userManager = userManager;
            _categoryGateway = categoryGateway;
            _subCategoryGateway = subCategoryGateway;
            _postGateway = postGateway;
        }
        public async Task OnGetAsync()
        {
            Categories = await _categoryGateway.GetCategories();
            SubCategories = await _subCategoryGateway.GetSubCategories();
            Posts = await _postGateway.GetPosts();
        }
    }
}
