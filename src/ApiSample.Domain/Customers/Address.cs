namespace ApiSample.Domain.Customers;

public class Address
{
    public int Id { get; private set; }
    public string Street { get; private set; }

    public int CustomerId { get; private set; }

    public Address(string street)
    {
        Street = street ?? throw new ArgumentNullException(nameof(street));
    }
}
