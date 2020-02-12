﻿using Shop.Application.Infrastructure;

namespace Shop.Application.Cart
{
    public class GetCustomerInformation
    {
        private readonly ISessionManager _sessionManager;

        public GetCustomerInformation(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public class Response
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
            var customerInformation = _sessionManager.GetCustomerInformation();

            if(customerInformation == null)
            {
                return null;
            }

            return new Response
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
            };
        }

    }
}
