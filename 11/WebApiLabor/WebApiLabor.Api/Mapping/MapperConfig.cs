﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLabor.Bll.Entities;

namespace WebApiLabor.Api.Mapping
{
    public class MapperConfig
    {
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, Dtos.Product>()
                    .ForMember(dto => dto.Orders, opt => opt.Ignore())
                    .AfterMap((p, dto, ctx) => dto.Orders = p.ProductOrders.Select(po => ctx.Mapper.Map<Dtos.Order>(po.Order)).ToList());
                cfg.CreateMap<Dtos.Product, Product>();

                cfg.CreateMap<Order, Dtos.Order>();
                cfg.CreateMap<Dtos.Order, Order>();

                cfg.CreateMap<Category, Dtos.Category>();
                cfg.CreateMap<Dtos.Category, Category>();
            });


            return config.CreateMapper();
        }
    }
}