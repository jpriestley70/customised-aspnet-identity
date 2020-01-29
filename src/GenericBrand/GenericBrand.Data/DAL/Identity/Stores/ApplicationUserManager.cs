using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GenericBrand.Data.Models.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace GenericBrand.Data.DAL.Identity.Stores
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store"></param>
        /// <param name="optionsAccessor"></param>
        /// <param name="passwordHasher"></param>
        /// <param name="userValidators"></param>
        /// <param name="passwordValidators"></param>
        /// <param name="keyNormalizer"></param>
        /// <param name="errors"></param>
        /// <param name="services"></param>
        /// <param name="logger"></param>
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        #region FirstName
        /// <summary>
        /// Get FirstName
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GetFirstNameAsync(ApplicationUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            ApplicationUserStore applicationUserStore = (ApplicationUserStore)Store;
            return await applicationUserStore.GetFirstNameAsync(user, CancellationToken);
        }

        /// <summary>
        /// Set First Name
        /// </summary>
        /// <param name="user"></param>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public async Task<IdentityResult> SetFirstNameAsync(ApplicationUser user, string firstName)
        {
            user.FirstName = firstName;
            return await base.Store.UpdateAsync(user, new System.Threading.CancellationToken());
        }
        #endregion

        #region LastName
        /// <summary>
        /// Get LastName
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GetLastNameAsync(ApplicationUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            ApplicationUserStore applicationUserStore = (ApplicationUserStore)Store;
            return await applicationUserStore.GetLastNameAsync(user, CancellationToken);
        }

        /// <summary>
        /// Set Last Name
        /// </summary>
        /// <param name="user"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public async Task<IdentityResult> SetLastNameAsync(ApplicationUser user, string lastName)
        {
            user.LastName = lastName;
            return await base.Store.UpdateAsync(user, new System.Threading.CancellationToken());
        }
        #endregion

        #region ToString
        /// <summary>
        /// Get Display Name
        /// If FirstName is empty then returns username
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public string ToStringDisplayName(ClaimsPrincipal principal)
        {
            var task = Task.Run<ApplicationUser>(async () =>  await base.GetUserAsync(principal));
            var user = task.Result;

            if (user == null) { return base.GetUserName(principal); }

            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                return base.GetUserName(principal);
            }
            else
            {
                return user.FirstName;
            }
        }
        #endregion
    }
}