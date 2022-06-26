using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Models
{
    public class StripePaymentDTO
    {
        public OrderDTO Order { get; set; }
        public string SuccessUrl { get; set; }
        public string CancellUrl { get; set; }
        public StripePaymentDTO()
        {
            SuccessUrl = "OrderConfirmation";
            CancellUrl = "Summary";
        }

    }
}
