using System.Collections.Generic;
using System.Threading.Tasks;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Abstractions.Services
{
    public interface INewsService
    {
        Task<List<News>> GetNews();
        Task SyncNews(List<News> news);
    }
}