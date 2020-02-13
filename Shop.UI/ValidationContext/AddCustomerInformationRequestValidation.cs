using FluentValidation;
using Shop.Application.Cart;

namespace Shop.UI.ValidationContext
{
    public class AddCustomerInformationRequestValidation : AbstractValidator<AddCustomerInformation.Request>
    {
        public AddCustomerInformationRequestValidation()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotNull().MinimumLength(10);
            RuleFor(x => x.Address1).NotNull();
            RuleFor(x => x.City).NotNull();
            RuleFor(x => x.State).NotNull();
            RuleFor(x => x.ZipCode).NotNull();
        }
    }
}
