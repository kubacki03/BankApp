namespace BankApp.Server.DTO
{
    public class TransferDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public string PayeeName { get; set; }
        public string Title { get; set; }
    }
}
