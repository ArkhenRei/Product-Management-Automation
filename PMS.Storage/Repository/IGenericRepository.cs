using PMS.Storage.Models;

namespace PMS.Storage.Repository
{
    public interface IGenericRepository<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(TPrimaryKey id);    

        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TPrimaryKey entityId);
        
        Task SaveChangesAsync();

        IQueryable<TEntity> GetQuery();


    }
}
