
using BankApp.Server.Models;

public class BaseAccount
    {
        protected int Id { get; init; }
        public string AccountNumber { get; init; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }             
        public bool IsActive { get; set; }                  
                      
        public string Password { get; set; }
        public string Login { get; set; }
        
        public string Email { get; set; }

        public ICollection<BaseTransfer> Transfers { get; set; } = new List<BaseTransfer>();

     }

