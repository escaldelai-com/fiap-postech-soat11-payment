using AutoMapper;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Payment.Presenter.Mappers.Data;

public class DefaultAddressMapper : Profile
{

    public DefaultAddressMapper()
    {
        CreateMap<DefaultAddressData, DefaultAddressDto>()
            .ReverseMap();
    }

}
