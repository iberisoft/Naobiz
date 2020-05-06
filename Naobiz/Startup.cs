using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Naobiz.Data;
using Naobiz.Services;

namespace Naobiz
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
            var settings = Configuration.Get<Settings>();
            services.AddSingleton(settings);

            services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(settings.DbConnection));
            services.AddHttpContextAccessor();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = false;
            })
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();

            services.AddFluentEmail(settings.SiteEmail)
                .AddRazorRenderer()
                .AddSmtpSender(settings.Smtp.Host, settings.Smtp.Port, settings.Smtp.User, settings.Smtp.Password);

            services.AddScoped<UserService>();
            services.AddScoped<EmailService>();
            services.AddSingleton<ChatService>();
            services.AddScoped<TerritoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            dbContext.Database.EnsureCreated();

            app.UseRouting();
            app.UseAuthentication();

            app.ApplicationServices
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
