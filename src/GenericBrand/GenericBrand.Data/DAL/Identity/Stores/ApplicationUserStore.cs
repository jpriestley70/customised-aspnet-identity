using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GenericBrand.Data.Models.Identity;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace GenericBrand.Data.DAL.Identity.Stores
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationUserToken, ApplicationRoleClaim>
    {
        public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        /// <summary>
        /// Create User Claim
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        /// <returns></returns>
        protected override ApplicationUserClaim CreateUserClaim(ApplicationUser user, Claim claim)
        {
            var userClaim = new ApplicationUserClaim() { UserId = user.Id };
            userClaim.InitializeFromClaim(claim);

            //return base.CreateUserClaim(user, claim);
            return userClaim;
        }

        /// <summary>
        /// Create User Login
        /// </summary>
        /// <param name="user"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        protected override ApplicationUserLogin CreateUserLogin(ApplicationUser user, UserLoginInfo login)
        {
            return new ApplicationUserLogin()
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName
            };

            //return base.CreateUserLogin(user, login);
        }

        /// <summary>
        /// Create User Role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        protected override ApplicationUserRole CreateUserRole(ApplicationUser user, ApplicationRole role)
        {
            return new ApplicationUserRole()
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            //return base.CreateUserRole(user, role);
        }

        /// <summary>
        /// Create User Token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="loginProvider"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override ApplicationUserToken CreateUserToken(ApplicationUser user, string loginProvider, string name, string value)
        {
            return new ApplicationUserToken()
            {
                UserId = user.Id,
                LoginProvider = loginProvider,
                Name = name,
                Value = value
            };

            //return base.CreateUserToken(user, loginProvider, name, value);
        }

        /// <summary>
        /// Get FirstName
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetFirstNameAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.FirstName);
        }

        /// <summary>
        /// Get LastName
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetLastNameAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.LastName);
        }
    }
}
