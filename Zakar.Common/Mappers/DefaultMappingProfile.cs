﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.Common.Mappers
{
   public class DefaultMappingProfile : Profile
    {
       protected override void Configure()
       {
           Mapper.CreateMap<Zone, ZoneViewModel>()
                 .ForSourceMember(m => m.Groups, c => c.Ignore());
           Mapper.CreateMap<ZoneViewModel, Zone>()
                 .ForMember(m => m.Groups, c => c.Ignore());
           Mapper.CreateMap<Group, GroupViewModel>()
                 .ForSourceMember(m => m.Zone, c => c.Ignore())
                 .ForSourceMember(m => m.Churches, c => c.Ignore());
           Mapper.CreateMap<GroupViewModel, Group>()
                 .ForMember(m => m.Zone, c => c.Ignore())
                 .ForMember(m => m.Churches, c => c.Ignore());
           Mapper.CreateMap<ChurchViewModel, Church>()
                 .ForMember(m => m.Group, c => c.Ignore())
                 .ForMember(m => m.PCFs, c => c.Ignore())
                 .ForMember(m => m.Partners, c => c.Ignore());
           Mapper.CreateMap<Church, ChurchViewModel>()
               .ForSourceMember(m => m.Group, c => c.Ignore())
                 .ForSourceMember(m => m.PCFs, c => c.Ignore())
                 .ForSourceMember(m => m.Partners, c => c.Ignore());

           Mapper.CreateMap<CurrencyViewModel, Currency>()
               .ForMember(m => m.Partnerships, c => c.Ignore());
           Mapper.CreateMap<Currency, CurrencyViewModel>()
               .ForSourceMember(m => m.Partnerships, c => c.Ignore());

           Mapper.CreateMap<PartnershipArmViewModel, PartnershipArm>()
               .ForMember(m => m.NonValidatedPartnershipRecords, c => c.Ignore())
               .ForMember(m => m.Partnerships, c => c.Ignore());
           Mapper.CreateMap<PartnershipArm, PartnershipArmViewModel>()
                .ForSourceMember(m => m.NonValidatedPartnershipRecords, c => c.Ignore())
               .ForSourceMember(m => m.Partnerships, c => c.Ignore());
       }
    }
}