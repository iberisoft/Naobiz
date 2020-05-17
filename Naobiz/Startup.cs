using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Naobiz.Data;
using Naobiz.Services;
using Serilog;

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
            services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));

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

            var emailServicesBuilder = services.AddFluentEmail(settings.SiteEmail)
                .AddRazorRenderer();
            if (settings.Mailgun != null)
            {
                emailServicesBuilder.AddMailGunSender(settings.Mailgun.DomainName, settings.Mailgun.ApiKey, settings.Mailgun.Region);
                Log.Information("Configure emailing via Mailgun domain {DomainName}", settings.Mailgun.DomainName);
            }
            else
            {
                emailServicesBuilder.AddSmtpSender(settings.Smtp.Host, settings.Smtp.Port, settings.Smtp.User, settings.Smtp.Password);
                Log.Information("Configure emailing via SMTP server {Host}", settings.Smtp.Host);
            }

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
            dbContext.HashUserPasswords();

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
