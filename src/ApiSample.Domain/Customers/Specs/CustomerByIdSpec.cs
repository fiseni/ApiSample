using Ardalis.Specification;

namespace ApiSample.Domain.Customers.Specs;

public class CustomerByIdSpec : Specification<Customer>
{
    public CustomerByIdSpec(int id)
    {
        Query.Include(x => x.Addresses)
             .Where(x => x.Id == id);
    }
}
