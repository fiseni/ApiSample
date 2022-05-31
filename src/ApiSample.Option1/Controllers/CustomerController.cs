namespace ApiSample.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public CustomerController(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerModel>>> List(CancellationToken cancellationToken = default)
    {
        var customers = await _dbContext.Customers
            .Include(x => x.Addresses)
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<CustomerModel>>(customers);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerModel>> Get(int id, CancellationToken cancellationToken = default)
    {
        var customer = await _dbContext.Customers
            .Include(x => x.Addresses)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null)
        {
            return NotFound();
        }

        var result = _mapper.Map<CustomerModel>(customer);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerModel>> Add(CustomerCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var customer = new Customer(createModel.FirstName, createModel.LastName);

        foreach (var address in createModel.Addresses)
        {
            customer.AddAddress(new(address.Street));
        }

        _dbContext.Customers.Add(customer);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<CustomerModel>(customer);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerModel>> Update(int id, CustomerUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var customer = await _dbContext.Customers
            .Include(x => x.Addresses)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null)
        {
            return NotFound();
        }

        customer.Update(updateModel.FirstName, updateModel.LastName);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<CustomerModel>(customer);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        var customer = await _dbContext.Customers
            .Include(x=>x.Addresses)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null)
        {
            return NotFound();
        }

        _dbContext.Remove(customer);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}
