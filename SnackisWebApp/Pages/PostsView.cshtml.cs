using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Areas.Identity.Data;
using SnackisWebApp.Gateway;
using SnackisWebApp.Models;



namespace SnackisWebApp.Pages
{
    public class PostsViewModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly SubCategoryGateway _subCategoryGateway;
        private readonly PostGateway _postGateway;

        public SnackisUser MyUser { get; set; }
        public SubCategory SubCategory { get; set; }
        public List<PresentationPost> Posts { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public Guid SubCategoryId { get; set; }

        [BindProperty]
        public Post NewOrChangedPost { get; set; }

        public PostsViewModel(UserManager<SnackisUser> userManager, SubCategoryGateway subCategoryGateway, PostGateway postGateway)
        {
            _userManager = userManager;
            _subCategoryGateway = subCategoryGateway;
            _postGateway = postGateway;
        }
        public class PresentationPost
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Content { get; set; }
            public Guid SubCategoryID { get; set; }
            public SnackisUser User { get; set; }
        }
        public async Task<IActionResult> OnGetAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);
            if (SubCategoryId == Guid.Empty)
            {
                return NotFound();
            }
            var posts = await _postGateway.GetPosts();
            SubCategory = await _subCategoryGateway.GetSubCategory(SubCategoryId);

            foreach (var post in posts)
            {
                var presentationPost = new PresentationPost
                {
                    ID = post.ID,
                    Name = post.Name,
                    SubCategoryID = post.SubCategoryID,
                    User = await _userManager.FindByIdAsync(post.User.ToString()),
                    Content = post.Content,
                    CreatedAt = post.CreatedAt
                };
                Posts.Add(presentationPost);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (NewOrChangedPost != null)
            {
                NewOrChangedPost.ID = Guid.NewGuid();
                var result = await _postGateway.PostPosts(NewOrChangedPost);
                if (result)
                {
                    return RedirectToPage(new { SubCategoryId = NewOrChangedPost.SubCategoryID });
                }
            }

            return RedirectToPage();
        }
    }
}
