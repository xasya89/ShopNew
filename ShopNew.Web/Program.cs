using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ShopNew.Web.Data;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using ShopNew.DAL;
using ShopNew.DAL.Models;
using ShopNew.Web.Extensions;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Solutaris.InfoWARE.ProtectedBrowserStorage.Extensions;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Solutaris.InfoWARE.ProtectedBrowserStorage.Services;
using ShopNew.Web.Infrastructure;
using ShopNew.BLL.Models.LoginModels;
using Microsoft.AspNetCore.Identity;

namespace ShopNew.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
            builder.Services.AddScoped<ISessionStorageService, SessionStorageService>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddMudServices();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddDbContextFactory<shop_newContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("PgSQL"));
            });
            builder.Services.AddIWProtectedBrowserStorage();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            using (var scope = app.Services.CreateScope())
            using (var db = scope.ServiceProvider.GetService<IDbContextFactory<shop_newContext>>().CreateDbContext())
            {
                db.Database.Migrate();
                InitFirstValue.Init(db).Wait();
            }

            app.Run();
        }
    }

    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _store;
        public CustomAuthStateProvider(ISessionStorageService store)
        {
            _store = store;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AuthenticationState returnAnonymus()
            {
                var identity = new ClaimsIdentity();
                var anonymusPrincipal = new ClaimsPrincipal(identity);
                return new AuthenticationState(anonymusPrincipal);
            }
            var token = await _store.GetAsync<SecurityTokenModel>("token");
            if (token == null) return returnAnonymus();

            var claims = new List<Claim>()
            {
                new Claim("UserId", token.UserId.ToString()),
                new Claim(ClaimTypes.Name, token.UserName),
                new Claim(ClaimTypes.Role, token.Role.ToString()),
            };
            var identity = new ClaimsIdentity(claims,"Token");
            var user = new ClaimsPrincipal(identity);
            
            return new AuthenticationState(user);
        }
    }
}