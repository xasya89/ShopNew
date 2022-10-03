using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using ShopNew.BLL.Models;
using ShopNew.BLL.Models.LoginModels;
using ShopNew.DAL;
using ShopNew.Web.Infrastructure;
using ShopNew.Web.Pages;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ShopNew.Web.ViewModels
{
    public class LoginViewModel: OwningComponentBase, IDisposable
    {
        [Inject]
        [NotNull]
        IDbContextFactory<shop_newContext> _dbFactory { get; set; }

        [Inject]
        ISessionStorageService _store { get; set; }
        [Inject]
        ILocalStorageService _localStore { get; set; }
        [Inject]
        NavigationManager _navigation { get; set; }

        shop_newContext _context;
        public LoginModel loginModel { get; set; } = new();
        public EditContext loginEditContext;

        protected override async Task OnInitializedAsync()
        {
            loginEditContext = new EditContext(loginModel);
            _context = _dbFactory.CreateDbContext();
            if(await _localStore.GetStringAsync("login-name") != null)
            {
                loginModel.Login = await _localStore.GetStringAsync("login-name");
                loginModel.Password = await _localStore.GetStringAsync("login-pass");
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
        }

        public async Task Login()
        {
            if (loginEditContext.Validate())
            {
                var user = await _context.Users.Where(u => u.Email == loginModel.Login & u.Password == loginModel.PasswordMd5)
                    .FirstOrDefaultAsync();
                if (user == null)
                {
                    loginModel.Password = "";
                    return;
                }
                await _store.SetAsync<SecurityTokenModel>("token", new SecurityTokenModel { UserId = user.Id, UserName = user.Name, Role = user.UserRole });
                await _localStore.SetStringAsync("login-name", loginModel.Login);
                await _localStore.SetStringAsync("login-pass", loginModel.Password);
                _navigation.NavigateTo("/", true);
            }
        }

        public void Dispose() => _context.Dispose();
    }
}
