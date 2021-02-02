using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Annonce_Asp.netCore.Models;
using Annonce_Asp.netCore.Models.Repository;
using Annonce_Asp.netCore.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Annonce_Asp.netCore.Controllers
{
    public class CategoriesController : Controller
    {
        public Repository<Categories> Rep_Cat { get; }
        public IHostingEnvironment Hosting { get; }
        public SignInManager<AppUsers> SignInManager { get; }
        public UserManager<AppUsers> UserManager { get; }

        // GET: Categories
        public CategoriesController(Repository<Categories> rep_Cat,
            IHostingEnvironment hosting,
            SignInManager<AppUsers> signInManager,
            UserManager<AppUsers> userManager)
        {
            Rep_Cat = rep_Cat;
            Hosting = hosting;
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
                    var list_Cat = Rep_Cat.List();
                    return View(list_Cat);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");

        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (Valide_Sign())
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    return View(find_Cat(id));
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");

        }

        // GET: Categories/Create
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

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Create_CatViewModel model)
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
                            string fileName = "";
                            if (model.GetFile.FileName != "")
                            {
                                string chemain = Path.Combine(Hosting.WebRootPath, "Image_Cat");
                                fileName = model.GetFile.FileName;
                                string path = Path.Combine(chemain, fileName);
                                model.GetFile.CopyTo(new FileStream(path, FileMode.Create));
                                var categories = new Categories()
                                {
                                    nom_Cat = model.nom_Cat,
                                    Image = fileName
                                };
                                Rep_Cat.Add(categories);
                                return RedirectToAction(nameof(Index));
                            }
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

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (Valide_Sign())
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    var find_Cat = Rep_Cat.Find(id);
                    var Cat = new Create_CatViewModel()
                    {
                        id = id,
                        nom_Cat = find_Cat.nom_Cat,
                        Image = find_Cat.Image
                    };
                    return View(Cat);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");

        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Create_CatViewModel categories)
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
                            string fileName = "";
                            if (categories.GetFile.FileName != "")
                            {
                                string chemain = Path.Combine(Hosting.WebRootPath, "Image_Cat");
                                fileName = categories.GetFile.FileName;
                                string path = Path.Combine(chemain, fileName);
                                string old_Path = Path.Combine(chemain, categories.Image);
                                if (path != old_Path)
                                {
                                    System.IO.File.Delete(old_Path);
                                    categories.GetFile.CopyTo(new FileStream(path, FileMode.Create));
                                }
                                var find_Cat = Rep_Cat.Find(id);
                                find_Cat.nom_Cat = categories.nom_Cat;
                                find_Cat.Image = fileName;
                                Rep_Cat.Update(id, find_Cat);
                                return RedirectToAction(nameof(Index));
                            }
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

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (Valide_Sign())
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    return View(find_Cat(id));
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");

        }

        // POST: Categories/Delete/5
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
                        Rep_Cat.Delete(id);
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
        public Categories find_Cat(int id)
        {
            var find_Cat = Rep_Cat.Find(id);
            return find_Cat;

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