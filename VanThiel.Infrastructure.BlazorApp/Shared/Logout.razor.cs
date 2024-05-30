using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using VanThiel.Infrastructure.Blazor.Authentication;

namespace VanThiel.Infrastructure.Blazor.Shared;

public partial class Logout
{
    #region [ Properties - Inject ]
    [Inject]
    public AuthenticationStateProvider AuthenticationProvider { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {
        await this.LogoutAsync();
    }
    #endregion

    #region [ Methods -  ]
    private async Task LogoutAsync()
    {
        var authenticationProvider = (AuthenticationProvider)this.AuthenticationProvider;
        await authenticationProvider.UpdateAuthenticationStateAsync(null);
        NavigationManager.NavigateTo("/");
    }
    #endregion
}
