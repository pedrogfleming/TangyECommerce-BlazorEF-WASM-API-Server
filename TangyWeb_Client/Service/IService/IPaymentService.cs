using Tangy_Models;

namespace TangyWeb_Client.Service.IService
{
    public interface IPaymentService
    {
        public Task<SuccessModelDTO>Checkout(StripePaymentDTO model);
    }
}
