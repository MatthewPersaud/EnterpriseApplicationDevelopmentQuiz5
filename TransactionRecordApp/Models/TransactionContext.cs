using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TransactionRecordApp.Models
{
    /// <summary>
    /// This class will inherit from the Entity Framework (EF) class
    /// called DbContext and is used by the code to interact with the DB
    /// </summary>
    public class TransactionContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Define a constructor that simply passes the options argument
        /// up to the base class constuctor
        /// </summary>
        /// <param name="options"></param>
        public TransactionContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Gives access to the Transactions table in the DB
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Gives access to the TransactionType table in the DB
        /// </summary>
        public DbSet<TransactionType> TransactionType { get; set; }

        /// <summary>
        /// *Code snippet #1*
        /// Creates an admin user
        /// </summary>
        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "Sesame123#";
            string roleName = "Admin";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            // if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }

        /// <summary>
        /// Add some records upon build
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call base class version
            base.OnModelCreating(modelBuilder);
            
            // Add the transaction types
            modelBuilder.Entity<TransactionType>().HasData(
                new TransactionType() { TransactionTypeId = "S", Name = "Sell", CommissionFee = 5.99 },
                new TransactionType() { TransactionTypeId = "B", Name = "Buy", CommissionFee = 5.40}
                );

            // Seed the DB with 5 movies including a reference to the TransactionType table
            modelBuilder.Entity<Transaction>().HasData(
                new Transaction()
                {
                    TransactionId = 1,
                    TickerSymbol = "AAPL",
                    CompanyName = "Apple",
                    Quantity = 2,
                    SharePrice = 142.90,
                    TransactionTypeId = "B"
                },
                new Transaction()
                {
                    TransactionId = 2,
                    TickerSymbol = "F",
                    CompanyName = "Ford Motors Company",
                    Quantity = 4,
                    SharePrice = 12.82,
                    TransactionTypeId = "S"
                },
                new Transaction()
                {
                    TransactionId = 3,
                    TickerSymbol = "GOOG",
                    CompanyName = "Alphabet Inc.",
                    Quantity = 100,
                    SharePrice = 2701.76,
                    TransactionTypeId = "S"
                },
                new Transaction()
                {
                    TransactionId = 4,
                    TickerSymbol = "MSFT",
                    CompanyName = "Microsoft Corporation",
                    Quantity = 100,
                    SharePrice = 123.45,
                    TransactionTypeId = "B"
                }
                );
        }
    }
}
