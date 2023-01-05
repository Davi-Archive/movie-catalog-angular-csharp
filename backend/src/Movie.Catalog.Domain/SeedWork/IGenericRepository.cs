namespace Movie.Catalog.Domain.SeedWork
{
    public interface IGenericRepository<TAggregate> : IRepository
    {
        public Task Insert(TAggregate aggregate, CancellationToken cancellationToken);
    }
}
