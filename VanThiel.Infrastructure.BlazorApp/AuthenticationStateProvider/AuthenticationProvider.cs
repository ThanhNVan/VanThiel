using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using VanThiel.Infrastructure.Blazor.Data;

namespace VanThiel.Infrastructure.Blazor.Authentication;

public class AuthenticationProvider : AuthenticationStateProvider
{
    #region [ Fields ]
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    private readonly ISessionStorageService _session;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsPrincipal());
    private readonly ILogger<AuthenticationProvider> _logger;
    #endregion

    #region [ CTor ]
    public AuthenticationProvider(JwtSecurityTokenHandler jwtSecurityTokenHandler,
                                    ISessionStorageService session,
                                  ILogger<AuthenticationProvider> logger)
    {
        this._jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        this._session = session;
        this._logger = logger;
    }
    #endregion

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSession = await _session.GetItemAsync<UserSession>("UserSession");
            if (userSession == null)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }

            var tokenContent = new ClaimsIdentity(ParseClaimsFromJwt(userSession.AccessToken), "jwt");
            var user = new ClaimsPrincipal(new ClaimsIdentity(tokenContent.Claims, "Jwt"));
            var result = new AuthenticationState(user);
            return result;
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return new AuthenticationState(_anonymous);
        }
    }

    public async Task UpdateAuthenticationStateAsync(string? accessToken)
    {
        var claimsPrincipal = default(ClaimsPrincipal);

        if (!string.IsNullOrEmpty(accessToken)) // sign in 
        {
            var tokenContent = this._jwtSecurityTokenHandler.ReadJwtToken(accessToken);

            var identity = new ClaimsIdentity(ParseClaimsFromJwt(accessToken), "jwt");
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(tokenContent.Claims, "Jwt"));

            var userSession = new UserSession {
                AccessToken = accessToken,
                Email = identity.Claims.FirstOrDefault(x => x.Type == "Email").Value,
                UserId = identity.Claims.FirstOrDefault(x => x.Type == "UserId").Value,
                Fullname = identity.Claims.FirstOrDefault(x => x.Type == "FullName").Value,
                PhoneNumber = identity.Claims.FirstOrDefault(x => x.Type == "PhoneNumber").Value,
                Role = identity.Claims.FirstOrDefault(x => x.Type == "role").Value,
            };

            await _session.SetItemAsync("UserSession", userSession);
            await Task.Delay(200);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
        } else
        { // sign out
            claimsPrincipal = _anonymous;
            await _session.RemoveItemAsync("UserSession");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

    }

    public void UpdateAuthenticationState(AuthenticationState state)
    {
        NotifyAuthenticationStateChanged(Task.FromResult(state));
    }

    public async Task<string> GetToken()
    {
        var result = string.Empty;

        try
        {
            var userSession = await _session.GetItemAsync<UserSession>(nameof(UserSession));
            if (userSession != null)
            {
                result = userSession.AccessToken;
            }

            return result;
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            throw;
        }
    }

    public void MarkUserAsAuthenticated(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

        NotifyAuthenticationStateChanged(authState);
    }
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    public static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}
