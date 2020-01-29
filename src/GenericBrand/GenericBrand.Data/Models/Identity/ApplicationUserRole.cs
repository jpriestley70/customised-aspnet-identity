using Microsoft.AspNetCore.Identity;
using System;

namespace GenericBrand.Data.Models.Identity
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public ApplicationUser ApplicationUser { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
    }
}
