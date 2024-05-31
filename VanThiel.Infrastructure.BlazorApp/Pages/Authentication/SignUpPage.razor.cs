using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs.RequestModel;
using VanThiel.Infrastructure.Blazor.Authentication;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;

namespace VanThiel.Infrastructure.Blazor.Pages;

public partial class SignUpPage
{
    [SupplyParameterFromForm]
    private SignUpModel SignUpModel { get; set; } = new();

    #region [ Properties - Inject ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    private IAuthenticationService AuthenticationService { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    #endregion

    #region [ Properties ]
    public string Warning { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;
    #endregion

    #region [ Methods - Public ]
    public void MoveToIndex()
    {
        NavigationManager.NavigateTo("/");
    }

    public async Task SignUpAsync()
    {
        if (ConfirmPassword != SignUpModel.Password)
        {
            this.Warning = "Password must match Confirm Password";
            return;
        }
        try
        {
            var response = await this.AuthenticationService.UserSignUpAsync(this.SignUpModel);
            var authenticationProvider = (AuthenticationProvider)AuthenticationStateProvider;
            await authenticationProvider.UpdateAuthenticationStateAsync(response);
            NavigationManager.NavigateTo("/my-profile");
        } catch (Exception ex)
        {
            this.Warning = ex.Message;
        }

        return;
    }
    #endregion
}
