using System;
using System.Threading.Tasks;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Abstractions.Repositories
{
    public interface ICustomerRepository:IRepository<Customer>
    {
        Task<Customer> Get(Guid id);
    }
}