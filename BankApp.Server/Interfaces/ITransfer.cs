using BankApp.Server.DTO;

namespace BankApp.Server.Interfaces
{
    public interface ITransfer
    {

        public void SendTransfer(TransferModelRequest request);

        public ICollection<TransferModelRequest> GetAccountTransfers(string accountNumber);
    }
}
