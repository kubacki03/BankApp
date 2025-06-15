namespace BankApp.Server.Models
{
    public class BaseTransfer
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public int PayeeId { get; set; } 
        public BaseAccount Payee { get; set; } 

        public int SenderId { get; set; } 
        public BaseAccount Sender { get; set; } 

        public string Title { get; set; }

    
        public BaseTransfer() { }

        public BaseTransfer(decimal amount, DateTime date, BaseAccount payee, BaseAccount sender, string title)
        {
            Amount = amount;
            Date = date;
            Payee = payee;
            Sender = sender;
            Title = title;
        }
    }


}

