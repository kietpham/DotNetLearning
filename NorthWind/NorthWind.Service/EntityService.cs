using Microsoft.EntityFrameworkCore;
using NorthWind.Data;
using NorthWind.Models.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Service
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : EntityBase
    {
        private readonly NorthwindContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EntityService(NorthwindContext context) {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Exist(params object[] keys)
        {
            TEntity entity = _dbSet.Find(keys);
            return (entity != null);
        }

        public bool Exists(int id)
        {
            return _dbSet.Any(x => x.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        // async
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> GetByKeyAsync(params object[] keys)
        {
            return await _dbSet.FindAsync(keys);
        }

        // Not ready
        public async Task<IEnumerable<TEntity>> GetByParamsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> SaveAsync(TEntity entityToSave)
        {
            if (entityToSave.State == Models.Domain.State.Added) {
                _dbSet.Add(entityToSave);
            } else if (entityToSave.State == Models.Domain.State.Deleted) {
                _dbSet.Remove(entityToSave);
            } else if (entityToSave.State == Models.Domain.State.Modified) {
                _dbSet.Update(entityToSave);
            }

            if (entityToSave.State != Models.Domain.State.Unchanged) {
                await _context.SaveChangesAsync();
                entityToSave.State = Models.Domain.State.Unchanged;
            }

            return entityToSave;
        }

        private bool displosedValue;

        protected virtual void Dispose(bool disposing) {
            if (!displosedValue) {
                if (disposing) {
                    _context.Dispose();
                }
                displosedValue = true;
            }
        }
    }
}
