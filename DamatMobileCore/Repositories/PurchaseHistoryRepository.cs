using System.Collections.Generic;
using System.Threading.Tasks;
using DamatMobile.Core.Abstractions;
using DamatMobile.Core.Abstractions.Repositories;
using DamatMobile.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DamatMobile.Core.Repositories
{
    public class PurchaseHistoryRepository : BaseRepository<PurchaseHistory>, IPurchaseHistoryRepository
    {
        private readonly IArmugonContext _context;

        public PurchaseHistoryRepository(IArmugonContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<PurchaseHistory>> GetPurchaseHistory()
        {
            return _context.GetDbSet<PurchaseHistory>().Include(history => history.PurchaseDetails).ToListAsync();
        }
    }
}