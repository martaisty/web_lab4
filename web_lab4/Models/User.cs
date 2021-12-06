using System.Collections.Generic;
using web_lab4.Abstractions;

namespace web_lab4.Models
{
    public class User : IEntity<int>
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}