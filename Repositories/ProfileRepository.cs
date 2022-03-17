using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ShinyShop.Models;
using ShinyShop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ShinyShop.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ShinyShopContext _context;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        public ProfileRepository(ShinyShopContext context, IDataProtectionProvider dataProtectionProvider)
        {
            _context = context;
            _dataProtectionProvider = dataProtectionProvider;
        }
        public ShinyShopContext GetContext()
        {
            return _context;
        }

        public async Task<User> GetCurrentUser(ClaimsPrincipal user)
        {
            User currentUser = await _context.Users
                .Include(u => u.Role)
                .Include(m => m.Messages)
                .FirstOrDefaultAsync(u => u.Email == user.Identity.Name);
            return currentUser;
        }

        public IDataProtectionProvider GetDataProtectionProvider()
        {
            return _dataProtectionProvider;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
    }
}
