using System.Threading.Tasks;
using DamatMobile.Core.Dtos;

namespace DamatMobile.Core.Abstractions
{
    public interface IAuthorizationService
    {
        Task SendVerificationCode(string phoneNumber);
        Task<CustomerDto> ConfirmUser(int code,string phoneNumber);
        Task RegisterUser(CustomerDto customer);
        Task RegisterToken();
        void Logout();
    }
}