using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Payment.Presenter.Mappers.Data;

public class PaymentInfoMapper : Profile
{

    public PaymentInfoMapper()
    {
        CreateMap<PaymentInfoDto, PaymentInfoData>()
            .ForPath(d => d.Order!.Id, o => o.MapFrom(s => s.OrderId))
            .ForMember(d => d.PaymentData, o => o.MapFrom(s => JsonConvert.SerializeObject(s.PaymentData)))
            .ReverseMap()
            .ForMember(d => d.PaymentData, o => o.MapFrom(s => JsonConvert.DeserializeObject<JObject>(s.PaymentData!)));
    }

}
