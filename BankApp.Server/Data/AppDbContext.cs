using Microsoft.EntityFrameworkCore;


    public class AppDbContext :DbContext
    {

       public DbSet<BaseAccount> accounts;
    }

