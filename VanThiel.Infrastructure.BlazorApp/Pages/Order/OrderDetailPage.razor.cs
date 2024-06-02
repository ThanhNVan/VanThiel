using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs.ReturnModel;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;

namespace VanThiel.Infrastructure.Blazor.Pages;

public partial class OrderDetailPage
{
    #region [ Properties ]
    [Parameter]
    public string OrderId { get; set; }
    #endregion

    #region [ Properties - Inject ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private IOrderService OrderService { get; set; }

    [Inject]
    public IMessageService MessageService { get; set; }
    #endregion

    #region [ Properties ]
    public OrderInfo OrderInfo { get; set; }

    public IQueryable<OrderDetailInfo> Data { get; set; }
    #endregion

    #region [ Override Methods ]
    protected override async Task OnInitializedAsync()
    {
        MessageService.Clear();
        this.OrderInfo = await this.OrderService.GetSingle_InfoByIdAsync(OrderId);
        this.Data = this.OrderInfo.Details.AsQueryable();
    }
    #endregion

    #region [ Methods ]
    public void MoveToOrderDetailPage(string orderId)
    {
        NavigationManager.NavigateTo($"order-dashboard/{orderId}");
        return;
    }
    #endregion
}
