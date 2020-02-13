﻿using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    [Service]
    public class CreateProduct
    {
        private IProductManager _productManager;

        public CreateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<Response> DoAsync(Request request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                PhotoUrl = request.PhotoUrl
            };

            if (await _productManager.CreateProduct(product) <= 0)
            {
                throw new Exception("Failed to create product");
            };

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PhotoUrl = product.PhotoUrl
            };
        }


        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string PhotoUrl { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string PhotoUrl { get; set; }
        }


    }
}
