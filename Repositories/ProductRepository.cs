using Microsoft.EntityFrameworkCore;
using ShinyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShinyShop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShinyShopContext _context;
        public ProductRepository(ShinyShopContext context)
        {
            _context = context;
        }

        public async Task<Product> GetItemById(int? id)
        {
            var item = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return item;
        }

        public async Task<string> GetNameById(int id)
        {
            var name = await _context.Products.Where(p => p.Id == id).Select(p => p.ImateTitle).FirstOrDefaultAsync();
            return name;
        }

        public bool IsExisting(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public void Add(Product product)
        {
            _context.Add(product);
        }

        public void Update(Product product)
        {
            _context.Update(product);
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
