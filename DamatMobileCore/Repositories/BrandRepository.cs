using DamatMobile.Core.Abstractions;
using DamatMobile.Core.Abstractions.Repositories;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Repositories
{
    public class BrandRepository:BaseRepository<Brand>,IBrandRepository
    {
        public BrandRepository(IArmugonContext context) : base(context)
        {
        }
    }
}