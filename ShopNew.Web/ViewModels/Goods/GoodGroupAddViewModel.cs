using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using ShopNew.DAL.Models;
using ShopNew.DAL;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components.Forms;

namespace ShopNew.Web.ViewModels.Goods
{
    public class GoodGroupAddViewModel: ViewModelBase
    {
        [Parameter]
        public int? groupId { get; set; }
        public GoodGroup goodGroup { get; set; } = new();
        public EditContext editContext { get; set; }
        public bool success;

        protected override async Task OnInitializedAsync()
        {
            editContext = new EditContext(goodGroup);
            await base.OnInitializedAsync();
            goodGroup = await _context.GoodGroups.Where(gr => gr.Id == groupId).FirstOrDefaultAsync();
            if (goodGroup == null)
                goodGroup = new() { Name="", ShopId=shop.Id};
            editContext=new EditContext(goodGroup);
        }

        public async Task Save(EditContext editContext)
        {
            if (goodGroup.Name == "")
                return;
            success = true;
            StateHasChanged();
            if (goodGroup.Id==0)
                _context.GoodGroups.Add(goodGroup);
            await _context.SaveChangesAsync();
            navigation.NavigateTo("/goods");
        }
    }
}
