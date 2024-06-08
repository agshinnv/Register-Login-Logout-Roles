﻿using FrontToBack.Models;
using FrontToBack.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<UserRolesVM> userRoles = new();

            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                string rolesStr = string.Join(",", roles.ToArray());

                userRoles.Add(new UserRolesVM
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Username = user.UserName,
                    Roles = rolesStr

                });
            }


            return View(userRoles);
        }
    }
}
