namespace ApiSample.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
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

    public async Task<CustomerModel> UpdateCustomerAsync(int id, CustomerUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetCustomerAsync(id, cancellationToken);

        if (customer is null)
        {
            throw new KeyNotFoundException($"The customer with id: {id} is not found!");
        }

        customer.Update(updateModel.FirstName, updateModel.LastName);

        await _customerRepository.UpdateCustomerAsync(customer, cancellationToken);

        var result = _mapper.Map<CustomerModel>(customer);

        return result;
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
