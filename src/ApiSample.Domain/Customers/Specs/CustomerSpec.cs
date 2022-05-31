using Ardalis.Specification;

namespace ApiSample.Domain.Customers.Specs;

public class CustomerSpec : Specification<Customer>
{
    public CustomerSpec()
    {
        Query.Include(x => x.Addresses);
    }
}
