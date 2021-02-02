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
    public class LatestsNewsController : Controller
    {
        public Repository<Latest_News> Repository { get; }
        public IHostingEnvironment Hosting { get; }
        public UserManager<AppUsers> UserManager { get; }
        public SignInManager<AppUsers> SignInManager { get; }

        // GET: LatestsNews
        public LatestsNewsController(Repository<Latest_News> repository,IHostingEnvironment hosting, UserManager<AppUsers> userManager,
            SignInManager<AppUsers> signInManager)
        {
            Repository = repository;
            Hosting = hosting;
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            if (SignInManager.IsSignedIn(User))
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    var liste_News = Repository.List();
                    return View(liste_News);
                }
                else
                {
                    return NotFound();
                }
                }
                return RedirectToAction("login", "Account");
            }

        // GET: LatestsNews/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (SignInManager.IsSignedIn(User))
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    var find_News = Repository.Find(id);
            return View(find_News);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: LatestsNews/Create
        public async Task<IActionResult> Create()
        {
            if (SignInManager.IsSignedIn(User))
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

        // POST: LatestsNews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News_ViewModel model)
        {
            if (SignInManager.IsSignedIn(User))
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    try
                {
                    string fileName = "";
                    if (model.file.FileName != "")
                    {
                        string chemain = Path.Combine(Hosting.WebRootPath, "Image_News");
                        fileName = model.file.FileName;
                        string path = Path.Combine(chemain, fileName);
                        model.file.CopyTo(new FileStream(path, FileMode.Create));
                        var news = new Latest_News()
                        {
                            date_Publiciter = DateTime.Now,
                            Description = model.Description,
                            Titre = model.Titre,
                            Image =fileName,
                            url = model.url
                        };
                        Repository.Add(news);
                        return RedirectToAction(nameof(Index));
                    }
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

        // GET: LatestsNews/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (SignInManager.IsSignedIn(User))
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    var find_News = Repository.Find(id);
                    var model = new News_ViewModel()
                    {
                        Image = find_News.Image,
                        Description = find_News.Description,
                        Titre = find_News.Titre,
                        url = find_News.url
                    };
                    return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        return RedirectToAction("login", "Account");
    }

        // POST: LatestsNews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, News_ViewModel model)
        {
            string fileName = "";
            if (SignInManager.IsSignedIn(User))
            {
                var get_User = await UserManager.GetUserAsync(User);
                var find_News = Repository.Find(id);
                if (get_User.UserType == "Admin")
                {
                    try
            {
                        if (model.file == null)
                        {
                            fileName = find_News.Image;
                        }
                        else
                        {
                            string chemain = Path.Combine(Hosting.WebRootPath, "Image_News");
                            fileName = model.file.FileName;
                            string new_Path = Path.Combine(chemain, fileName);
                            string old_Path = Path.Combine(chemain, find_News.Image);
                            if (new_Path != old_Path)
                            {
                                System.IO.File.Delete(old_Path);
                                model.file.CopyTo(new FileStream(new_Path, FileMode.Create));
                            }
                        }
                        find_News.Description = model.Description;
                        find_News.Image = fileName;
                        find_News.Titre = model.Titre;
                        find_News.url = model.url;
                        Repository.Update(id,find_News);
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

        // GET: LatestsNews/Delete/5
        public async Task<IActionResult> Delete(int id)
                {
                    if (SignInManager.IsSignedIn(User))
                    {
                        var get_User = await UserManager.GetUserAsync(User);
                        if (get_User.UserType == "Admin")
                        {
                    var find_News = Repository.Find(id);
                    return View(find_News);
                }
            else
            {
                return NotFound();
            }
        }
        return RedirectToAction("login", "Account");
    }

        // POST: LatestsNews/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            if (SignInManager.IsSignedIn(User))
            {
                var get_User = await UserManager.GetUserAsync(User);
                if (get_User.UserType == "Admin")
                {
                    try
            {
                Repository.Delete(id);
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
    }
}