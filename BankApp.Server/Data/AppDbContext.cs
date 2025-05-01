using BankApp.Server.Models;
using Microsoft.EntityFrameworkCore;


    public class AppDbContext : DbContext
    {

       public DbSet<BaseAccount> accounts;
        
       public DbSet<BaseTransfer> transfers;


    public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
    {
    }
}

