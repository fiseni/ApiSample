namespace ApiSample.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerModel>>> List(CancellationToken cancellationToken = default)
    {
        var result = await _customerService.GetCustomersAsync(cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerModel>> Get(int id, CancellationToken cancellationToken = default)
    {
        var result = await _customerService.GetCustomerAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerModel>> Add(CustomerCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var result = await _customerService.AddCustomerAsync(createModel, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerModel>> Update(int id, CustomerUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var result = await _customerService.UpdateCustomerAsync(id, updateModel, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        await _customerService.DeleteCustomerAsync(id, cancellationToken);

        return Ok();
    }
}
