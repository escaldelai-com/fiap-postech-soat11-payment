using AutoMapper;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Data.Model;

namespace Restaurant.Payment.Presenter.Mappers.Data;

public class OrderItemMapper : Profile
{

    public OrderItemMapper()
    {
        CreateMap<OrderItemDto, OrderItemData>()
            .ReverseMap();
    }

}
