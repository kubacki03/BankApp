
     public class BaseAccount
    {
        protected int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }             
        public bool IsActive { get; set; }                  
                      
        public string Password { get; set; }
        public string Login { get; set; }

     }

