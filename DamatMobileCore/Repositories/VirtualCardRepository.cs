using DamatMobile.Core.Abstractions;
using DamatMobile.Core.Abstractions.Repositories;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Repositories
{
    public class VirtualCardRepository:BaseRepository<VirtualCard>,IVirtualCardRepository
    {
        public VirtualCardRepository(IArmugonContext context) : base(context)
        {
        }
    }
}