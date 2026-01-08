using AutoMapper;
using MongoDB.Driver;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.Repository;
using Restaurant.Payment.Data.Model;

namespace Restaurant.Payment.Data.Repositories;

public class OrderRepository(
    IMapper mapper,
    IMongoDatabase context) : IOrderRepository
{

    private readonly IMongoCollection<OrderData> collection =
        context.GetCollection<OrderData>("order");


    public async Task<OrderDto> Create(OrderDto order)
    {
        var entity = mapper.Map<OrderData>(order);

        await collection.InsertOneAsync(entity);

        return mapper.Map<OrderDto>(entity);
    }

    public async Task<OrderDto> Update(OrderDto order)
    {
        var entity = mapper.Map<OrderData>(order);

        await collection.ReplaceOneAsync(
            x => x.Id == entity.Id,
            entity);

        return mapper.Map<OrderDto>(entity);
    }

}
