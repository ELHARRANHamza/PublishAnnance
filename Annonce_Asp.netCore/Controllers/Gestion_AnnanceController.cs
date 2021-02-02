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
    public class Gestion_AnnanceController : Controller
    {
        public Repository<Annonces> Rep_Ann { get; }
        public SignInManager<AppUsers> SignInManager { get; }
        public UserManager<AppUsers> UserManager { get; }

        public Gestion_AnnanceController(Repository<Annonces> Rep_Ann, 
            SignInManager<AppUsers> signInManager,
            UserManager<AppUsers> userManager)
        {
            this.Rep_Ann = Rep_Ann;
            SignInManager = signInManager;
            UserManager = userManager;
        }
        // GET: Gestion_Annance
        public async Task<IActionResult> Index_Envoyer()
        {
            if (Valide_Sign())
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    var ann = GetAnnonces(1);
            return View(ann);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Gestion_Annance/Details/5
        public async Task<IActionResult> Annonce_Accepter()
                {
                    if (Valide_Sign())
                    {
                        var get_User = await UserManager.GetUserAsync(User);
                        if (get_User.UserType == "Admin")
                        {
                            var ann = GetAnnonces(2);
            return View(ann);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return RedirectToAction("login", "Account");
                }

        public async Task<IActionResult> Annonce_Refuser()
        {
                            if (Valide_Sign())
                            {
                                var get_User = await UserManager.GetUserAsync(User);
                                if (get_User.UserType == "Admin")
                                {
                                    var ann = GetAnnonces(3);
            return View(ann);
                                }
                                else
                                {
                                    return NotFound();
                                }
                            }
                            return RedirectToAction("login", "Account");
                        }
        public async Task<IActionResult> Valider_Ann(int id)
        {
                                    if (Valide_Sign())
                                    {
                                        var get_User = await UserManager.GetUserAsync(User);
                                        if (get_User.UserType == "Admin")
                                        {
                                            return View(find_Annonces(id));
                                        }
                                        else
                                        {
                                            return NotFound();
                                        }
                                    }
                                    return RedirectToAction("login", "Account");
                                }
        [HttpPost]
        public async Task<IActionResult> Valider_Ann(int id,Annonces annonces)
        {
            if (Valide_Sign())
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    try
            {
                var ann = find_Annonces(id);
                ann.pos = 2;
                Rep_Ann.Update(id, ann);
                return RedirectToAction(nameof(Index_Envoyer));
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


        public async Task<IActionResult> Refuser_Ann(int id)
        {
                    if (Valide_Sign())
                    {
                        var get_User = await UserManager.GetUserAsync(User);
                        if (get_User.UserType == "Admin")
                        {
                            return View(find_Annonces(id));
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return RedirectToAction("login", "Account");
                }
        [HttpPost]
        public async Task<IActionResult> Refuser_Ann(int id, Annonces annonces)
        {
            if (Valide_Sign())
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    try
            {
                var ann = find_Annonces(id);
                ann.pos = 3;
                Rep_Ann.Update(id, ann);
                return RedirectToAction(nameof(Index_Envoyer));
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


        public IList<Annonces> GetAnnonces(int pos)
        {
            var ann = Rep_Ann.List().Where(a => a.pos == pos).ToList();
            return ann;
        }
        public Annonces find_Annonces(int id)
        {
            var find = Rep_Ann.Find(id);
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