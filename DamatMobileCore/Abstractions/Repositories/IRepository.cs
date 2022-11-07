using System.Collections.Generic;
using System.Threading.Tasks;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Abstractions.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAll();
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void RemoveAll();
        void Update(TEntity entity);
        Task<bool> SaveChangesAsync();
    }
}