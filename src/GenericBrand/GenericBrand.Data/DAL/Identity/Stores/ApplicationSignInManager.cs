using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GenericBrand.Data.Models.Identity;
using System.Threading.Tasks;

namespace GenericBrand.Data.DAL.Identity.Stores
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="contextAccessor"></param>
        /// <param name="claimsFactory"></param>
        /// <param name="optionsAccessor"></param>
        /// <param name="logger"></param>
        /// <param name="schemes"></param>
        /// <param name="confirmation"></param>
        public ApplicationSignInManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {

        }

        /// <summary>
        /// Pre Sign-In Check
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected override async Task<SignInResult> PreSignInCheck(ApplicationUser user)
        {
            // Add any pre-check sign in processes here

            return await base.PreSignInCheck(user);
        }
    }
}
