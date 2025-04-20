namespace BankApp.Server.Interfaces
{
    public interface IRepository
    {
       abstract BaseAccount GetAccountByLogin(string username);
    }
}
