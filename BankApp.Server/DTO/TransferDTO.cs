namespace BankApp.Server.DTO
{
    public class TransferDTO
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string PayeeName { get; set; }
        public string Title { get; set; }
    }
}
