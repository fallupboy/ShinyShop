using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShinyShop.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string SenderUsername { get; set; }
        public User Recipient { get; set; }
    }
}
