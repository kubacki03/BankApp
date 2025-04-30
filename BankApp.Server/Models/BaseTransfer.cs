namespace BankApp.Server.Models
{
    public class BaseTransfer
    {
        private int Id { get; set; }

        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public BaseAccount Payee { get; private set; }
        public BaseAccount Sender { get; private set; }
        public string Title { get; private set; }

        public BaseTransfer( decimal amount, DateTime date, BaseAccount payee, BaseAccount sender, string title)
        {
           
            Amount = amount;
            Date = date;
            Payee = payee;
            Sender = sender;
            Title = title;
        }

        public int GetId()
        {
            return Id;
        }
    }
}
