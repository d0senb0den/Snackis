using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SnackisWebApp.Gateway;
using SnackisWebApp.Models;

namespace SnackisWebApp.Pages.Admin
{
    public class AdminIndexModel : PageModel
    {
        private readonly CategoryGateway _categoryGateway;
        private readonly SubCategoryGateway _subCategoryGateway;

        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid DeleteCategoryId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid DeleteSubCategoryId { get; set; }

        [BindProperty]
        public Category NewOrChangedCategory { get; set; }

        [BindProperty]
        public SubCategory NewOrChangedSubCategory { get; set; }

        public SelectList SelectList { get; set; }

        public AdminIndexModel(CategoryGateway categoryGateway, SubCategoryGateway subCategoryGateway)
        {
            _categoryGateway = categoryGateway;
            _subCategoryGateway = subCategoryGateway;
        }

        public async Task OnGetAsync()
        {
            Categories = await _categoryGateway.GetCategories();
            SubCategories = await _subCategoryGateway.GetSubCategories();

            SelectList = new SelectList(Categories, "ID", "Name");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (NewOrChangedCategory != null)
            {
                NewOrChangedCategory.ID = Guid.NewGuid();
                await _categoryGateway.PostCategories(NewOrChangedCategory);
            }

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostSubCreateAsync()
        {
            if (NewOrChangedSubCategory != null)
            {
                NewOrChangedSubCategory.ID = Guid.NewGuid();
                await _subCategoryGateway.PostSubCategories(NewOrChangedSubCategory);
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            if (DeleteCategoryId != Guid.Empty)
            {
                await _categoryGateway.DeleteCategories(DeleteCategoryId);
            }
            if (DeleteSubCategoryId != Guid.Empty)
            {
                await _subCategoryGateway.DeleteSubCategories(DeleteSubCategoryId);
            }

            return RedirectToPage();
        }
    }
}
