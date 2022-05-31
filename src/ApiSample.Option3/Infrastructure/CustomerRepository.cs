namespace ApiSample.Infrastructure;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _dbContext;

    public CustomerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Customer>> GetCustomersAsync(CancellationToken cancellationToken = default)
    {
        var customers = await _dbContext.Customers
            .Include(x => x.Addresses)
            .ToListAsync(cancellationToken);

        return customers;
    }

    public async Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken = default)
    {
        var customer = await _dbContext.Customers
            .Include(x => x.Addresses)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        return customer;
    }

    public async Task<Customer> AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _dbContext.Customers.Add(customer);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return customer;
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);

        return customer;
    }

    public async Task DeleteCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _dbContext.Remove(customer);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
