using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;

namespace VanThiel.Infrastructure.Blazor.Pages;

public partial class MyProfilePage
{
    [SupplyParameterFromForm]
    private UserMyProfile Model { get; set; } = new();

    #region [ Properties - Inject ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    private IUserService UserService { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    #endregion

    #region [ Override Methods ]
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        this.Model = await this.UserService.GetSingle_MyProfileAsync();
        await base.OnAfterRenderAsync(firstRender);
    }
    #endregion

    #region [ Properties ]
    public bool IsAllowUpdate { get; set; } 
    #endregion

    #region [ Public Methods ]
    public void AllowUpdate()
    {
        this.IsAllowUpdate = true;
    }


    #endregion
}
