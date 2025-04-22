namespace BankApp.Server.DTO
{
    public class TransferModelRequest
    {
        private long Id { get; set; }
        private decimal Amount { get; set; }

        private string SenderAccountNumber { get; set; }

        protected string RecipientAccountNumber { get; set; }

        private DateTime Date { get; set; }
    }
}
