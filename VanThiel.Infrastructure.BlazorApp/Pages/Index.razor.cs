using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs.RequestModel;

namespace VanThiel.Infrastructure.Blazor.Pages;

public partial class Index
{
    #region [ Properties - Inject ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    //[Inject]
    //private HttpClientContext HttpClientContext { get; set; }
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

        //var result = await this.HttpClientContext.User.SignInAsync(signInModel);

        //if (result == null)
        //{
        //    this.Warning = "Incorrect Email or Password";
        //    return;
        //}

        //if (result.Model == null)
        //{
        //    this.Warning = "Incorrect Email or Password";
        //    return;
        //}
        //var encyptedAccessToken = Encription.Encrypt(result.Model.AccessToken);
        //result.Model.AccessToken = encyptedAccessToken;

        //await SessionStorage.SetItemAsync(AppUserRole.Model, result.Model);

        //NavigationManager.NavigateTo("Admin/Products", true);
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
