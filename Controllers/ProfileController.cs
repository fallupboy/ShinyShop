using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShinyShop.Models;
using ShinyShop.Repositories;
using ShinyShop.Repositories.IRepositories;
using ShinyShop.Services;
using ShinyShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShinyShop.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _repo;
        private readonly INFTRepository _repoNFT;

        public ProfileController(IProfileRepository repo, INFTRepository repoNFT)
        {
            _repo = repo;
            _repoNFT = repoNFT;
        }

        public async Task<IActionResult> Index()
        {
            User currentUser = await _repo.GetCurrentUser(User);

            if (currentUser != null)
            {
                return View(currentUser);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _repo.GetCurrentUser(User);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Address,City")] User user)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                User currentUser = await _repo.GetCurrentUser(User);

                currentUser.Username = user.Username;
                currentUser.Address = user.Address;
                currentUser.City = user.City;

                await _repo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult EditPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(ChangePasswordViewModel model)
        {
            CipherService cipher = new CipherService(_repo.GetDataProtectionProvider());

            User user = await _repo.GetCurrentUser(User);

            if (ModelState.IsValid)
            {
                if (cipher.Decrypt(user.Password) == model.CurrentPassword)
                {
                    user.Password = cipher.Encrypt(model.NewPassword);
                    await _repo.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        public async Task<IActionResult> MyNFTs()
        {
            User currentUser = await _repo.GetCurrentUser(User);
            List<NFT> myNFTs = _repoNFT.GetContext().NFTs.Where(i => i.UserId == currentUser.Id).OrderBy(i => i.Id).ToList();

            if (myNFTs == null)
            {
                return View();
            }

            return View(myNFTs);
        }

        public async Task<IActionResult> MyMessages()
        {
            User currentUser = await _repo.GetCurrentUser(User);
            return View(currentUser.Messages);
        }

        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageViewModel viewModel)
        {
            User currentUser = await _repo.GetCurrentUser(User);
            User recepientUser = await _repo.GetContext().Users.FirstOrDefaultAsync(u => u.Username == viewModel.Recipient);
            if (recepientUser != null)
            {
                recepientUser.Messages.Add(new Message()
                {
                    Text = viewModel.Message,
                    Recipient = recepientUser,
                    SenderUsername = currentUser.Username
                });
                await _repo.SaveChangesAsync();
                return View();
            }
            return View(viewModel);
        }

        private bool UserExists(int id)
        {
            return _repo.UserExists(id);
        }
    }
}
