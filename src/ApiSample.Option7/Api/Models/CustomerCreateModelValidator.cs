namespace ApiSample.Api.Models;

public class CustomerCreateModelValidator : AbstractValidator<CustomerCreateModel>
{
    public CustomerCreateModelValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();

        RuleForEach(x => x.Addresses).SetValidator(new AddressCreateModelValidator());
    }
}
