using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shop.Application.Cart;
using Shop.Application.Orders;
using Shop.Database;
using Stripe;

namespace Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public string PublicKey { get; }
        private ApplicationDbContext _ctx;

        public PaymentModel(IConfiguration configuration, ApplicationDbContext ctx)
        {
            PublicKey = configuration["Stripe:PublicKey"].ToString();
            _ctx = ctx;
        }

        public IActionResult OnGet()
        {
            
            var information = new GetCustomerInformation(HttpContext.Session).Do();

            if (information == null)
            {
                return RedirectToPage("/Checkout/CustomerInformation");
            }

            return Page();
        }


        public async Task<IActionResult> OnPost(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var CartOrder = new Application.Cart.GetOrder(HttpContext.Session, _ctx).Do();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = CartOrder.GetTotalCharge(),
                Description = "Shop Purchase",
                Currency = "usd",
                Customer = customer.Id
            });

            var sessionId = HttpContext.Session.Id;

            await new CreateOrder(_ctx).Do(new CreateOrder.Request
            {
                StripeReference = charge.Id,
                SessionId = sessionId,
                FirstName = CartOrder.CustomerInformation.FirstName,
                LastName = CartOrder.CustomerInformation.LastName,
                Email = CartOrder.CustomerInformation.Email,
                PhoneNumber = CartOrder.CustomerInformation.PhoneNumber,
                Address1 = CartOrder.CustomerInformation.Address1,
                Address2 = CartOrder.CustomerInformation.Address2,
                City = CartOrder.CustomerInformation.City,
                State = CartOrder.CustomerInformation.State,
                ZipCode = CartOrder.CustomerInformation.ZipCode,

                Stocks = CartOrder.Products.Select(x => new CreateOrder.Stock
                {
                    StockId = x.StockId,
                    Quantity = x.Quantity
                }).ToList()
            });

            return RedirectToPage("/Index");
        }
    }
}