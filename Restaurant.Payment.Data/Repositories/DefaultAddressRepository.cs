using AutoMapper;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.Repository;
using Restaurant.Payment.Data.Contexts;
using Restaurant.Payment.Data.Model;

namespace Restaurant.Payment.Data.Repositories;

public class DefaultAddressRepository(
    IMapper mapper,
    PaymentContext context) : IDefaultAddressRepository
{

    public async Task<DefaultAddressDto?> GetById(int? id)
    {
        var data = await context.Set<DefaultAddressData>()
            .FindAsync(id);

        return mapper.Map<DefaultAddressDto>(data);
    }

}
