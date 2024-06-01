using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs.ReturnModel;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;

namespace VanThiel.Infrastructure.Blazor.Pages;

public partial class MyCartPage
{
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private IOrderService OrderService { get; set; }

    [Inject]
    private ICartService CartService { get; set; }

    [Inject]
    public IMessageService MessageService { get; set; }

    #region [ Properties ]
    public IQueryable<CartInfo> Data { get; set; }

    PaginationState pagination = new PaginationState { ItemsPerPage = 12 };
    #endregion

    #region [ Override Methods ]
    protected override async Task OnInitializedAsync()
    {
        this.Data = (await this.CartService.GetMany_ByUserAsync()).AsQueryable();
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
                this.Data.FirstOrDefault(x => x.ProductId == productId).ProductInCart--;

                if (this.Data.FirstOrDefault(x => x.ProductId == productId).ProductInCart == 0)
                {
                    this.Data = this.Data.Where(x => x.ProductInCart > 0);
                }
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
                this.Data.FirstOrDefault(x => x.ProductId == productId).ProductInCart++;
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
                this.Data = this.Data.Where(x => x.ProductId != productId);
                return;
            }
        } catch (Exception ex)
        {
            MessageService.Clear();
            await MessageService.ShowMessageBarAsync(ex.Message, MessageIntent.Error, "MESSAGES_TOP");
            return;
        }
    }

    private async Task CheckOutAsync()
    {
        try
        {
            var payload = this.Data.Where(x => x.IsSelected).Select(x => x.Id).ToList();
            var result = await this.OrderService.Post_CheckOutAsync(payload);
            
            if (result) 
            {
                MessageService.Clear();
                await MessageService.ShowMessageBarAsync("Order successfully", MessageIntent.Success, "MESSAGES_TOP");
            }

            this.Data = this.Data.Where(x => !x.IsSelected);
            return;

        } catch (Exception ex)
        {
            MessageService.Clear();
            await MessageService.ShowMessageBarAsync(ex.Message, MessageIntent.Error, "MESSAGES_TOP");
            return;
        }
    }
    #endregion
}
