using System.Collections.Generic;
using System.Threading.Tasks;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Abstractions.Repositories
{
    public interface IPurchaseHistoryRepository:IRepository<PurchaseHistory>
    {
        Task<List<PurchaseHistory>> GetPurchaseHistory();
    }
}