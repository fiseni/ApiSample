namespace ApiSample.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly CreateCustomerHandler _createCustomerHandler;
    private readonly UpdateCustomerHandler _updateCustomerHandler;
    private readonly GetCustomerHandler _getCustomerHandler;
    private readonly ListCustomerHandler _listCustomerHandler;
    private readonly DeleteCustomerHandler _deleteCustomerHandler;

    public CustomerController(CreateCustomerHandler createCustomerHandler,
                              UpdateCustomerHandler updateCustomerHandler,
                              GetCustomerHandler getCustomerHandler,
                              ListCustomerHandler listCustomerHandler,
                              DeleteCustomerHandler deleteCustomerHandler)
    {
        _createCustomerHandler = createCustomerHandler;
        _updateCustomerHandler = updateCustomerHandler;
        _getCustomerHandler = getCustomerHandler;
        _listCustomerHandler = listCustomerHandler;
        _deleteCustomerHandler = deleteCustomerHandler;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerModel>>> List(CancellationToken cancellationToken = default)
    {
        var result = await _listCustomerHandler.GetCustomersAsync(cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerModel>> Get(int id, CancellationToken cancellationToken = default)
    {
        var result = await _getCustomerHandler.GetCustomerAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerModel>> Add(CustomerCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var result = await _createCustomerHandler.AddCustomerAsync(createModel, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerModel>> Update(int id, CustomerUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var result = await _updateCustomerHandler.UpdateCustomerAsync(id, updateModel, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        await _deleteCustomerHandler.DeleteCustomerAsync(id, cancellationToken);

        return Ok();
    }
}
