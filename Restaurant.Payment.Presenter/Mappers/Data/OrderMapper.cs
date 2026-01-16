using AutoMapper;
using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Data.Model;

namespace Restaurant.Payment.Presenter.Mappers.Data;

public class OrderMapper : Profile
{

    public OrderMapper()
    {
        CreateMap<OrderDto, OrderData>()
            .ForPath(d => d.Cliente, o => o.MapFrom(s => s.Cliente!.Id))
            .ReverseMap()
            .ForPath(d => d.Cliente!.Id, o => o.MapFrom(s => s.Cliente));
    }

}
