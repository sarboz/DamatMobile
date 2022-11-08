using System.Collections.Generic;
using System.Threading.Tasks;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Abstractions.Services
{
    public interface IBrandService
    {
        Task<List<Brand>> GetBrands();
        Task SyncBrands(List<Brand> brands);
    }
}