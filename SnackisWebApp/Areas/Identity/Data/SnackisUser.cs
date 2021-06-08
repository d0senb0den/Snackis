using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SnackisWebApp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the SnackisUser class
    public class SnackisUser : IdentityUser
    {
        public byte[] ProfilePicture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
