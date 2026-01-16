using Restaurant.Payment.Application.DTO;

namespace Restaurant.Payment.Application.Interfaces.Repository;

public interface IDefaultAddressRepository
{

    Task<DefaultAddressDto?> GetById(int? id);

}
