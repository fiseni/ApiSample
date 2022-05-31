namespace ApiSample.Services;

public class CreateCustomerHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }


    public async Task<CustomerModel> AddCustomerAsync(CustomerCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var customer = new Customer(createModel.FirstName, createModel.LastName);

        foreach (var address in createModel.Addresses)
        {
            customer.AddAddress(new(address.Street));
        }

        await _customerRepository.AddCustomerAsync(customer, cancellationToken);

        var result = _mapper.Map<CustomerModel>(customer);

        return result;
    }

}
