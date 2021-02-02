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
    public class VillesController : Controller
    {
        public Repository<Ville> Rep_Ville { get; }
        public SignInManager<AppUsers> SignInManager { get; }
        public UserManager<AppUsers> UserManager { get; }

        public VillesController(Repository<Ville> rep_Ville,
            SignInManager<AppUsers> signInManager,
            UserManager<AppUsers> userManager)
        {
            Rep_Ville = rep_Ville;
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
                    var liste_Villes = Rep_Ville.List();
                    return View(liste_Villes);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Villes/Details/5
        public async Task<IActionResult> Details(int id)
        {
                    if (Valide_Sign())
                    {
                        var get_User = await UserManager.GetUserAsync(User);
                        if (get_User.UserType == "Admin")
                        {
                            return View(find_Ville(id));
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return RedirectToAction("login", "Account");
                }

        // GET: Villes/Create
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

        // POST: Villes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ville ville)
        {
            if (Valide_Sign())
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    if (ModelState.IsValid)
            {
                try
                {
                    Rep_Ville.Add(ville);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
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

        // GET: Villes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
                    if (Valide_Sign())
                    {
                        var get_User = await UserManager.GetUserAsync(User);
                        if (get_User.UserType == "Admin")
                        {
                            return View(find_Ville(id));
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return RedirectToAction("login", "Account");
                }

        // POST: Villes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ville ville)
        {
            if (Valide_Sign())
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    try
            {
                var find = find_Ville(id);
                find.ville_Name = ville.ville_Name;
                Rep_Ville.Update(id, find);
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

        // GET: Villes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
                    if (Valide_Sign())
                    {
                        var get_User = await UserManager.GetUserAsync(User);
                        if (get_User.UserType == "Admin")
                        {
                            return View(find_Ville(id));
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return RedirectToAction("login", "Account");
                }

        // POST: Villes/Delete/5
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
                Rep_Ville.Delete(id);
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
        public Ville find_Ville(int id)
        {
            var find = Rep_Ville.Find(id);
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