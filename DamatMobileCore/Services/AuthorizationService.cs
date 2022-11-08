using System;
using System.Threading.Tasks;
using AutoMapper;
using DamatMobile.Core.Abstractions;
using DamatMobile.Core.Abstractions.Api;
using DamatMobile.Core.Abstractions.Repositories;
using DamatMobile.Core.Dtos;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IApiEndpoints _apiEndpoints;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAppSettings _appSettings;
        private readonly IMapper mapper;

        public AuthorizationService(IApiEndpoints apiEndpoints, ICustomerRepository customerRepository,
            IAppSettings appSettings,IMapper mapper)
        {
            _apiEndpoints = apiEndpoints;
            _customerRepository = customerRepository;
            _appSettings = appSettings;
            this.mapper = mapper;
        }

        public Task SendVerificationCode(string phoneNumber)
        {
            return _apiEndpoints.SendVerificationCode(phoneNumber);
        }

        public Task<CustomerDto> ConfirmUser(int code, string phoneNumber)
        {
            return _apiEndpoints.ConfirmCustomer(code, phoneNumber);
        }

        public Task RegisterUser(CustomerDto customerDto)
        {
            var customer = mapper.Map<Customer>(customerDto);
            _appSettings.UserId = customer.Id;
            _appSettings.UserName = $"{customer.FirstName} {customer.LastName}";
            _customerRepository.Add(customer);
            return _customerRepository.SaveChangesAsync();
        }

        public async Task RegisterToken()
        {
            if (string.IsNullOrEmpty(_appSettings.Token))
                return;
            if (_appSettings.IsTokenRegistered)
                return;
            try
            {
                await _apiEndpoints.RegisteredToken(_appSettings.Token, _appSettings.UserId);
                _appSettings.IsTokenRegistered = true;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void Logout()
        {
            _appSettings.UserId = Guid.Empty;
            _customerRepository.RemoveAll();
        }
    }
}