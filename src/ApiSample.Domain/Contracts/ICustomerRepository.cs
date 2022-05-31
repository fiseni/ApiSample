using ApiSample.Domain.Customers;

namespace ApiSample.Domain.Contracts;

public interface ICustomerRepository
{
    Task<Customer> AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
    Task DeleteCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
    Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Customer>> GetCustomersAsync(CancellationToken cancellationToken = default);
    Task<Customer> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
}
