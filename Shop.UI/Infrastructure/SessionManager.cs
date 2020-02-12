using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.UI.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {

            _session = httpContextAccessor.HttpContext.Session;
        }

        public void AddCustomerInformation(CustomerInformation customer)
        {
            var stringObject = JsonConvert.SerializeObject(customer);

            _session.SetString("customer-info", stringObject);
        }

        public void AddProduct(CartProduct cartProduct)
        {

            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");

            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }

            if (cartList.Any(x => x.StockId == cartProduct.StockId))
            {
                cartList.Find(x => x.StockId == cartProduct.StockId).Quantity += cartProduct.Quantity;
            }
            else
            {
                cartList.Add(cartProduct);
               
            }

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);


        }

        public IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector)
        {
            var stringObject = _session.GetString("cart");

            if (string.IsNullOrEmpty(stringObject))
                return new List<TResult>();

            var cartList = JsonConvert.DeserializeObject<IEnumerable<CartProduct>>(stringObject);

            return cartList.Select(selector);


        }

        public CustomerInformation GetCustomerInformation()
        {
            var stringObject = _session.GetString("customer-info");

            if (string.IsNullOrEmpty(stringObject))
                return null;

            var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(stringObject);

            return customerInformation;
        }

        public string GetId() => _session.Id;

        public void RemoveProduct(int stockId, int quantity)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");

            if (string.IsNullOrEmpty(stringObject)) return;

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            if (!cartList.Any(x => x.StockId == stockId)) return;

            var product = cartList.First(x => x.StockId == stockId);

            product.Quantity -= quantity;

            if(product.Quantity <= 0)
            {
                cartList.Remove(product);
            }

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);
        }
    }
}
