using ApiSample.Domain.Contracts;

namespace ApiSample.Infrastructure;

public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
{
    public Repository(AppDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
    }
}