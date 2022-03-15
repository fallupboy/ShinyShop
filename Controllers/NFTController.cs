using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShinyShop.Models;
using ShinyShop.Repositories;
using ShinyShop.Repositories.IRepositories;
using ShinyShop.Services;

namespace ShinyShop.Controllers
{
    [Authorize]
    public class NFTController : Controller
    {
        private readonly INFTRepository _repo;
        private readonly IProfileRepository _repoProfile;

        public NFTController(INFTRepository repo, IProfileRepository repoProfile)
        {
            _repo = repo;
            _repoProfile = repoProfile;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;

            ViewData["CurrentFilter"] = searchString;

            IQueryable<NFT> nfts = _repo.GetContext().NFTs.OrderBy(i => i.Id);
            if (!String.IsNullOrEmpty(searchString))
            {
                nfts = nfts.Where(s => s.ImageName.ToLower().Contains(searchString.ToLower()));
            }

            int pageSize = 20;
            return View(await PaginatedList<NFT>.CreateAsync(nfts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var nft = await _repo.GetContext().NFTs
                .Include(r => r.User)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (nft == null)
            {
                return NotFound();
            }

            return View(nft);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UploadImage()
        {
            foreach (var file in Request.Form.Files)
            {
                NFT nft = new NFT();
                nft.ImageName = file.FileName.Replace(".png", "");

                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                nft.ImageData = _repo.ConvertImageDataToString(ms.ToArray());
                _repo.Add(nft);
                await _repo.SaveChangesAsync();

                ms.Close();
                ms.Dispose();
            }
            ViewBag.Message = "Image(s) stored in database!";
            return RedirectToAction("Index");
        }
    }
}
