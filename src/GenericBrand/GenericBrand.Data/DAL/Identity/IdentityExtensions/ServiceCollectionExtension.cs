using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using GenericBrand.Data.Models.Identity;
using GenericBrand.Data.DAL.Identity.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericBrand.Data.DAL.Identity.IdentityExtensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddIdentityExtended(this IServiceCollection services)
        {
            AddIdentityExtended(services, options => { });
        }

        public static void AddIdentityExtended(this IServiceCollection services, Action<IdentityOptions> options)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options)
                .AddUserStore<ApplicationUserStore>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
