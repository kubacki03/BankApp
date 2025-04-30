namespace BankApp.Server.DTO
{
    public class TransferModelRequest
    {

       
            public decimal Amount { get; set; }
            public string SenderAccountNumber { get; set; }
            public string RecipientAccountNumber { get; set; }
            public DateTime Date { get; set; }
            public string Title { get; set; }
        

        public decimal GetAmount()
        {
            return Amount;
        }

        public string GetSenderAccountNumber()
        {
            return SenderAccountNumber;
        }

        public string GetRecipientAccountNumber()
        {
            return RecipientAccountNumber;
        }

        public DateTime GetDate()
        {
            return Date;
        }

        public string GetTitle()
        {
            return Title;
        }
    }
}
