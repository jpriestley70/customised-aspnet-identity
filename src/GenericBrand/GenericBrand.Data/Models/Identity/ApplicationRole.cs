using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GenericBrand.Data.Models.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
        }

        public int RoleLevel { get; set; }

        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}