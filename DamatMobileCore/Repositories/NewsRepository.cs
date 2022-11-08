using DamatMobile.Core.Abstractions;
using DamatMobile.Core.Abstractions.Repositories;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Repositories
{
    public class NewsRepository:BaseRepository<News>,INewsRepository
    {
        public NewsRepository(IArmugonContext context) : base(context)
        {
        }
    }
}