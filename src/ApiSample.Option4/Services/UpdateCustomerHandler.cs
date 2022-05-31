namespace ApiSample.Services;

public class UpdateCustomerHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
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
}
