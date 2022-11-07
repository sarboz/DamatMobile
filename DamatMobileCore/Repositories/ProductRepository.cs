using DamatMobile.Core.Abstractions;
using DamatMobile.Core.Abstractions.Repositories;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Repositories
{
    public class ProductRepository:BaseRepository<Product>,IProductRepository
    {
        public ProductRepository(IArmugonContext context) : base(context)
        {
        }
    }
}