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
    public class MessageViewModel : PageModel
    {
        private SignInManager<SnackisUser> _signInManager;
        private UserManager<SnackisUser> _userManager;
        private MessageGateway _messageGateway;

        public SnackisUser MyUser { get; set; }
        public List<PresentationMessage> Messages { get; set; } = new();

        [BindProperty]
        public Guid DeleteMessageId { get; set; }
        public MessageViewModel(SignInManager<SnackisUser> signInManager, UserManager<SnackisUser> userManager, MessageGateway messageGateway)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _messageGateway = messageGateway;
        }

        public class PresentationMessage
        {
            public Guid ID { get; set; }
            public string Content { get; set; }
            public SnackisUser FromUser { get; set; }
            public SnackisUser ToUser { get; set; }
            public DateTime SentAt { get; set; }
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (_signInManager.IsSignedIn(User))
            {
                MyUser = await _userManager.GetUserAsync(User);
                var messages = await _messageGateway.GetMessagesByUserIdAsync(MyUser.Id);

            foreach (var message in messages)
            {
                var presentationMessage = new PresentationMessage
                {
                    ID = message.ID,
                    Content = message.Content,
                    FromUser = await _userManager.FindByIdAsync(message.FromUser.ToString()),
                    ToUser = await _userManager.FindByIdAsync(message.ToUser.ToString()),
                    SentAt = message.SentAt
                };
                Messages.Add(presentationMessage);
            }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteMessageAsync()
        {
            await _messageGateway.DeleteMessage(DeleteMessageId);
            return RedirectToPage();
        }
    }
}
