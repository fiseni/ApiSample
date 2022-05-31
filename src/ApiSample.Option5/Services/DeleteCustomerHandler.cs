using ApiSample.Domain.Customers.Specs;

namespace ApiSample.Services;

public class DeleteCustomerHandler
{
    private readonly IRepository<Customer> _repository;
    private readonly IMapper _mapper;

    public DeleteCustomerHandler(IRepository<Customer> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task DeleteCustomerAsync(int id, CancellationToken cancellationToken = default)
    {
        var spec = new CustomerByIdSpec(id);
        var customer = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (customer is null)
        {
            throw new KeyNotFoundException($"The customer with id: {id} is not found!");
        }

        await _repository.DeleteAsync(customer, cancellationToken);
    }
}
