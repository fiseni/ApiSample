using ApiSample.Domain.Customers.Specs;

namespace ApiSample.Services;

public record ListCustomerRequest() : IRequest<List<CustomerModel>>;

public class ListCustomerHandler : IRequestHandler<ListCustomerRequest, List<CustomerModel>>
{
    private readonly IReadRepository<Customer> _readRepository;
    private readonly IMapper _mapper;

    public ListCustomerHandler(IReadRepository<Customer> readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<List<CustomerModel>> Handle(ListCustomerRequest request, CancellationToken cancellationToken)
    {
        var spec = new CustomerSpec();
        var result = await _readRepository.ProjectToListAsync<CustomerModel>(spec, cancellationToken);

        return result;
    }
}
