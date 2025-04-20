
     public class BaseAccount
    {
        protected int Id { get; set; }
        protected string AccountNumber { get; set; }
        protected decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }             
        public bool IsActive { get; set; }                  
                      
        public string Password { get; set; }
        public string Login { get; set; }

     }

