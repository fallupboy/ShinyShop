using ShinyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShinyShop.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetItemById(int? id);
        Task<string> GetNameById(int id);
        bool IsExisting(int id);
        Task<List<Product>> GetAll();
        void Add(Product product);
        void Update(Product product);
        void Remove(Product product);
        Task SaveChangesAsync();
        
    }
}
