namespace BankApp.Server.Models
{
    public class BaseTransfer
    {
        private int Id { get; set; }

        private readonly decimal Amount;

        private DateTime Date { get; set; } = DateTime.Now;

        private   BaseAccount Payee { get; set; }
        private BaseAccount Sender { get; set; }

        private string Title { get; set; }
    }
}
