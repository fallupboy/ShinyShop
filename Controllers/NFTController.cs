using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShinyShop.Models;
using ShinyShop.Repositories;

namespace ShinyShop.Controllers
{
    [Authorize]
    public class NFTController : Controller
    {
        private readonly INFTRepository _repo;

        public NFTController(INFTRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            List<NFT> nfts = _repo.GetContext().NFTs.OrderBy(i => i.Id).ToList();
            List<string> imageBase64Data = new List<string>();
            foreach (var nft in nfts)
            {
                imageBase64Data.Add(Convert.ToBase64String(nft.ImageData));
            }

            var imageTitles = nfts.Select(i => i.ImageName);
            var imageDataURLs = imageBase64Data.Select(i => string.Format("data:image/png;base64,{0}", i));

            var dict = imageTitles.Zip(imageDataURLs, (k, v) => new { k, v })
                .ToDictionary(x => x.k, x => x.v);
            ViewData["Products"] = dict;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage()
        {
            foreach (var file in Request.Form.Files)
            {
                NFT nft = new NFT();
                nft.ImageName = file.FileName.Replace(".png", "");

                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                nft.ImageData = ms.ToArray();
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
