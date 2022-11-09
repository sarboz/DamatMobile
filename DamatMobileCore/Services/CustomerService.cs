using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DamatMobile.Core.Abstractions;
using DamatMobile.Core.Abstractions.Api;
using DamatMobile.Core.Abstractions.Repositories;
using DamatMobile.Core.Models;
using Exception = System.Exception;

namespace DamatMobile.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IApiEndpoints _apiEndpoints;
        private readonly ICustomerRepository _customerRepository;
        private readonly INetworkConnectivity _networkConnectivity;
        private readonly IMapper mapper;
        private readonly IPurchaseHistoryRepository _purchaseHistoryRepository;
        private readonly IAppSettings _appSettings;
        private readonly IVirtualCardRepository _virtualCardRepository;

        public CustomerService(IApiEndpoints apiEndpoints
            , ICustomerRepository customerRepository
            , INetworkConnectivity networkConnectivity
            , IMapper mapper
            , IPurchaseHistoryRepository purchaseHistoryRepository
            , IAppSettings appSettings
            , IVirtualCardRepository virtualCardRepository)
        {
            _apiEndpoints = apiEndpoints;
            _customerRepository = customerRepository;
            _networkConnectivity = networkConnectivity;
            this.mapper = mapper;
            _purchaseHistoryRepository = purchaseHistoryRepository;
            _appSettings = appSettings;
            _virtualCardRepository = virtualCardRepository;
        }

        public async Task<List<VirtualCard>> GetCustomersCard()
        {
            if (!_networkConnectivity.IsConnected)
                return await _virtualCardRepository.GetAll();

            var customer = await _customerRepository.Get(_appSettings.UserId);
            if (customer is null)
                Console.WriteLine("Customer is null");

            var virtualCardDtos = await _apiEndpoints.GetCustomersVirtualCard(customer.Id);

            Console.WriteLine("card feched " + virtualCardDtos[0].Name);
            var virtualCards = mapper.Map<List<VirtualCard>>(virtualCardDtos);
            return virtualCards;
        }

        public async Task<List<PurchaseHistory>> GetCustomerPurchases()
        {
            if (!_networkConnectivity.IsConnected)
                return await _purchaseHistoryRepository.GetPurchaseHistory();
            try
            {
                var purchaseHistoryDto = await _apiEndpoints.GetPurchaseHistory(_appSettings.UserId);
                var purchaseHistories = mapper.Map<List<PurchaseHistory>>(purchaseHistoryDto);
                return purchaseHistories;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task SyncPurchase(List<PurchaseHistory> histories)
        {
            var purchaseHistories = await _purchaseHistoryRepository.GetAll();
            if (purchaseHistories.Any())
            {
                _purchaseHistoryRepository.RemoveAll();
                await _purchaseHistoryRepository.SaveChangesAsync();
            }

            histories.ForEach(_purchaseHistoryRepository.Add);
            await _purchaseHistoryRepository.SaveChangesAsync();
        }

        public async Task SyncCustomerCard(List<VirtualCard> cards)
        {
            var customer = await _customerRepository.Get(_appSettings.UserId);
            var virtualCards = await _virtualCardRepository.GetAll();
            if (virtualCards.Any())
            {
                _virtualCardRepository.RemoveAll();
                await _virtualCardRepository.SaveChangesAsync();
            }

            cards.ForEach(item =>
            {
                item.CustomerId = customer.Id;
                _virtualCardRepository.Add(item);
            });
            await _virtualCardRepository.SaveChangesAsync();
        }

        public Task<Customer> GetCurrentCustomer()
        {
            return _customerRepository.Get(_appSettings.UserId);
        }
    }
}