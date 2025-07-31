using BankApp.Server.DTO;
using BankApp.Server.Models;

namespace BankApp.Server.Interfaces
{
    public interface ITransfer
    {

        public void SendTransfer(TransferModelRequest request);

        byte[] GenerateConfirmation(int transfer);
        
    }
}
