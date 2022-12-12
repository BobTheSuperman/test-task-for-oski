using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestForOski.WebClient.Services;
using TestForOski.WebClient.Services.Interfaces;
using TestForOski.WebClient.Utils;
using TestForOski.WebClient.Utils.Interfaces;

namespace TestForOski.WebClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApplicationSettings>(Configuration);

            services.AddHttpContextAccessor();

            services.AddScoped<IApiUtil, ApiUtil>();
            services.AddHttpClient<IHttpUtil, HttpUtil>(client =>
            {
#if DEBUG  
                client.BaseAddress = new Uri(Configuration.GetSection("Urls:Api:Http").Value);
#else
                client.BaseAddress = new Uri(configuration.GetSection("Urls:Api:Https").Value);
#endif
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddAuthorization();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.LoginPath = new PathString("/Account/Login");
                 });

            services.AddControllersWithViews().AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
