using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShinyShop.ViewModels
{
    public class CreateMessageViewModel
    {
        [Required]
        public string Recipient { get; set; }

        [Required]
        [MaxLength(1300)]
        public string Message { get; set; }
    }
}
