using BankApp.Server.DTO;

namespace BankApp.Server.Interfaces
{
    public interface IRegister
    {
        bool Register(RegisterModelRequest modelRequest);
        
        bool DoesUserExist(RegisterModelRequest modelRequest);
    }
}
