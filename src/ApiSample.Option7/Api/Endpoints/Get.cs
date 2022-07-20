using ApiSample.Domain.Customers.Specs;

namespace ApiSample.Api.Endpoints;

[ApiController]
public class Get : ControllerBase
{
    [HttpGet("api/customers/{id}")]
    public async Task<ActionResult<CustomerModel>> GetCustomer(int id, [FromServices] IMediator mediator, CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(new GetCustomerRequest(id), cancellationToken));
}

public record GetCustomerRequest(int Id) : IRequest<CustomerModel>;

public class GetCustomerHandler : IRequestHandler<GetCustomerRequest, CustomerModel>
{
    private readonly IReadRepository<Customer> _repository;
    private readonly IMapper _mapper;

    public GetCustomerHandler(IReadRepository<Customer> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomerModel> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
    {
        var spec = new CustomerByIdSpec(request.Id);
        var result = await _repository.ProjectToFirstOrDefaultAsync<CustomerModel>(spec, cancellationToken);

        if (result is null)
        {
            throw new KeyNotFoundException($"The customer with id: {request.Id} is not found!");
        }

        return result;
    }
}
