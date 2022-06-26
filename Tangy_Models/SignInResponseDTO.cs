using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Models
{
    public class SignInResponseDTO
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Token to authenticate the user
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// If the authentication is successful,
        /// We use this to store the user critical information
        /// </summary>
        public UserDTO UserDTO { get; set; }

    }
}
