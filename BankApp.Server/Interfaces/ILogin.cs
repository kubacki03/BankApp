using BankApp.Server.DTO;

namespace BankApp.Server.Interfaces
{
    public interface ILogin
    {
        Task<string?> LoginAsync(LoginModelRequest modelRequest, CancellationToken cancellationToken = default);
    }
}
