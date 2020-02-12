using Shop.Domain.Models;
using System.Collections.Generic;

namespace Shop.Application.Infrastructure
{
    public interface ISessionManager
    {
        string GetId();
        void AddProduct(int stockId, int quantity);
        void RemoveProduct(int stockId, int quantity);
        List<CartProduct> GetCart();

        void AddCustomerInformation(CustomerInformation customer);
        CustomerInformation GetCustomerInformation();
    }
}
