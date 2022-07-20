namespace ApiSample.Api.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Address, AddressModel>();
        CreateMap<Customer, CustomerModel>();
    }
}
