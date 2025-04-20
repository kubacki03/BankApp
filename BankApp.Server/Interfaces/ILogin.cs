using BankApp.Server.DTO;

namespace BankApp.Server.Interfaces
{
    public interface ILogin
    {
        string Login(LoginModelRequest modelRequest);

        string GenerateTempPassword(string username);

    }
}
