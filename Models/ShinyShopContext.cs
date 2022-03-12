using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShinyShop.Models
{
    public class ShinyShopContext : DbContext
    {
        public DbSet<NFT> NFTs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public ShinyShopContext(DbContextOptions<ShinyShopContext> options)
            : base(options)
        { }
    }
}
