using Microsoft.EntityFrameworkCore;
using PMS.API.Data;
using PMS.Storage.Models;

namespace PMS.Storage.Repository
{
    public class GenericRepository<TEntity, TPrimaryKey> : IGenericRepository<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
    {
        private readonly PMSDbContext _context;
        private DbSet<TEntity> _dbSet;

        public GenericRepository(PMSDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task DeleteAsync(TPrimaryKey entityId) 
        {
            var result = await _dbSet.FindAsync(entityId);
            if (result != null)
            {
                _dbSet.Remove(result);
            }
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                return _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<TEntity?> GetByIdAsync(TPrimaryKey id)
        {
            try
            {
                var result = await _dbSet.FindAsync(id);
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IQueryable<TEntity> GetQuery()
        {
            try
            {
                return _dbSet.AsQueryable();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            try
            {
                var result = await _dbSet.AddAsync(entity);
                return result.Entity;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                return Task.Run<TEntity>(() =>
                {
                    var result = _dbSet.Update(entity);
                    return result.Entity;
                });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

    }
}
