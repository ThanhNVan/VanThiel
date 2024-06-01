using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Threading.Tasks;
using VanThiel.Domain.Entities;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;

namespace VanThiel.Infrastructure.Blazor.Pages;


public partial class ProductDashboardPage
{
    #region [ Properties - Inject ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private IProductService ProductService { get; set; }

    [Inject]
    private ICartService CartService { get; set; }

    [Inject]
    private IAuthenticationService AuthenticationService { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    public IMessageService MessageService { get; set; }
    #endregion

    #region [ Properties ]
    public IQueryable<Product> Data { get; set; }

    PaginationState pagination = new PaginationState { ItemsPerPage = 12 };
    #endregion

    #region [ Override Methods ]
    protected override async Task OnInitializedAsync()
    {
        this.Data = (await this.ProductService.GetMany_ActiveAsync()).AsQueryable();
    }
    #endregion

    #region [ Private - Methods ]
    private async Task MinusItemAsync(string productId)
    {
        try
        {
            var result = await this.CartService.Update_SubtractFromCartAsync(productId);
            if (result)
            {
                var message = $"Subtracted from cart";
                MessageService.Clear();
                await MessageService.ShowMessageBarAsync(message, MessageIntent.Success, "MESSAGES_TOP");
                return;
            }
        } catch (Exception ex)
        {
            MessageService.Clear();
            await MessageService.ShowMessageBarAsync(ex.Message, MessageIntent.Error, "MESSAGES_TOP");
            return;
        }

    }

    private async Task AddItemAsync(string productId)
    {
        try
        {
            var result = await this.CartService.Update_AddSingleToCartAsync(productId);
            if (result)
            {
                var message = $"Added to cart";
                MessageService.Clear();
                await MessageService.ShowMessageBarAsync(message, MessageIntent.Success, "MESSAGES_TOP");
                return;
            }
        } catch (Exception ex)
        {
            MessageService.Clear();
            await MessageService.ShowMessageBarAsync(ex.Message, MessageIntent.Error, "MESSAGES_TOP");
            return;
        }
    }

    private void MoveToProductDetailPage(string productId)
    {

    }

    private async Task RemoveAllItemAsync(string productId)
    {
        try
        {
            var result = await this.CartService.Update_RemoveFromCartAsync(productId);
            if (result)
            {
                var message = $"Remove from cart";
                MessageService.Clear();
                await MessageService.ShowMessageBarAsync(message, MessageIntent.Success, "MESSAGES_TOP");
                return;
            }
        } catch (Exception ex)
        {
            MessageService.Clear();
            await MessageService.ShowMessageBarAsync(ex.Message, MessageIntent.Error, "MESSAGES_TOP");
            return;
        }
    }
    #endregion
}
