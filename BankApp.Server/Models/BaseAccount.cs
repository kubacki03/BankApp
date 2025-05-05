
using BankApp.Server.Models;

public class BaseAccount
{
    public int Id { get; init; }
    public string Iban { get; init; }
    protected string Name { get; init; }

    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; }

    public string Password { get; set; }
    public string Login { get; set; }

    public string Email { get; set; }

    public ICollection<BaseTransfer> Transfers { get; set; } = new List<BaseTransfer>(); // Lista transakcji

    public int UserId { get; set; }
    public User User { get; set; }
}


