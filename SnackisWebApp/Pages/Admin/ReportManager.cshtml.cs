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

namespace SnackisWebApp.Pages.Admin
{
    public class ReportManagerModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly ReportGateway _reportGateway;
        private readonly PostGateway _postGateway;
        private readonly CommentGateway _commentGateway;

        public SnackisUser MyUser { get; set; }
        public List<Report> Reports { get; set; }

        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }

        [BindProperty]
        public Guid DeletePostId { get; set; }

        [BindProperty]
        public Guid DeleteCommentId { get; set; }

        [BindProperty]
        public Guid DeleteReportId { get; set; }

        public ReportManagerModel(ReportGateway ReportGateway, UserManager<SnackisUser> userManager, PostGateway postGateway, CommentGateway commentGateway)
        {
            _userManager = userManager;
            _reportGateway = ReportGateway;
            _postGateway = postGateway;
            _commentGateway = commentGateway;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);
            Reports = await _reportGateway.GetReports();
            Posts = await _postGateway.GetPosts();
            Comments = await _commentGateway.GetComments();
            return Page();
        }

        public async Task<IActionResult> OnPostDeletePostAsync()
        {
            await _postGateway.DeletePosts(DeletePostId);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteCommentAsync()
        {
            await _commentGateway.DeleteComments(DeleteCommentId);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteReportAsync()
        {
            await _reportGateway.DeleteReports(DeleteReportId);
            return RedirectToPage();
        }
    }
}
