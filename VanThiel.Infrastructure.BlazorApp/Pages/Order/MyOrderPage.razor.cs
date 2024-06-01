using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs.ReturnModel;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;

namespace VanThiel.Infrastructure.Blazor.Pages;

public partial class MyOrderPage
{
    #region [ Properties - Inject ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private IOrderService OrderService { get; set; }

    [Inject]
    public IMessageService MessageService { get; set; }
    #endregion

    #region [ Properties ]
    public IQueryable<MyOrderInfo> Data { get; set; }

    public PaginationState Pagination = new PaginationState { ItemsPerPage = 12 };
    #endregion

    #region [ Override Methods ]
    protected override async Task OnInitializedAsync()
    {
        MessageService.Clear();
        this.Data = (await this.OrderService.GetMany_ByUserAsync()).AsQueryable();
    }
    #endregion

    #region [ Methods ]
    public void MoveToOrderDetailPage(string orderId)
    {
        return;
    }
    #endregion
}
