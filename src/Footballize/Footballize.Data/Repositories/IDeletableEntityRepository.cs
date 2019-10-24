namespace Footballize.Data.Repositories
{
    using System.Linq;
    using Models.Interfaces;

    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();
        
        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}