using AutoMapper;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Data.Model;

namespace Restaurant.Payment.Presenter.Mappers.Data;

public class OrderItemMapper : Profile
{

    public OrderItemMapper()
    {
        CreateMap<OrderItemDto, OrderItemData>()
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Nome))
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Tipo))
            .ForMember(d => d.Price, o => o.MapFrom(s => s.Preco))
            .ReverseMap();
    }

}
