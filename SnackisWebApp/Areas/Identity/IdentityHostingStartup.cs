/*using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SnackisWebApp.Areas.Identity.Data;
using SnackisWebApp.Data;

[assembly: HostingStartup(typeof(SnackisWebApp.Areas.Identity.IdentityHostingStartup))]
namespace SnackisWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SnackisWebAppContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SnackisWebAppContextConnection")));

                services.AddDefaultIdentity<SnackisUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<SnackisWebAppContext>();
            });
        }
    }
}*/