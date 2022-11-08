using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DamatMobile.Core.Dtos;
using Refit;

namespace DamatMobile.Core.Abstractions.Api
{
    public interface IApiEndpoints
    {
        [Get("/api/Customer/{customerId}")]
        public Task<CustomerDto> GetCustomerInfo(Guid customerId);

        [Get("/api/Brand/GetAllBrands")]
        public Task<List<BrandDto>> GetBrands();
        
        [Get("/api/News/GetNotExpired")]
        public Task<List<NewsDto>> GetNews();

        [Get("/api/PurchaseHistory/GetPurchaseHistory/{customerId}")]
        public Task<List<PurchaseHistoryDto>> GetPurchaseHistory(Guid customerId);

        [Get("/api/Customer/GetCustomersVirtualCard/{customerId}")]
        public Task<List<VirtualCardDto>> GetCustomersVirtualCard(Guid customerId);

        [Post("/api/Authorization/SendVerificationCode")]
        public Task SendVerificationCode([Header("phoneNumber")] string phoneNumber);

        [Get("/api/Authorization/ConfirmVerification")]
        public Task<CustomerDto> ConfirmCustomer([Header("code")] long code,
            [Header("phoneNumber")] string phoneNumber);

        [Post("/api/Notification/RegisterToken/{token}/{customerId}")]
        public Task RegisteredToken([Query] string token, [Query] Guid customerId);

        [Get("/api/Notification/GetCustomerNotifications/{customerId}")]
        public Task<List<NotificationDto>> GetCustomerNotifications([Query] Guid customerId);
    }
}