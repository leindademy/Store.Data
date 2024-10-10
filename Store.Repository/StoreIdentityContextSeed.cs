using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName= "Leinda Said",
                    Email = "Leinda66@gmail.com",
                    UserName = "LeindaSaid",
                    Address = new Address
                    { 
                        FirstName = "Leinda",
                        LastName = "Said", 
                        City = "Ras Sudr",
                        State = "Cairo",
                        Street = "10 Street",
                        PostalCode = "12345"

                    }
                };
                await userManager.CreateAsync(user, "Password123!");
            }
        }
        
     
    }
}
