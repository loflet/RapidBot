using AutoMapper;
using RapidBot.Service.DTOs;
using RapidBot.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RapidBot.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<CustomerDto, CustomerViewModel>().ReverseMap();
            CreateMap<OrderDto, OrderViewModel>().ReverseMap();
            CreateMap<OrderItemDto, OrderItemViewModel>().ReverseMap();
            CreateMap<ProductDto, ProductViewModel>().ReverseMap();
            CreateMap<SupplierDto, SupplierViewModel>().ReverseMap();
        }
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }        
    }
}