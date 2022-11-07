using System.Collections.Generic;
using System.Threading.Tasks;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Abstractions
{
    public interface ICustomerService
    {
        public Task<List<VirtualCard>> GetCustomersCard();
        public Task<List<PurchaseHistory>> GetCustomerPurchases();
        public Task<Customer> GetCurrentCustomer();
        Task SyncCustomerCard(List<VirtualCard> cards);
        Task SyncPurchase(List<PurchaseHistory> histories);
    }
}