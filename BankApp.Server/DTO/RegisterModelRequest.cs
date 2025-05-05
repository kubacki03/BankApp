namespace BankApp.Server.DTO
{
    public record RegisterModelRequest
    {
        public string password { get; set; }
        public    string email { get; set; }
        public  string name { get; set; }
        public   string lastName { get; set; }
        public DateOnly birthDay { get; set; }
        public   string pesel { get; set; }
        public   string? companyName { get; set; }
        public   string? regon { get; set; }
        public    string? nip { get; set; }
    }
}
