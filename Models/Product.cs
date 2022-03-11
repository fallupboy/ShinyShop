using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShinyShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ImateTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}
