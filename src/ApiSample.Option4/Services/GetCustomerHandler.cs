namespace ApiSample.Services;

public class GetCustomerHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerModel> GetCustomerAsync(int id, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetCustomerAsync(id, cancellationToken);

        if (customer is null)
        {
            throw new KeyNotFoundException($"The customer with id: {id} is not found!");
        }

        var result = _mapper.Map<CustomerModel>(customer);

        return result;
    }
}
