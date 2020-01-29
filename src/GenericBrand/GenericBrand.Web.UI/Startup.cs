using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GenericBrand.Data.DAL;
using GenericBrand.Data.DAL.Identity.IdentityExtensions;
using Microsoft.AspNetCore.Identity.UI.Services;
using GenericBrand.Web.UI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace GenericBrand.Web.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AppDbConnection"),
                    x => x.MigrationsAssembly("GenericBrand.Data")));

            // TODO: Replace AddIdentityExtended with Require Confirmed Account option when SendGrid is setup
            services.AddIdentityExtended();
            // services.AddIdentityExtended(options => options.SignIn.RequireConfirmedAccount = true);

            // Add the localization services to the services container
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddControllersWithViews();
            services.AddControllersWithViews(options =>
            {
                // Automatically validate Antiforgery Tokens. No need for [ValidateAntiForgeryToken] on methods.
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
                // Add support for finding localized views, based on file name suffix, e.g. Index.fr.cshtml
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                // Add support for localizing strings in data annotations (e.g. validation messages) via the IStringLocalizer abstractions.
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(SharedDataResource));
                });

            // Configure supported cultures and localization options
            var supportedCultures = new[]
            {
                new CultureInfo("en")   // English
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                // State what the default culture for your application is. This will be used if no specific culture
                // can be determined for a given request.
                options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");

                // You must explicitly state which cultures your application supports.
                // These are the cultures the app supports for formatting numbers, dates, etc.
                options.SupportedCultures = supportedCultures;

                // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
                options.SupportedUICultures = supportedCultures;

                //options.RequestCultureProviders.Insert(0, new UrlRequestCultureProvider());
                // You can change which providers are configured to determine the culture for requests, or even add a custom
                // provider with your own logic. The providers will be asked in order to provide a culture for each request,
                // and the first to provide a non-null result that is in the configured supported cultures list will be used.
                // By default, the following built-in providers are configured:
                // - QueryStringRequestCultureProvider, sets culture via "culture" and "ui-culture" query string values, useful for testing
                // - CookieRequestCultureProvider, sets culture via "ASPNET_CULTURE" cookie
                // - AcceptLanguageHeaderRequestCultureProvider, sets culture via the "Accept-Language" request header
                //options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                //{
                //  // My custom request culture logic
                //  return new ProviderCultureResult("en");
                //}));
            });


            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            IOptions<RequestLocalizationOptions> localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
