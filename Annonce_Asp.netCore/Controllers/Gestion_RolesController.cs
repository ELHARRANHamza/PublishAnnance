using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Annonce_Asp.netCore.Models;
using Annonce_Asp.netCore.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Annonce_Asp.netCore.Controllers
{
    public class Gestion_RolesController : Controller
    {
        public RoleManager<IdentityRole> RoleManager { get; }
        public SignInManager<AppUsers> SignInManager { get; }
        public UserManager<AppUsers> UserManager { get; }

        public Gestion_RolesController(RoleManager<IdentityRole> roleManager, 
            SignInManager<AppUsers> signInManager,
            UserManager<AppUsers> userManager)
        {
            RoleManager = roleManager;
            SignInManager = signInManager;
            UserManager = userManager;
                  }
        // GET: Gestion_Roles
        public async Task<IActionResult> Index()
        {
            var get_User = await UserManager.GetUserAsync(User);
            if (SignInManager.IsSignedIn(User))
            {
                if (get_User.UserType == "Admin")
                {
                    var list_Role = RoleManager.Roles.ToList();
                    return View(list_Role);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Gestion_Roles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var get_User = await UserManager.GetUserAsync(User);
            if (SignInManager.IsSignedIn(User))
            {
                if (get_User.UserType == "Admin") { 
                    var find_Role = RoleManager.Roles.SingleOrDefault(r => r.Id == id);
            return View(find_Role);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Gestion_Roles/Create
        public async Task<IActionResult> Create()
        {
            var get_User = await UserManager.GetUserAsync(User);
            if (SignInManager.IsSignedIn(User))
            {
                if (get_User.UserType == "Admin")
                {
                    return View();
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // POST: Gestion_Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Roles_ViewModels roles)
        {
            var get_User = await UserManager.GetUserAsync(User);
            if (SignInManager.IsSignedIn(User))
            {
                if (get_User.UserType == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            var role = new IdentityRole()
                            {
                                Name = roles.nom_role
                            };
                            var result = await RoleManager.CreateAsync(role);
                            if (result.Succeeded)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                            return RedirectToAction(nameof(Index));
                        }
                        catch
                        {
                            return View();
                        }
                    }
                    return View(roles);
                }
                else
                {
                    return NotFound();
                }
                }
                return RedirectToAction("login", "Account");
            }
        // GET: Gestion_Roles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var get_User = await UserManager.GetUserAsync(User);
            if (SignInManager.IsSignedIn(User))
            {
                if (get_User.UserType == "Admin")
                {
                    var find_Role = RoleManager.Roles.SingleOrDefault(r => r.Id == id);
            var role_model = new Roles_ViewModels()
            {
                nom_role = find_Role.Name
            };
            return View(role_model);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // POST: Gestion_Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Roles_ViewModels roles_ViewModels)
        {
            var get_User = await UserManager.GetUserAsync(User);
            if (SignInManager.IsSignedIn(User))
            {
                if (get_User.UserType == "Admin")
                {
                    if (ModelState.IsValid)
            {
                try
                {
                    var find_Role = RoleManager.Roles.SingleOrDefault(r => r.Id == id);
                    find_Role.Name = roles_ViewModels.nom_role;
                  var result=await RoleManager.UpdateAsync(find_Role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(roles_ViewModels);
                }
                catch
                {
                    return View();
                }
            }
            return View(roles_ViewModels);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Gestion_Roles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var get_User = await UserManager.GetUserAsync(User);
            if (SignInManager.IsSignedIn(User))
            {
                if (get_User.UserType == "Admin")
                {
                    var find_Role = RoleManager.Roles.SingleOrDefault(r => r.Id == id);
            return View(find_Role);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // POST: Gestion_Roles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, IFormCollection collection)
        {
            var get_User = await UserManager.GetUserAsync(User);
            if (SignInManager.IsSignedIn(User))
            {
                if (get_User.UserType == "Admin")
                {
                    try
            {
                var find_Role = RoleManager.Roles.SingleOrDefault(r => r.Id == id);
              var result =await RoleManager.DeleteAsync(find_Role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
            return View();
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }
       
       
    }
}