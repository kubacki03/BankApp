using System.ComponentModel.DataAnnotations;

namespace BankApp.Server.Models
{
    public class DebitCard
    {
        [MaxLength(32)]
        public string CardNumber { get; set; }

        public string ExpirationDate { get; set; }

        [StringLength(3)]
        public string CVV { get; set; }

        public DateTime Lifetime { get; set; }

        
    }
}
