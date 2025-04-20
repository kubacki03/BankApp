using BankApp.Server.DTO;

namespace BankApp.Server.Interfaces
{
    public interface ILogin
    {
        void Login(LoginModelRequest modelRequest);

        string GenerateTempPassword(string username);

    }
}
