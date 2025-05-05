namespace BankApp.Server.Models
{
    public class BaseTransfer
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public int PayeeId { get; set; } // Klucz obcy do BaseAccount (Payee)
        public BaseAccount Payee { get; set; } // Nawigacja do BaseAccount (Payee)

        public int SenderId { get; set; } // Klucz obcy do BaseAccount (Sender)
        public BaseAccount Sender { get; set; } // Nawigacja do BaseAccount (Sender)

        public string Title { get; set; }

        // Konstruktor bezparametrowy – potrzebny dla EF Core
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

