using System.Collections.Generic;
using System.IO;
using System.Linq;
using web_lab4.Models;

namespace web_lab4.DataInitializer
{
    public static class DataSeeder
    {
        public static void Initialize(DatabaseContext ctx)
        {
            var users = CreateUsers();

            if (!ctx.Users.Any())
            {
                ctx.Users.AddRange(users);
            }

            ctx.SaveChanges();
        }
        
        private static List<User> CreateUsers()
        {
            return new[]
            {
                new User
                {
                    Username = "tweet",
                    Password = "tweet123"
                },
                new User
                {
                    Username = "danila",
                    Password = "danila123"
                },
                new User
                {
                    Username = "mike",
                    Password = "mike123"
                },
                new User
                {
                    Username = "Tom",
                    Password = "tom123"
                },
                new User
                {
                    Username = "inmate",
                    Password = "inmate123"
                }
            }.ToList();
        }
    }
}