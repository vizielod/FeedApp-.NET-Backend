using AutoMapper;
//using FeedApp.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedApp.Bll.Entities;

namespace FeedApp.Api.Mapping
{
    public class MapperConfig
    {
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                /*cfg.CreateMap<Food, Dtos.Food>() //Fontos, hogy a BLL.Entities legyen using-olva es ne az Api.Dtos
                    .ForMember(dto => dto.EatingList, opt => opt.Ignore())
                    .AfterMap((f, dto, ctx) => dto.EatingList = f.FoodsForEatings.Select(ffe => ctx.Mapper.Map<Dtos.Eating>(ffe.Eating)).ToList());*/ //ez nehéz szülés volt és még mindig nem teljesen világos
                cfg.CreateMap<Food, Dtos.Food>();
                cfg.CreateMap<Dtos.Food, Food>();

                cfg.CreateMap<Eating, Dtos.Eating>()
                    .ForMember(dto => dto.FoodList, opt => opt.Ignore())
                    .AfterMap((e, dto, ctx) => dto.FoodList = e.FoodsForEatings.Select(ffe => ctx.Mapper.Map<Dtos.Food>(ffe.Food)).ToList());
                cfg.CreateMap<Dtos.Eating, Eating>();

                cfg.CreateMap<UserInfo, Dtos.UserInfo>();
                cfg.CreateMap<Dtos.UserInfo, UserInfo> ();

                cfg.CreateMap<User, Dtos.User>();
                cfg.CreateMap<Dtos.User, User>();

            });

            var mapper = config.CreateMapper();

            return mapper;
        }
    }
}
