using ApiSample.Domain.Customers.Specs;

namespace ApiSample.Services;

public class ListCustomerHandler
{
    private readonly IReadRepository<Customer> _readRepository;
    private readonly IMapper _mapper;

    public ListCustomerHandler(IReadRepository<Customer> readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<List<CustomerModel>> GetCustomersAsync(CancellationToken cancellationToken = default)
    {
        var spec = new CustomerSpec();
        var result = await _readRepository.ProjectToListAsync<CustomerModel>(spec, cancellationToken);

        return result;
    }
}
