namespace ApiSample.Services;

public class ListCustomerHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public ListCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<List<CustomerModel>> GetCustomersAsync(CancellationToken cancellationToken = default)
    {
        var customers = await _customerRepository.GetCustomersAsync(cancellationToken);

        var result = _mapper.Map<List<CustomerModel>>(customers);

        return result;
    }
}
