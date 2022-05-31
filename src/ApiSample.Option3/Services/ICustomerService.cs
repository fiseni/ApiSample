namespace ApiSample.Services;

public interface ICustomerService
{
    Task<CustomerModel> AddCustomerAsync(CustomerCreateModel createModel, CancellationToken cancellationToken = default);
    Task DeleteCustomerAsync(int id, CancellationToken cancellationToken = default);
    Task<CustomerModel> GetCustomerAsync(int id, CancellationToken cancellationToken = default);
    Task<List<CustomerModel>> GetCustomersAsync(CancellationToken cancellationToken = default);
    Task<CustomerModel> UpdateCustomerAsync(int id, CustomerUpdateModel updateModel, CancellationToken cancellationToken = default);
}