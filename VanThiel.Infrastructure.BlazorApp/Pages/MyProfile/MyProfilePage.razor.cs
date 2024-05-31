using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs;
using VanThiel.Infrastructure.Blazor.Authentication;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;
using VanThiel.SharedLibrary.Extension;

namespace VanThiel.Infrastructure.Blazor.Pages;

public partial class MyProfilePage
{
    [SupplyParameterFromForm]
    private UserMyProfile Model { get; set; } = new();
    private UserMyProfile Data { get; set; } = new();

    #region [ Properties - Inject ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    private IUserService UserService { get; set; }

    [Inject]
    private IAuthenticationService AuthenticationService { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    public IMessageService MessageService { get; set; }
    #endregion

    #region [ Override Methods ]
    protected override async Task OnInitializedAsync()
    {
        this.Data = await this.UserService.GetSingle_MyProfileAsync();
        this.Model = ObjectCopier.Clone(this.Data);
        StateHasChanged();
        await base.OnInitializedAsync();
    }
    #endregion

    #region [ Properties ]
    public bool IsReadonly { get; set; } = true;
    public bool IsValid { get; set; } = false;
    public string PhoneWarning { get; set; } = string.Empty;
    #endregion

    #region [ Public Methods ]
    public void AllowUpdate()
    {
        this.IsReadonly = false;
        StateHasChanged();
    }

    public void CancelUpdate()
    {
        this.IsReadonly = true;
        this.Model = ObjectCopier.Clone(this.Data);
        StateHasChanged();
    }

    public async Task SaveAsync()
    {
        await IsPhoneNumberExistedAsync();
        await IsEmailExistedAsync();

        try
        {
            var jwtToken = await AuthenticationService.Update_UserProfileAsync(this.Model);
            this.IsReadonly = true;

            var authenticationProvider = (AuthenticationProvider)AuthenticationStateProvider;
            await authenticationProvider.UpdateAuthenticationStateAsync(jwtToken);
            StateHasChanged();

            return;
        } catch (Exception ex)
        {
            this.IsValid = false;
            await MessageService.ShowMessageBarAsync($"{ex.Message}", MessageIntent.Warning, "MESSAGES_TOP");
            return;
        }
    }

    public async Task IsPhoneNumberExistedAsync()
    {
        try
        {
            Regex validatePhoneNumberRegex = new Regex("^\\+?[1-9][0-9]{7,14}$");
            validatePhoneNumberRegex.IsMatch(this.Model.PhoneNumber);
            if (!validatePhoneNumberRegex.IsMatch(this.Model.PhoneNumber))
            {
                this.IsValid = false;
                var message = $"Invalid Phone Number";
                await MessageService.ShowMessageBarAsync(message, MessageIntent.Warning, "MESSAGES_TOP");
                return;
            }

            var isExistedPhone = await this.UserService.GetSingle_IsExistPhoneNumberAsync(this.Model.PhoneNumber, this.Data.PhoneNumber);
            if (isExistedPhone)
            {
                this.IsValid = false;
                var message = $"Existed Phone Number";
                await MessageService.ShowMessageBarAsync(message, MessageIntent.Warning, "MESSAGES_TOP");
                return;
            }

            this.IsValid = true;
            await MessageService.ShowMessageBarAsync("Valid Phone Number", MessageIntent.Success, "MESSAGES_TOP");
            return;
        } catch (Exception ex)
        {

            this.IsValid = false;
            await MessageService.ShowMessageBarAsync($"{ex.Message}", MessageIntent.Warning, "MESSAGES_TOP");
            return;
        }
    }

    public async Task IsEmailExistedAsync()
    {
        try
        {
            var isEmail = MailAddress.TryCreate(this.Model.Email, out _);
            if (!isEmail)
            {
                this.IsValid = false;
                var message = $"Invalid Email Address";
                await MessageService.ShowMessageBarAsync(message, MessageIntent.Warning, "MESSAGES_TOP");
                return;
            }

            var isExistedEmail = await this.UserService.GetSingle_IsExistEmailAsync(this.Model.Email, this.Data.Email);
            if (isExistedEmail)
            {
                this.IsValid = false;
                var message = $"Existed Email";
                await MessageService.ShowMessageBarAsync(message, MessageIntent.Warning, "MESSAGES_TOP");
                return;
            }

            this.IsValid = true;
            await MessageService.ShowMessageBarAsync("Valid Email Address", MessageIntent.Success, "MESSAGES_TOP");
            return;
        } catch (Exception ex)
        {
            this.IsValid = false;
            await MessageService.ShowMessageBarAsync($"{ex.Message}", MessageIntent.Warning, "MESSAGES_TOP");
            return;
        }
    }
    #endregion
}
