using Shop.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.Cart
{
    [Service]
    public class GetOrder
    {
        private ISessionManager _sessionManager;

        public GetOrder(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public class Response
        {
            public IEnumerable<Product> Products { get; set; }
            public CustomerInformation CustomerInformation { get; set; }

            public int GetTotalCharge() => Products.Sum(x => x.Price * x.Quantity);
        }

        public class Product
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public int StockId { get; set; }
            public int Price { get; set; }
            public string PhotoUrl { get; set; }
        }

        public class CustomerInformation
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
        }

        public Response Do()
        {
            var listOfProducts = _sessionManager
                .GetCart(x => new Product
                {
                    ProductId = x.ProductId,
                    StockId = x.StockId,
                    Price = (int)(x.Price * 100),
                    Quantity = x.Quantity,
                    PhotoUrl = x.PhotoUrl
                });

            var customerInformation = _sessionManager.GetCustomerInformation();

            return new Response
            {
                Products = listOfProducts,
                CustomerInformation = new CustomerInformation
                {
                    FirstName = customerInformation.FirstName,
                    LastName = customerInformation.LastName,
                    Email = customerInformation.Email,
                    PhoneNumber = customerInformation.PhoneNumber,
                    Address1 = customerInformation.Address1,
                    Address2 = customerInformation.Address2,
                    City = customerInformation.City,
                    State = customerInformation.State,
                    ZipCode = customerInformation.ZipCode
                }
            };
        }
    }
}
