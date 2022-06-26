using Microsoft.AspNetCore.Components;
using System.Web;
using Tangy_Models;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Pages.Authentication
{
    public partial class Login
    {
        private SignInRequestDTO SignInRequest = new();
        public bool Isprocessing { get; set; } = false;
        public bool ShowSignInErrors { get; set; }
        public string Errors { get; set; }

        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        public string ReturnUrl { get; set; }
        private async Task LoginUser()
        {
            ShowSignInErrors = false;
            Isprocessing = true;
            var result = await _authService.Login(SignInRequest);
            if (result.IsAuthSuccessful)
            {
                //login succesfull
                var absoluteUrl = new Uri(_navigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUrl.Query);
                ReturnUrl = queryParam["returnUrl"];
                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    //Since we implemented NotifyUserLoggedIn() and  NotifyUserLogout() in AuthStateProvider,
                    //we no longer need to force the refresh of whole the page with forceload
                    _navigationManager.NavigateTo("/");
                }
                else
                {
                    _navigationManager.NavigateTo($"/{ReturnUrl}");
                }
            }
            else
            {
                //login failed
                Errors = result.ErrorMessage;
                ShowSignInErrors = true;

            }
            Isprocessing = false;
        }

    }
}
