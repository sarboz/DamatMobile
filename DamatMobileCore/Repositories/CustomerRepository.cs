using System;
using System.Threading.Tasks;
using DamatMobile.Core.Abstractions;
using DamatMobile.Core.Abstractions.Repositories;
using DamatMobile.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DamatMobile.Core.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly IArmugonContext _context;

        public CustomerRepository(IArmugonContext context) : base(context)
        {
            _context = context;
        }

        public Task<Customer> Get(Guid id)
        {
            return _context.GetDbSet<Customer>().SingleAsync(customer => customer.id.Equals(id));
        }
    }
}