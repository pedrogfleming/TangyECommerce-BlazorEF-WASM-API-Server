using Tangy_Models;

namespace TangyWeb_Client.ViewModel
{
    public class ShoppingCart
    {
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int ProductPriceId { get; set; }
        public ProductPriceDTO ProductPrice { get; set; }
        /// <summary>
        /// The total element contained in the shoppingCart obj
        /// </summary>
        public int Count { get; set; }
    }
}
