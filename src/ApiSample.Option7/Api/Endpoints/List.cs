using ApiSample.Domain.Customers.Specs;

namespace ApiSample.Api.Endpoints;

[ApiController]
public class List : ControllerBase
{
    [HttpGet("api/customers")]
    public async Task<ActionResult<List<CustomerModel>>> ListCustomers([FromServices] IMediator mediator, CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(new ListCustomerRequest(), cancellationToken));
}

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
