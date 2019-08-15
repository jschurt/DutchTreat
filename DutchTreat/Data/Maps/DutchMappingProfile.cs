using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data.Maps
{
    public class DutchMappingProfile : Profile
    {
        public DutchMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(ovm => ovm.Id))
                .ReverseMap(); //Aqui criamos o mapping para OrderViewModel para Order


            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();

        }


    } //class
} //namespace
