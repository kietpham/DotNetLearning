using NorthWind.Models.Domain.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthWind.Service
{
    // generic interface, generic interface này chỉ cho phép TEntity là EntityBase theo where, nếu không thì sẽ lấy hết
    public interface IEntityService<TEntity> : IDisposable where TEntity: EntityBase
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetByKeyAsync(params object[] keys);

        Task<IEnumerable<TEntity>> GetByParamsAsync();

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> SaveAsync(TEntity entityToSave);

        bool Exists(int id);

        bool Exist(params object[] keys);
    }
}
