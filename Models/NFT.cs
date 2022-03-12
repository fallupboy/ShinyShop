using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShinyShop.Models
{
    public class NFT
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
    }
}
