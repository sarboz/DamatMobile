using System.Collections.Generic;
using System.Threading.Tasks;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Abstractions.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task SyncProducts(List<Product> dtos);
    }
}