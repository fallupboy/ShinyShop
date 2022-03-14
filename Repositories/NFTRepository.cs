using Microsoft.EntityFrameworkCore;
using ShinyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShinyShop.Repositories
{
    public class NFTRepository : INFTRepository
    {
        private readonly ShinyShopContext _context;
        public NFTRepository(ShinyShopContext context)
        {
            _context = context;
        }

        public ShinyShopContext GetContext()
        {
            return _context;
        }

        public async Task<NFT> GetItemById(int? id)
        {
            var item = await _context.NFTs.FirstOrDefaultAsync(p => p.Id == id);
            return item;
        }

        public async Task<string> GetNameById(int id)
        {
            var name = await _context.NFTs.Where(p => p.Id == id).Select(p => p.ImageName).FirstOrDefaultAsync();
            return name;
        }

        public bool IsExisting(int id)
        {
            return _context.NFTs.Any(e => e.Id == id);
        }

        public async Task<List<NFT>> GetAll()
        {
            return await _context.NFTs.ToListAsync();
        }

        public Dictionary<string, string> GetNFTsForOutput(List<NFT> nfts)
        {
            List<string> imageBase64Data = new List<string>();
            foreach (var nft in nfts)
            {
                imageBase64Data.Add(Convert.ToBase64String(nft.ImageData));
            }

            var imageTitles = nfts.Select(i => i.ImageName);
            var imageDataURLs = imageBase64Data.Select(i => string.Format("data:image/png;base64,{0}", i));

            var nftDict = imageTitles.Zip(imageDataURLs, (k, v) => new { k, v })
                .ToDictionary(x => x.k, x => x.v);

            return nftDict;
        }

        public void Add(NFT nft)
        {
            _context.Add(nft);
        }

        public void Update(NFT nft)
        {
            _context.Update(nft);
        }

        public void Remove(NFT nft)
        {
            _context.NFTs.Remove(nft);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
