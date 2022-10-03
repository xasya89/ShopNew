using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using ShopNew.DAL.Models;
using ShopNew.DAL;
using System.Diagnostics.CodeAnalysis;

namespace ShopNew.Web.ViewModels.Goods
{
    public class GoodsViewModel:OwningComponentBase, IDisposable
    {
        [CascadingParameter(Name = "Shop")]
        public Shop shop { get; set; }
        [Inject]
        [NotNull]
        IDbContextFactory<shop_newContext> _dbFactory { get; set; }
        private shop_newContext _context;
        [Inject]
        private NavigationManager navigation { get; set; }

        public IEnumerable<Good> pagedData;
        public MudTable<Good> table;

        private int totalItems;
        public string searchString = null;
        public IEnumerable<GoodGroup> goodGroups = new List<GoodGroup>();
        public GoodGroup selectedGoodGroup = null;
        private bool isInitialized = false;


        protected override async Task OnInitializedAsync()
        {
            _context = _dbFactory.CreateDbContext();
            await SelectedGroups();
            isInitialized = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //await SelectedGroups();
        }

        public async Task SelectedGroups()
        {
            if (shop != null & _context != null)
            {
                goodGroups = await _context.GoodGroups.Where(gr => gr.ShopId == shop.Id).OrderBy(gr => gr.Name).ToListAsync();
                selectedGoodGroup = null;
                //selectedGoodGroup = goodGroups?.FirstOrDefault();
            }
        }

        public async Task<TableData<Good>> ServerReload(TableState state)
        {
            if (!isInitialized)
                return new TableData<Good>() { TotalItems = 0, Items = new List<Good>() };
            var goods = new List<Good>();
            if (selectedGoodGroup != null)
            {
                goods = await _context.Goods.Include(g => g.GoodGroup)
                    .Where(g => g.GoodGroupId == selectedGoodGroup.Id & (EF.Functions.Like(g.Name, $"%{searchString}%"))).ToListAsync();
            }
            else
                goods = await _context.Goods.Include(g => g.GoodGroup)
                    .Where(g => (EF.Functions.Like(g.Name, $"%{searchString}%"))).ToListAsync();
            if (goods.Count == 0)
            {
                var barcode = await _context.BarCodes.Where(b => b.Code == searchString).FirstOrDefaultAsync();
                if (barcode != null)
                    goods = await _context.Goods.Include(g => g.GoodGroup).Where(g => g.Id == barcode.Id).ToListAsync();
            }
            pagedData = goods.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            return new TableData<Good>() { TotalItems = goods.Count, Items = goods };
            //var groups = await _context.GoodGroups.Where(gr => gr.ShopId == shop.Id).AsNoTracking().ToListAsync();
            //var goods = await _context.Goods.Where(g=>g.)
            //IEnumerable<GoodGroup> data = await _context.GoodGroups.Include(gr => gr.Goods).ThenInclude(g => g.BarCodes)
            //    .Where(gr => gr.ShopId == shop.Id & (EF.Functions.Like(gr.Goods.))).AsNoTracking().ToListAsync();
            /*
            data = data.Where(element =>
            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (element.Sign.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if ($"{element.Number} {element.Position} {element.Molar}".Contains(searchString))
                    return true;
                return false;
            }).ToArray();
            */
            /*
             totalItems = data.Count();
             switch (state.SortLabel)
             {
                 case "nr_field":
                     data = data.OrderByDirection(state.SortDirection, o => o.Number);
                     break;
                 case "sign_field":
                     data = data.OrderByDirection(state.SortDirection, o => o.Sign);
                     break;
                 case "name_field":
                     data = data.OrderByDirection(state.SortDirection, o => o.Name);
                     break;
                 case "position_field":
                     data = data.OrderByDirection(state.SortDirection, o => o.Position);
                     break;
                 case "mass_field":
                     data = data.OrderByDirection(state.SortDirection, o => o.Molar);
                     break;
             }

             pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
             return new TableData<Element>() { TotalItems = totalItems, Items = pagedData };
             */
        }

        public void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }

        public void AddGood() => navigation.NavigateTo("/goodedit");

        public void Dispose() => _context.Dispose();
    }
}
