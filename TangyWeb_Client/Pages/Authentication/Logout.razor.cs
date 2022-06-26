using Microsoft.AspNetCore.Components;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await _authService.Logout();
            //Since we implemented NotifyUserLoggedIn() and  NotifyUserLogout() in AuthStateProvider,
            //we no longer need to force the refresh of whole the page with forceload
            _navigationManager.NavigateTo("/");
        }
    }
}
