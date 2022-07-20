using ApiSample.Domain.Customers.Specs;

namespace ApiSample.Api.Endpoints;

[ApiController]
public class Delete : ControllerBase
{
    [HttpDelete("api/customers/{id}")]
    public async Task<ActionResult> DeleteCustomer(int id, [FromServices] IMediator mediator, CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(new DeleteCustomerRequest(id), cancellationToken));
}

public record DeleteCustomerRequest(int Id) : IRequest<Unit>;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerRequest, Unit>
{
    private readonly IRepository<Customer> _repository;
    private readonly IMapper _mapper;

    public DeleteCustomerHandler(IRepository<Customer> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        var spec = new CustomerByIdSpec(request.Id);
        var customer = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (customer is null)
        {
            throw new KeyNotFoundException($"The customer with id: {request.Id} is not found!");
        }

        await _repository.DeleteAsync(customer, cancellationToken);
        return Unit.Value;
    }
}
