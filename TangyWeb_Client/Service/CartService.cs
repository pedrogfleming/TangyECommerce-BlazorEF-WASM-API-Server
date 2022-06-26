﻿using Blazored.LocalStorage;
using Tangy_Common;
using TangyWeb_Client.Service.IService;
using TangyWeb_Client.ViewModel;

namespace TangyWeb_Client.Service
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        public event Action OnChange;
        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public async Task IncrementCart(ShoppingCart cartToAdd)
        {
            var cart = await _localStorage.GetItemAsync<List<ShoppingCart>>(SD.ShoppingCart);
            bool itemInCart = false;
            if(cart is null)
            {
                cart = new List<ShoppingCart>();
            }
            foreach (var obj in cart)
            {
                if(obj.ProductId == cartToAdd.ProductId && obj.ProductPriceId == cartToAdd.ProductPriceId)
                {
                    itemInCart = true;
                    obj.Count += cartToAdd.Count;
                }
            }
            if (!itemInCart)
            {
                cart.Add(new ShoppingCart()
                {
                    ProductId = cartToAdd.ProductId,
                    ProductPriceId = cartToAdd.ProductPriceId,
                    Count = cartToAdd.Count
                });
            }
            await _localStorage.SetItemAsync(SD.ShoppingCart, cart);
            OnChange.Invoke();
        }
        public async Task DecrementCart(ShoppingCart cartToDecrement)
        {
            var cart = await _localStorage.GetItemAsync<List<ShoppingCart>>(SD.ShoppingCart);
            //If count is 0 or 1 then we remove the item
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductId == cartToDecrement.ProductId && cart[i].ProductPriceId == cartToDecrement.ProductPriceId)
                {
                    if (cart[i].Count == 1 || cartToDecrement.Count == 0)
                    {
                        cart.Remove(cart[i]);
                    }
                    else
                    {
                        cart[i].Count -= cartToDecrement.Count;
                    }
                }
            }
            await _localStorage.SetItemAsync(SD.ShoppingCart, cart);
            OnChange.Invoke();
        }
    }
}