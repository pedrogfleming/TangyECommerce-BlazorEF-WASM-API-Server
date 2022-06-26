using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_DataAccess
{
    /// <summary>
    /// We extends the table User with : IdentityUser
    /// ApplicationUser is a new category user,
    /// distinc of the default IdentityUser
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
