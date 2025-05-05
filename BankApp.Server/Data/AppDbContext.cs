using BankApp.Server.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<BaseAccount> Accounts { get; set; }
    public DbSet<CompanyAccount> CompanyAccounts { get; set; }

    public DbSet<BaseTransfer> Transfers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Konfiguracja dziedziczenia - Table Per Type (TPT)
        modelBuilder.Entity<BaseAccount>().ToTable("Accounts");
        modelBuilder.Entity<CompanyAccount>().ToTable("CompanyAccounts");

      
        modelBuilder.Entity<BaseAccount>()
            .HasOne(a => a.User)
            .WithMany(u => u.BaseAccounts)
            .HasForeignKey(a=> a.UserId) 
            .OnDelete(DeleteBehavior.Cascade);

        


        modelBuilder.Entity<BaseTransfer>()
       .HasOne(t => t.Sender)
       .WithMany()
       .HasForeignKey(t => t.SenderId)
       .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<BaseTransfer>()
            .HasOne(t => t.Payee)
            .WithMany()
            .HasForeignKey(t => t.PayeeId)
            .OnDelete(DeleteBehavior.Restrict); 


    }
}
