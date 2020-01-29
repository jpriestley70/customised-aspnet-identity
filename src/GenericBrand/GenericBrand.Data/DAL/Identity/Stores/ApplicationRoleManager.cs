using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using GenericBrand.Data.Models.Identity;

namespace GenericBrand.Data.DAL.Identity.Stores
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole> store, IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<ApplicationRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}