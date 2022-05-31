using ApiSample.Domain;
using MediatR;

namespace ApiSample.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerModel>>> List(CancellationToken cancellationToken = default)
    {
        var request = new ListCustomerRequest();
        var result = await _mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerModel>> Get(int id, CancellationToken cancellationToken = default)
    {
        var request = new GetCustomerRequest(id);
        var result = await _mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerModel>> Add(CustomerCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var request = new CreateCustomerRequest(createModel);
        var result = await _mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerModel>> Update(int id, CustomerUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var request = new UpdateCustomerRequest(id, updateModel);
        var result = await _mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        var request = new DeleteCustomerRequest(id);
        var result = await _mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
