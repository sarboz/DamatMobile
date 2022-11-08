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
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IApiEndpoints _apiEndpoints;
        private readonly INetworkConnectivity _networkConnectivity;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IApiEndpoints apiEndpoints,
            INetworkConnectivity networkConnectivity, IMapper mapper )
        {
            _brandRepository = brandRepository;
            _apiEndpoints = apiEndpoints;
            _networkConnectivity = networkConnectivity;
            this._mapper = mapper;
        }

        public async Task<List<Brand>> GetBrands()
        { 
            if (!_networkConnectivity.IsConnected) return await _brandRepository.GetAll();

            var brandDtos = await _apiEndpoints.GetBrands();
            var brands = _mapper.Map<List<Brand>>(brandDtos);
            return brands;
        }

        public async Task SyncBrands(List<Brand> dtos)
        {
            var brands = await _brandRepository.GetAll();
            if (brands.Any())   
            {
                _brandRepository.RemoveAll();
                await _brandRepository.SaveChangesAsync();
            }
            dtos.ForEach(_brandRepository.Add);
            await _brandRepository.SaveChangesAsync();
        }
    }
}