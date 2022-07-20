namespace ApiSample.Api.Models;

public class CustomerCreateModel
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public List<AddressCreateModel> Addresses { get; set; } = new();
}
