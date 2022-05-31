namespace ApiSample.Services;

public record CreateCustomerRequest(CustomerCreateModel CreateModel) : IRequest<CustomerModel>;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerRequest, CustomerModel>
{
    private readonly IRepository<Customer> _repository;
    private readonly IMapper _mapper;

    public CreateCustomerHandler(IRepository<Customer> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomerModel> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.CreateModel.FirstName, request.CreateModel.LastName);

        foreach (var address in request.CreateModel.Addresses)
        {
            customer.AddAddress(new(address.Street));
        }

        await _repository.AddAsync(customer, cancellationToken);

        var result = _mapper.Map<CustomerModel>(customer);

        return result;
    }
}
