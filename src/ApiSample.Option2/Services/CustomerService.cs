namespace ApiSample.Services;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public CustomerService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<CustomerModel>> GetCustomersAsync(CancellationToken cancellationToken = default)
    {
        var customers = await _dbContext.Customers
            .Include(x => x.Addresses)
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<CustomerModel>>(customers);

        return result;
    }

    public async Task<CustomerModel> GetCustomerAsync(int id, CancellationToken cancellationToken = default)
    {
        var customer = await _dbContext.Customers
            .Include(x => x.Addresses)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null)
        {
            throw new KeyNotFoundException($"The customer with id: {id} is not found!");
        }

        var result = _mapper.Map<CustomerModel>(customer);

        return result;
    }

    public async Task<CustomerModel> AddCustomerAsync(CustomerCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var customer = new Customer(createModel.FirstName, createModel.LastName);

        foreach (var address in createModel.Addresses)
        {
            customer.AddAddress(new(address.Street));
        }

        _dbContext.Customers.Add(customer);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<CustomerModel>(customer);

        return result;
    }

    public async Task<CustomerModel> UpdateCustomerAsync(int id, CustomerUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var customer = await _dbContext.Customers
            .Include(x => x.Addresses)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null)
        {
            throw new KeyNotFoundException($"The customer with id: {id} is not found!");
        }

        customer.Update(updateModel.FirstName, updateModel.LastName);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<CustomerModel>(customer);

        return result;
    }

    public async Task DeleteCustomerAsync(int id, CancellationToken cancellationToken = default)
    {
        var customer = await _dbContext.Customers
            .Include(x => x.Addresses)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null)
        {
            throw new KeyNotFoundException($"The customer with id: {id} is not found!");
        }

        _dbContext.Remove(customer);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
