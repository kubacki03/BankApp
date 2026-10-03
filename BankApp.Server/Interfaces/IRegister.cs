using BankApp.Server.DTO;

namespace BankApp.Server.Interfaces
{
    public interface IRegister
    {
        Task<bool> RegisterAsync(RegisterModelRequest modelRequest, CancellationToken cancellationToken = default);
    }
}
