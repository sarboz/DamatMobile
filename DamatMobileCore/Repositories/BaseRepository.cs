using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DamatMobile.Core.Abstractions;
using DamatMobile.Core.Abstractions.Repositories;
using DamatMobile.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DamatMobile.Core.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly IArmugonContext _context;

        public BaseRepository(IArmugonContext context)
        {
            _context = context;
        }

        public Task<List<TEntity>> GetAll() => _context.GetDbSet<TEntity>().ToListAsync();

        public void Add(TEntity entity) => _context.Add(entity);

        public void Remove(TEntity entity) => _context.Remove(entity);

        public void RemoveAll() => _context.RemoveAll<TEntity>();

        public void Update(TEntity entity) => _context.Update(entity);

        public async Task<bool> SaveChangesAsync()
        {
            var changesCount = -1;
            var changes = ((DbContext)_context).ChangeTracker.Entries()
                .Where(entry => entry.State != EntityState.Unchanged).ToList();
            try
            {
                Console.WriteLine(((DbContext)_context).ChangeTracker.DebugView.LongView);
                changesCount = await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return changesCount != -1;
        }
    }
}