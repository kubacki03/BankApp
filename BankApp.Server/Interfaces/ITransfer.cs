using BankApp.Server.DTO;

namespace BankApp.Server.Interfaces
{
    public interface ITransfer
    {
        Task SendTransferAsync(TransferModelRequest request, CancellationToken cancellationToken = default);

        Task<byte[]?> GenerateConfirmationAsync(int transferId, string email, CancellationToken cancellationToken = default);
    }
}
