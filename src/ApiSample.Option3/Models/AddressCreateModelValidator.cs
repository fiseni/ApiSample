namespace ApiSample.Models;

public class AddressCreateModelValidator : AbstractValidator<AddressCreateModel>
{
    public AddressCreateModelValidator()
    {
        RuleFor(x => x.Street).NotEmpty();
    }
}
