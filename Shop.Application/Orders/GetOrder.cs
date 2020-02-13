using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.Orders
{
    [Service]
    public class GetOrder
    {
        private readonly IOrderManager _orderManager;

        public GetOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public class Response
        {
            public string OrderRef { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }

            public IEnumerable<Product> Products { get; set; }

            public string TotalPrice { get; set; }
        }

        public class Product
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public int Quantity { get; set; }
            public string StockDescription { get; set; }
            public string PhotoUrl { get; set; }
        }

        public Response Do(string reference) => 
            _orderManager.GetOrderByReference(reference, Projection);

        private readonly static Func<Order, Response> Projection = (order) =>
            new Response
            {
                OrderRef = order.OrderRef,
                FirstName = order.FirstName,
                LastName = order.LastName,
                Email = order.Email,
                PhoneNumber = order.PhoneNumber,
                Address1 = order.Address1,
                Address2 = order.Address2,
                City = order.City,
                State = order.State,
                ZipCode = order.ZipCode,

                Products = order.OrderStocks.Select(y => new Product
                {
                    Name = y.Stock.Product.Name,
                    Description = y.Stock.Product.Description,
                    Price = $"$ {y.Stock.Product.Price.ToString("N2")}",
                    Quantity = y.Quantity,
                    StockDescription = y.Stock.Description,
                    PhotoUrl = y.Stock.Product.PhotoUrl
                }),

                TotalPrice = order.OrderStocks.Sum(y => y.Stock.Product.Price).ToString("N2")

            };
    }
}
