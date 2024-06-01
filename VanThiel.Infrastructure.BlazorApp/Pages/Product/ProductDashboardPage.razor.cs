using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Linq;
using System.Threading.Tasks;
using VanThiel.Domain.Entities;
using VanThiel.Infrastructure.Blazor.Extension;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Infrastructure.Blazor.Pages;


public partial class ProductDashboardPage
{
    #region [ Properties - Inject ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    private IUserService UserService { get; set; }

    [Inject]
    private IProductService ProductService { get; set; }

    [Inject]
    private IAuthenticationService AuthenticationService { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    public IMessageService MessageService { get; set; }

    [Inject]
    public PagingSettings PagingSettings { get; set; }
    #endregion

    #region [ Properties ]
    public PagingResult<Product> PagingResult { get; set; } = new PagingResult<Product>();

    public IQueryable<Product> Data { get; set; }

    public int CurrentPage { get; set; } = 1;

    public PaginationState Pagination { get; set; }

    #endregion

    #region [ Override Methods ]
    protected override async Task OnInitializedAsync()
    {
        this.PagingResult = await this.ProductService.GetMany_PagingAsync(this.CurrentPage);
        this.Pagination = new PaginationState { ItemsPerPage = this.PagingSettings.PageSize };
        await Pagination.SetTotalItemCountAsync(this.PagingResult.DataCount);

        this.Data = PagingResult.Data.AsQueryable();
    }
    #endregion

    #region [ Private - Methods ]
    private async Task GoToPageAsync(int capturedIndex)
    {
        this.PagingResult = await this.ProductService.GetMany_PagingAsync(capturedIndex + 1);
        await Pagination.SetCurrentPageIndexAsync(capturedIndex);
        await Pagination.SetTotalItemCountAsync(this.PagingResult.DataCount);
        
        this.Data = PagingResult.Data.AsQueryable();
    }

    private Appearance PageButtonAppearance(int pageIndex)
       => Pagination.CurrentPageIndex == pageIndex ? Appearance.Accent : Appearance.Neutral;

    private string? AriaCurrentValue(int pageIndex)
        => Pagination.CurrentPageIndex == pageIndex ? "page" : null;

    private string AriaLabel(int pageIndex)
        => $"Go to page {pageIndex}";

    private async Task MinusItemAsync(string productId)
    {

    }

    private async Task AddItemAsync(string productId)
    {

    }

    private void MoveToProductDetailPage(string productId)
    {

    }

    private async Task RemoveAllItemAsync(string productId)
    {

    }
    #endregion
}
