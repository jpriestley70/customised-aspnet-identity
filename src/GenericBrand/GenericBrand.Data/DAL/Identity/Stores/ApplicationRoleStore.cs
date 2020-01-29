using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GenericBrand.Data.Models.Identity;
using System;

namespace GenericBrand.Data.DAL.Identity.Stores
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, ApplicationDbContext, Guid, ApplicationUserRole, ApplicationRoleClaim>
    {
        public ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        protected override ApplicationRoleClaim CreateRoleClaim(ApplicationRole role, Claim claim)
        {
            return base.CreateRoleClaim(role, claim);
        }
    }
}