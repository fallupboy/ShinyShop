using ShinyShop.Models;
using ShinyShop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShinyShop.Repositories
{
    public interface INFTRepository
    {
        ShinyShopContext GetContext();
        Task<NFT> GetItemById(int? id);
        Task<string> GetNameById(int id);
        bool IsExisting(int id);
        Task<List<NFT>> GetAll();
        string ConvertImageDataToString(byte[] data);
        void Add(NFT nft);
        void Update(NFT nft);
        void Remove(NFT nft);
        Task SaveChangesAsync();
        
    }
}
