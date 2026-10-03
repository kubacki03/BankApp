namespace BankApp.Server.Services
{
    public class TransferException : Exception
    {
        public TransferException(string message) : base(message)
        {
        }
    }
}
