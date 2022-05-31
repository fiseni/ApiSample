using ApiSample.Domain.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace ApiSample.Domain.Customers;

public class Customer : IAggregateRoot
{
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    private readonly List<Address> _addresses = new();
    public IEnumerable<Address> Addresses => _addresses.AsEnumerable();

    public Customer(string firstName, string lastName)
    {
        Update(firstName, lastName);
    }

    public void AddAddress(Address address)
    {
        ArgumentNullException.ThrowIfNull(address);

        _addresses.Add(address);
    }

    [MemberNotNull(nameof(FirstName), nameof(LastName))]
    public void Update(string firstName, string lastName)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
    }
}
