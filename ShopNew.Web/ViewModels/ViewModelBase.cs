using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using ShopNew.DAL.Models;
using ShopNew.DAL;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Microsoft.AspNetCore.Components.Forms;
using ShopNew.BLL.Models.LoginModels;

namespace ShopNew.Web.ViewModels
{
    public class ViewModelBase: OwningComponentBase, IDisposable
    {
        [CascadingParameter(Name = "Shop")]
        public Shop shop { get; set; }
        [Inject]
        [NotNull]
        IDbContextFactory<shop_newContext> _dbFactory { get; set; }
        public shop_newContext _context;
        [Inject]
        public NavigationManager navigation { get; set; }


        protected override async Task OnInitializedAsync()
        {
            _context = _dbFactory.CreateDbContext();
        }
        public void Dispose() => _context.Dispose();
    }
}
