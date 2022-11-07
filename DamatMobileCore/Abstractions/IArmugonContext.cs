using System.Linq;
using System.Threading.Tasks;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Abstractions
{
    public interface IArmugonContext
    {
        void Add(object entity);
        void Update(object entity);
        void Remove(object entity);
        void RemoveAll<T>() where T : BaseEntity;
        IQueryable<T> GetDbSet<T>() where T : BaseEntity;
        Task<int> SaveChangesAsync();
    }
}