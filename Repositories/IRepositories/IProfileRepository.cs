using Microsoft.AspNetCore.DataProtection;
using ShinyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShinyShop.Repositories.IRepositories
{
    public interface IProfileRepository
    {
        ShinyShopContext GetContext();
        Task<User> GetCurrentUser(ClaimsPrincipal user);
        IDataProtectionProvider GetDataProtectionProvider();
        Task SaveChangesAsync();
        bool UserExists(int id);
    }
}
