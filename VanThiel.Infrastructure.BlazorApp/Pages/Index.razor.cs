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

public partial class Index
{
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
    private string Email { get; set; }
    private string Password { get; set; }
    private string Warning { get; set; } = string.Empty;
    #endregion

    #region [ Methods - Public ]
    public async Task SignInAsync()
    {
        var isValid = CheckValidInput();
        if (!isValid)
        {
            return;
        }

        var signInModel = new SignInModel { Email = this.Email, Password = this.Password };

        try
        {
            var response = await this.AuthenticationService.UserSignInAsync(signInModel);
            var authenticationProvider = (AuthenticationProvider)AuthenticationStateProvider;
            await authenticationProvider.UpdateAuthenticationStateAsync(response);
            NavigationManager.NavigateTo("/counter");
        } catch (Exception ex)
        {

            this.Warning = ex.Message;
        }

        return;
    }
    #endregion

    #region [ Methods - Private ]
    private bool CheckValidInput()
    {
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            this.Warning = "Invalid Input";
            return false;
        }
        return true;
    }
    #endregion
}
