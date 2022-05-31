namespace ApiSample.Services;

public class DeleteCustomerHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public DeleteCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task DeleteCustomerAsync(int id, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetCustomerAsync(id, cancellationToken);

        if (customer is null)
        {
            throw new KeyNotFoundException($"The customer with id: {id} is not found!");
        }

        await _customerRepository.DeleteCustomerAsync(customer, cancellationToken);
    }
}
