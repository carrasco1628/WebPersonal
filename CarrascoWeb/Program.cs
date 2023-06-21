using CarrascoWeb.Application.Configuration.Api;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;

namespace CarrascoWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var api = builder.Configuration.GetSection("ApiConfigurationDto").Get<ApiConfiguration>();

            // Add services to the container.                      
            builder.Services.AddRazorPages();   
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
            builder.Services.AddAntiforgery(token => token.HeaderName = "XSRF-TOKEN");
            builder.Services.AddHttpClient("carrascoApi", config =>
            {
                config.BaseAddress = new Uri(api.ApiConfigurations.First(x => x.Name.Equals("CarrascoApi")).BaseUri);
            });
            builder.Services.AddApplication();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            CultureInfo[] supportedCultures = new[]
            {
                new CultureInfo("es-ES"),
                new CultureInfo("en-US"),
                new CultureInfo("es"),
                new CultureInfo("en"),
            };

            app.UseRequestLocalization( options => 
            {
                options.DefaultRequestCulture = new RequestCulture(new CultureInfo("en-US"));
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider> {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };        
            });

            app.UseExceptionHandler("/unspected-error");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}");
            app.UseAuthorization();
                 
            app.MapRazorPages();

            app.Run();
        }
    }
}