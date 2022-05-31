using ApiSample.Domain.Customers.Specs;

namespace ApiSample.Services;

public class GetCustomerHandler
{
    private readonly IReadRepository<Customer> _repository;
    private readonly IMapper _mapper;

    public GetCustomerHandler(IReadRepository<Customer> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomerModel> GetCustomerAsync(int id, CancellationToken cancellationToken = default)
    {
        var spec = new CustomerByIdSpec(id);
        var result = await _repository.ProjectToFirstOrDefaultAsync<CustomerModel>(spec, cancellationToken);

        if (result is null)
        {
            throw new KeyNotFoundException($"The customer with id: {id} is not found!");
        }

        return result;
    }
}
