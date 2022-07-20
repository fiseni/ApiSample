using ApiSample.Domain.Contracts;

namespace ApiSample.Infrastructure;

public class ReadRepository<T> : RepositoryBase<T>, IReadRepository<T> where T : class
{
    public ReadRepository(AppDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
    }
}
