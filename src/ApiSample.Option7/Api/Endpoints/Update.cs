using ApiSample.Domain.Customers.Specs;

namespace ApiSample.Api.Endpoints;

[ApiController]
public class Update : ControllerBase
{
    [HttpPut("api/customers/{id}")]
    public async Task<ActionResult<CustomerModel>> UpdateCustomer(int id, CustomerUpdateModel updateModel, [FromServices] IMediator mediator, CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(new UpdateCustomerRequest(id, updateModel), cancellationToken));
}

public record UpdateCustomerRequest(int Id, CustomerUpdateModel UpdateModel) : IRequest<CustomerModel>;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerRequest, CustomerModel>
{
    private readonly IRepository<Customer> _repository;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(IRepository<Customer> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomerModel> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var spec = new CustomerByIdSpec(request.Id);
        var customer = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (customer is null)
        {
            throw new KeyNotFoundException($"The customer with id: {request.Id} is not found!");
        }

        customer.Update(request.UpdateModel.FirstName, request.UpdateModel.LastName);

        await _repository.UpdateAsync(customer, cancellationToken);

        var result = _mapper.Map<CustomerModel>(customer);

        return result;
    }
}
