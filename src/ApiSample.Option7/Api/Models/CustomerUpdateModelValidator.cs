namespace ApiSample.Api.Models;

public class CustomerUpdateModelValidator : AbstractValidator<CustomerUpdateModel>
{
    public CustomerUpdateModelValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}
