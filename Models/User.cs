using ShinyShop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShinyShop.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public List<Message> Messages { get; set; }

        public User()
        {
            Messages = new List<Message>();
        }
    }
}
