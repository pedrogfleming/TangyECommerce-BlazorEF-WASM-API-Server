using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Tangy_Common;
using TangyWeb_Client.Helper;

namespace TangyWeb_Client.Service
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService localStorage;
        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            this.localStorage = localStorage;
        }

        /// <summary>
        /// Needed to get the bearer token in each requeast and validate if the user is auth
        /// </summary>
        /// <returns></returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await localStorage.GetItemAsync<string>(SD.Local_Token);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.
                AuthenticationHeaderValue("bearer", token);
            if (token is null)
            {
                return new AuthenticationState(
                    new ClaimsPrincipal(
                        new ClaimsIdentity()));
                #region Hardcoding valid auths
                //return new AuthenticationState(
                //    new ClaimsPrincipal(
                //                new ClaimsIdentity(new[]
                //{
                //                            new Claim(ClaimTypes.Name,"ben@gmail.com")
                //                        }, "jwtAuthType")));
                #endregion
            }
            else
            {
                return new AuthenticationState(
                    new ClaimsPrincipal(
                        new ClaimsIdentity(
                            JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
            }
        }
        public void NotifyUserLoggedIn(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(
                                        new ClaimsIdentity(
                                            JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }
        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(
                                new AuthenticationState(
                                    new ClaimsPrincipal(
                                        new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
