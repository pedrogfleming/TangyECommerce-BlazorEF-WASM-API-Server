using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Common
{
    /// <summary>
    /// We use constants rather than hardcoded string to improve escalation and refactoring
    /// </summary>
    public static class SD
    {
        public const string ShoppingCart = "ShoppingCart";

        /// <summary>
        /// Order First status 
        /// </summary>
        public const string Status_Pending = "Pending";
        /// <summary>
        /// Order Second status when its confirmed
        /// </summary>
        public const string Status_Confirmed = "Confirmed";
        /// <summary>
        /// Order Third status when the shipment its done
        /// </summary>
        public const string Status_Shipped = "Shipped";
        /// <summary>
        /// When the order its refunded
        /// </summary>
        public const string Status_Refunded = "Refunded";
        /// <summary>
        /// When the order is cancelled
        /// </summary>
        public const string Status_Cancelled = "Cancelled";

        #region Roles
        public const string Role_Admin = "Admin";
        public const string Role_Customer = "Customer";
        #endregion
        #region Local Storae
        public const string Local_Token = "JWT Token";
        public const string Local_OrderDetails = "Local_OrderDetails";
        public const string Local_UserDetails = "UserDetails";
        #endregion
    }
}
