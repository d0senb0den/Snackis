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
    public class CommentsViewModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly SubCategoryGateway _subCategoryGateway;
        private readonly PostGateway _postGateway;
        private readonly CommentGateway _commentGateway;
        private readonly ReportGateway _reportGateway;
        private readonly MessageGateway _messageGateway;

        public SnackisUser MyUser { get; set; }
        public SubCategory SubCategory { get; set; }
        public List<PresentationComment> Comments { get; set; } = new();
        public Post Post { get; set; }
        public SnackisUser CreatedByUser { get; set; }

        [BindProperty]
        public Message NewMessage { get; set; }

        [BindProperty]
        public Report ReportInput { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid PostId { get; set; }

        [BindProperty]
        public Comment NewOrChangedComment { get; set; }

        public CommentsViewModel(UserManager<SnackisUser> userManager, SubCategoryGateway subCategoryGateway, PostGateway postGateway,
                                 CommentGateway commentGateway, ReportGateway reportGateway, MessageGateway messageGateway)
        {
            _userManager = userManager;
            _subCategoryGateway = subCategoryGateway;
            _postGateway = postGateway;
            _commentGateway = commentGateway;
            _reportGateway = reportGateway;
            _messageGateway = messageGateway;
        }

        public class PresentationComment
        {
            public Guid ID { get; set; }
            public string Content { get; set; }
            public DateTime CreatedAt { get; set; }
            public Guid PostID { get; set; }
            public SnackisUser User { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);
            Post = await _postGateway.GetPost(PostId);
            if (PostId == Guid.Empty)
            {
                return NotFound();
            }
            CreatedByUser = await _userManager.FindByIdAsync(Post.User.ToString());
            SubCategory = await _subCategoryGateway.GetSubCategory(Post.SubCategoryID);
            var comments = await _commentGateway.GetComments();

            foreach (var comment in comments)
            {
                var presentationComment = new PresentationComment
                {
                    ID = comment.ID,
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                    PostID = comment.PostID,
                    User = await _userManager.FindByIdAsync(comment.UserID.ToString())
                };
                Comments.Add(presentationComment);
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _commentGateway.PostComments(NewOrChangedComment);

            return RedirectToPage(new { PostId = NewOrChangedComment.PostID });
        }
        public async Task<IActionResult> OnPostReportAsync()
        {
            ReportInput.ID = Guid.NewGuid();
            await _reportGateway.PostReports(ReportInput);
            
            return RedirectToPage(new { PostId = PostId });
        }

        public async Task<IActionResult> OnPostMessageAsync()
        {
            NewMessage.ID = Guid.NewGuid();
            await _messageGateway.PostMessages(NewMessage);

            return RedirectToPage(new { PostId = PostId });
        }
    }
}
