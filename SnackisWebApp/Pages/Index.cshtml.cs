using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
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
        private readonly CategoryGateway _categoryGateway;
        private readonly SubCategoryGateway _subCategoryGateway;

        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid SelectedSubCategoryId { get; set; }

        public IndexModel(CategoryGateway categoryGateway, SubCategoryGateway subCategoryGateway)
        {
            _categoryGateway = categoryGateway;
            _subCategoryGateway = subCategoryGateway;
        }

        public async Task OnGetAsync()
        {
            Categories = await _categoryGateway.GetCategories();
            SubCategories = await _subCategoryGateway.GetSubCategories();
        }
    }
}
