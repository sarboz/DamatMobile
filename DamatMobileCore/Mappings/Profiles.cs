using AutoMapper;
using DamatMobile.Core.Dtos;
using DamatMobile.Core.Models;

namespace DamatMobile.Core.Mappings
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<PurchaseHistoryDto, PurchaseHistory>();
            CreateMap<PurchaseDetailDto, PurchaseDetail>();
            CreateMap<VirtualCardDto, VirtualCard>();
        }
    }
}