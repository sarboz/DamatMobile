using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DamatMobile.Core.Abstractions;
using DamatMobile.Core.Abstractions.Api;
using DamatMobile.Core.Abstractions.Repositories;
using DamatMobile.Core.Abstractions.Services;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IApiEndpoints _apiEndpoints;
        private readonly INetworkConnectivity _networkConnectivity;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IApiEndpoints apiEndpoints,
            INetworkConnectivity networkConnectivity, IMapper mapper )
        {
            _productRepository = productRepository;
            _apiEndpoints = apiEndpoints;
            _networkConnectivity = networkConnectivity;
            this.mapper = mapper;
        }

        public async Task<List<Product>> GetProducts()
        { 
            if (!_networkConnectivity.IsConnected) return await _productRepository.GetAll();

            var productDtos = await _apiEndpoints.GetProducts();
            var products = mapper.Map<List<Product>>(productDtos);
            Console.WriteLine("products count" + products.Count);
            return products;
        }

        public async Task SyncProducts(List<Product> dtos)
        {
            var products = await _productRepository.GetAll();
            if (products.Any())
            {
                _productRepository.RemoveAll();
                await _productRepository.SaveChangesAsync();
            }
            Console.WriteLine("products added" + dtos.Count);
            dtos.ForEach(_productRepository.Add);
            await _productRepository.SaveChangesAsync();
        }
    }
}