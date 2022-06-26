using Microsoft.AspNetCore.Components;
using Tangy_Models;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Pages.Authentication
{
    public partial class Register
    {
        private SignUpRequestDTO SignUpRequest = new();
        public bool Isprocessing { get; set; } = false;        
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        private async Task RegisterUser()
        {
            ShowRegistrationErrors = false;
            Isprocessing = true;
            var result = await _authService.RegisterUser(SignUpRequest);
            if (result.IsRegistrationSuccessful)
            {
                //registration succesfull
                _navigationManager.NavigateTo("/login");
            }
            else
            {
                //registration failed
                Errors = result.Errors;
                ShowRegistrationErrors = true;

            }
            Isprocessing = false;

        }
    }
}
