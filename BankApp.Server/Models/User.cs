namespace BankApp.Server.Models
{
    public class User
    {
        private string Name { get; set; }
        private string SecondName { get; set; }

        private DateOnly BirthDate { get; set; }

        private string Pesel { get; set; }
    }
}
