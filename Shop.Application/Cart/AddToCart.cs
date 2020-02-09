using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class AddToCart
    {
        private ISession _session;
        private ApplicationDbContext _ctx;

        public AddToCart(ISession session, ApplicationDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            var stockToHold = _ctx.Stock.Where(x => x.Id == request.StockId).FirstOrDefault();

            if(stockToHold.Quantity < request.Quantity)
            {
                return false;
            }

            _ctx.StockOnHold.Add(new StockOnHold
            {
                StockId = stockToHold.Id,
                Quantity = request.Quantity,
                Expiration = DateTime.Now.AddMinutes(20)
            });

            stockToHold.Quantity -= request.Quantity;

            await _ctx.SaveChangesAsync();

            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");

            if(!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }

            if(cartList.Any(x => x.StockId == request.StockId))
            {
                cartList.Find(x => x.StockId == request.StockId).Quantity += request.Quantity;
            }
            else
            {
                cartList.Add(new CartProduct
                {
                    StockId = request.StockId,
                    Quantity = request.Quantity
                });
            }

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);

            return true;
        }

    }
}
