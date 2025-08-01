﻿using System.ComponentModel.DataAnnotations;

namespace BankApp.Server.Models
{
    public class User
    {
        public int Id { get; set; } 

        public string Name { get; set; }
        public string LastName { get; set; }

        public DateOnly BirthDate { get; set; }

        public string Pesel { get; set; }

        public BaseAccount DefaulAccount { get; set; }
        public int DefaultAccountId { get; set; }

        public ICollection<BaseAccount> BaseAccounts { get; set; } = new List<BaseAccount>();
        

        public User() { }
        public User(string name, string secondName, DateOnly birthDate, string pesel)
        {
            Name = name;
            LastName = secondName;
            BirthDate = birthDate;
            Pesel = pesel;
        }
    }
}
