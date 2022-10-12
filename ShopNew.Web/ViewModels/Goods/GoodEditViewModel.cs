using Microsoft.AspNetCore.Components;
using ShopNew.DAL.Models;
using Microsoft.EntityFrameworkCore;
using ShopNew.DAL;
using Microsoft.AspNetCore.Components.Forms;

namespace ShopNew.Web.ViewModels.Goods
{
    public class GoodEditViewModel:OwningComponentBase, IDisposable
    {
        [CascadingParameter(Name = "Shop")]
        private Shop shop { get; set; }
        [Parameter]
        public int? id { get; set; }
        [Inject]
        private IDbContextFactory<shop_newContext> _dbFactory { get; set; }
        [Inject]
        private NavigationManager _navigation { get; set; }

        private shop_newContext _context;
        public Good good = new();
        public IEnumerable<GoodGroup> groups = new List<GoodGroup>();
        public IEnumerable<Supplier> suppliers=new List<Supplier>();
        public EditContext editContext;
        public bool success;

        public string newBarCodeStr = "";

        protected override async Task OnInitializedAsync()
        {
            editContext = new EditContext(good);
            _context=_dbFactory.CreateDbContext();
            good = await _context.Goods.Include(g=>g.BarCodes).Where(g => g.Id == id).FirstOrDefaultAsync();
            if (good == null)
                good = new();
            if (shop != null)
            {
                groups = await _context.GoodGroups.Where(gr => gr.ShopId == shop.Id).OrderBy(gr=>gr.Name).ToListAsync();
                suppliers = await _context.Suppliers.Where(s => s.ShopId == shop.Id).OrderBy(s => s.OrgName).ToListAsync();
            }
            editContext = new EditContext(good);
        }


        public void OnValidSubmit(EditContext context)
        {
            success = true;
            StateHasChanged();
            if (good.Id == 0)
            {
                _context.Goods.Add(good);
                //TODO: Добавить возможность указания начальных остатков
            }
            _context.SaveChanges();
            _navigation.NavigateTo("/goods");
        }


        public void AddBarCode()
        {
            if (newBarCodeStr != "") good.BarCodes.Add(new BarCode { Good = good, Code = newBarCodeStr });
        }
        public void DeleteBarCode(BarCode barcode) => good.BarCodes.Remove(barcode);

        public void Dispose() => _context.Dispose();
    }
}
