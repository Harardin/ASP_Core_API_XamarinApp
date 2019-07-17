using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using asp_xamar_solution.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace asp_xamar_solution
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(Configuration["DataBase:ConnectionString"]));
            services.AddTransient<IUserDataModel, EFUserDataModel>();
            services.AddTransient<IUserWalletData, EFUserWalletData>();
            services.AddTransient<ITransactionsHistory, EFTransactionsHistory>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Stoped here, start reading the article from the beginning
            CookiePolicyOptions cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };
            app.UseStaticFiles();
            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseAuthentication();
            

            app.UseMvc(routes => {

                routes.MapRoute(name: null, template: "{controller}/{action}", defaults: new { controller = "Welcome", action = "WelcomeAction" });


                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });
            //SeedData.FillData(app);
        }
    }
}
