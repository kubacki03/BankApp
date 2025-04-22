namespace BankApp.Server.DTO
{
    public class AccountDetailsDTO
    {
        public decimal Balance { get; set; }
        public string AccountNumber { get; set; }
        public List<TransferDTO> Transfers { get; set; }
    }
}
