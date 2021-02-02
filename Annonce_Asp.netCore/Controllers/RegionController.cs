using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Annonce_Asp.netCore.Models;
using Annonce_Asp.netCore.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Annonce_Asp.netCore.Controllers
{
    public class RegionController : Controller
    {
        public Repository<Region> Rep_Region { get; }
        public SignInManager<AppUsers> SignInManager { get; }
        public UserManager<AppUsers> UserManager { get; }

        public RegionController(Repository<Region> rep_Region,
            SignInManager<AppUsers> signInManager,
            UserManager<AppUsers> userManager)
        {
            Rep_Region = rep_Region;
            SignInManager = signInManager;
            UserManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            if (Valide_Sign())
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    var liste_Regions = Rep_Region.List();
                    return View(liste_Regions);
                }
                else
                {
                    return NotFound();
                }
                }
                return RedirectToAction("login", "Account");
            }

        // GET: Region/Details/5
        public async Task<IActionResult> Details(int id)
        {
                    if (Valide_Sign())
                    {
                        var get_User = await UserManager.GetUserAsync(User);
                        if (get_User.UserType == "Admin")
                        {
                            return View(find_Region(id));
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Region/Create
        public async Task<IActionResult> Create()
                        {
                            if (Valide_Sign())
                            {
                                var get_User = await UserManager.GetUserAsync(User);
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

        // POST: Region/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Region region)
        {
            if (Valide_Sign())
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    try
            {
                Rep_Region.Add(region);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Region/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
                    if (Valide_Sign())
                    {
                        var get_User = await UserManager.GetUserAsync(User);
                        if (get_User.UserType == "Admin")
                        {
                            return View(find_Region(id));
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // POST: Region/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Region region)
        {
            if (Valide_Sign())
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    try
            {
                var find = find_Region(id);
                find.Region_Nom = region.Region_Nom;
                Rep_Region.Update(id, find);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Region/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
                    if (Valide_Sign())
                    {
                        var get_User = await UserManager.GetUserAsync(User);
                        if (get_User.UserType == "Admin")
                        {
                            return View(find_Region(id));
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // POST: Region/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            if (Valide_Sign())
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    try
            {
                Rep_Region.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }
        public Region find_Region(int id)
        {
            var find = Rep_Region.Find(id);
            return find;

        }
        public bool Valide_Sign()
        {
            if (SignInManager.IsSignedIn(User))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}