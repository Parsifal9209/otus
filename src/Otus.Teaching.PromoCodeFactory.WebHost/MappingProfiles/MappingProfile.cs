using Mapster;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.WebHost.MappingProfiles
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PromoCode, PromoCodeShortResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Code, src => src.Code)
                .Map(dest => dest.ServiceInfo, src => src.ServiceInfo)
                .Map(dest => dest.BeginDate, src => src.BeginDate.ToString())
                .Map(dest => dest.EndDate, src => src.EndDate.ToString())
                .Map(dest => dest.PartnerName, src => src.PartnerName);

            config.NewConfig<GivePromoCodeRequest, PromoCode>()
                .Map(dest => dest.Code, src => src.PromoCode)
                .Map(dest => dest.ServiceInfo, src => src.ServiceInfo)
                .Map(dest => dest.PartnerName, src => src.PartnerName)
                .Map(dest => dest.Preference, src => new Preference() { Name = src.Preference});
        }
    }
}
