using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.Presenter;
using Restaurant.Payment.Application.Interfaces.Repository;
using Restaurant.Payment.Data.Contexts;
using Restaurant.Payment.Data.Model;

namespace Restaurant.Payment.Data.Repositories;

public class OrderRepository(
    IMapper mapper,
    IDatePresenter presenter,
    PaymentContext context) : IOrderRepository
{

    public async Task<OrderDto> GetById(string id)
    {
        var entity = await context.Set<OrderData>()
            .Include(o => o.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);

        return mapper.Map<OrderDto>(entity);
    }

    public async Task<OrderDto> Create(OrderDto order)
    {
        order.Data = presenter.ToUtc(order.Data);

        var entity = mapper.Map<OrderData>(order);

        await context.Set<OrderData>().AddAsync(entity);
        await context.SaveChangesAsync();

        var dto = mapper.Map<OrderDto>(entity);

        dto.Data = presenter.ToTimeZone(dto.Data);

        return dto;
    }

    public async Task<OrderDto> Update(OrderDto order)
    {
        order.Data = presenter.ToUtc(order.Data);

        var entity = mapper.Map<OrderData>(order);

        context.Set<OrderData>().Update(entity);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();

        var dto = mapper.Map<OrderDto>(entity);

        dto.Data = presenter.ToTimeZone(dto.Data);

        return dto;
    }

}
