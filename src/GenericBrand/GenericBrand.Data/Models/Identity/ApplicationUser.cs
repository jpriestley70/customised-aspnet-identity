using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GenericBrand.Data.Models.Identity
{
    //[ModelMetadataType(typeof(ApplicationUserMetadata))]
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        // References
        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
