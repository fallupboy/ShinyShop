﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShinyShop.Models;
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

        public ProfileController(IProfileRepository repo)
        {
            _repo = repo;
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Address,City")] User user)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                User currentUser = await _repo.GetCurrentUser(User);

                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
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

        private bool UserExists(int id)
        {
            return _repo.UserExists(id);
        }
    }
}
