namespace ApiSample.Services;

public class CreateCustomerHandler
{
    private readonly IRepository<Customer> _repository;
    private readonly IMapper _mapper;

    public CreateCustomerHandler(IRepository<Customer> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<CustomerModel> AddCustomerAsync(CustomerCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var customer = new Customer(createModel.FirstName, createModel.LastName);

        foreach (var address in createModel.Addresses)
        {
            customer.AddAddress(new(address.Street));
        }

        await _repository.AddAsync(customer, cancellationToken);

        var result = _mapper.Map<CustomerModel>(customer);

        return result;
    }

}
