using Restaurant.Payment.Application.DTO;

namespace Restaurant.Payment.Application.Interfaces.ExternalServices;

public interface IIdentificationService
{

    Task<ClientDto?> GetByCpf(string? cpf);

    Task<ClientDto?> GetById(string? id);

    Task<IEnumerable<ClientDto>> GetByIds(IEnumerable<string> ids);

}
