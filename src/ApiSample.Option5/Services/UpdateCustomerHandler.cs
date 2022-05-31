using ApiSample.Domain.Customers.Specs;

namespace ApiSample.Services;

public class UpdateCustomerHandler
{
    private readonly IRepository<Customer> _repository;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(IRepository<Customer> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomerModel> UpdateCustomerAsync(int id, CustomerUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var spec = new CustomerByIdSpec(id);
        var customer = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (customer is null)
        {
            throw new KeyNotFoundException($"The customer with id: {id} is not found!");
        }

        customer.Update(updateModel.FirstName, updateModel.LastName);

        await _repository.UpdateAsync(customer, cancellationToken);

        var result = _mapper.Map<CustomerModel>(customer);

        return result;
    }
}
