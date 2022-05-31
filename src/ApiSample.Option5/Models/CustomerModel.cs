namespace ApiSample.Models;

public class CustomerModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public List<AddressModel> Addresses { get; set; } = new();
}
