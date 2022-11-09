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
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IApiEndpoints _apiEndpoints;
        private readonly INetworkConnectivity _networkConnectivity;
        private readonly IMapper _mapper;

        public NewsService(INewsRepository newsRepository, IApiEndpoints apiEndpoints,
            INetworkConnectivity networkConnectivity, IMapper mapper )
        {
            _newsRepository = newsRepository;
            _apiEndpoints = apiEndpoints;
            _networkConnectivity = networkConnectivity;
            this._mapper = mapper;
        }

        public async Task<List<News>> GetNews()
        { 
            if (!_networkConnectivity.IsConnected) return await _newsRepository.GetAll();

            var news = await _apiEndpoints.GetNews();
            var newsList = _mapper.Map<List<News>>(news);
            return newsList;
        }

        public async Task SyncNews(List<News> dtos)
        {
            var brands = await _newsRepository.GetAll();
            if (brands.Any())   
            {
                _newsRepository.RemoveAll();
                await _newsRepository.SaveChangesAsync();
            }
            dtos.ForEach(_newsRepository.Add);
            await _newsRepository.SaveChangesAsync();
        }
    }
}