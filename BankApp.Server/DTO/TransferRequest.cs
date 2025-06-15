namespace BankApp.Server.DTO
{
    public class TransferRequest
    {
        public decimal Amount { get; set; }
        public string RecipientAccountNumber { get; set; }

        public string Title { get; set; }
    }
}
