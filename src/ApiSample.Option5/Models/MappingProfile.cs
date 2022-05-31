namespace ApiSample.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Address, AddressModel>();
        CreateMap<Customer, CustomerModel>();
    }
}
