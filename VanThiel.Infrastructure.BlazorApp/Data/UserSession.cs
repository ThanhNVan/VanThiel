using VanThiel.Domain.DTOs.ReturnModel;

namespace VanThiel.Infrastructure.Blazor.Data;

public class UserSession : UserAccessInfo
{
    #region [ Properties ]
    public string AccessToken { get; set; }

    public string Role { get; set; }

    public string ClientId { get; set; }
    #endregion
}
