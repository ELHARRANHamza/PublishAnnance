using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class AnnocesController : Controller
    {
        public Repository<Annonces> Rep_Annonces { get; }
        public Repository<Region> Rep_Region { get; }
        public Repository<Ville> Rep_Ville { get; }
        public Repository<Categories> Rep_Categories { get; }
        public UserManager<AppUsers> UserManager { get; }
        public SignInManager<AppUsers> SignInManager { get; }
        public IHostingEnvironment Hosting { get; }

        // GET: Annoces

        public AnnocesController(Repository<Annonces> rep_Annonces,
            Repository<Region> rep_Region,
            Repository<Ville> rep_Ville,
            Repository<Categories> rep_Categories,
            SignInManager<AppUsers> signInManager,
            UserManager<AppUsers> userManager,
           IHostingEnvironment hosting)
        {
            Rep_Annonces = rep_Annonces;
            Rep_Region = rep_Region;
            Rep_Ville = rep_Ville;
            Rep_Categories = rep_Categories;
            UserManager = userManager;
            SignInManager = signInManager;
            Hosting = hosting;
        }
        public async Task<IActionResult> Index()
        {
            var user = await UserManager.GetUserAsync(User);
            
            if (SignInManager.IsSignedIn(User))
            {
                var List_Annonces = Rep_Annonces.List().Where(a=>a.users==user);
                return View(List_Annonces);
            }
            return RedirectToAction("login", "Account");
        }
    

        // GET: Annoces/Details/5
        public ActionResult Details(int id)
        {
            if (SignInManager.IsSignedIn(User))
            {
                return View(find_Annonces(id));
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Annoces/Create
        public ActionResult Create()
        {
            if (SignInManager.IsSignedIn(User))
            {
                var model = new Create_AnnonceViewModel()
                {
                    region = Rep_Region.List(),
                    ville = Rep_Ville.List(),
                    categories = Rep_Categories.List()
                };
                return View(model);
            }
            return RedirectToAction("login", "Account");

        }

        // POST: Annoces/Create
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Create(Create_AnnonceViewModel model)
        {
            if (SignInManager.IsSignedIn(User))
            {
                int pos = 0;
                
                try
            {
                    var user_Id = UserManager.GetUserId(User);
                    var user = UserManager.Users.SingleOrDefault(u => u.Id == user_Id);
                    if (user.UserType == "Admin")
                    {
                        pos = 2;
                    }
                    else
                    {
                        pos = 1;
                    }
                    string filleName = "";
                    if (model.file.FileName != "")
                    {
                        string chemain = Path.Combine(Hosting.WebRootPath, "Image_Ann");
                        filleName = model.file.FileName;
                        string path = Path.Combine(chemain, filleName);
                        model.file.CopyTo(new FileStream(path, FileMode.Create));

                        var Find_Cat = Rep_Categories.Find(model.id_Cat);
                        var Find_Region = Rep_Region.Find(model.id_region);
                        var Find_Ville = Rep_Ville.Find(model.id_Ville);
                        var Ann = new Annonces()
                        {
                            users=user,
                            date_Publiciter = DateTime.Now,
                            Ann_Email = model.Ann_Email,
                            Code_Postal = model.Code_Postal,
                            Ann_Texte = model.Ann_Texte,
                            pos = pos,
                            ville = Find_Ville,
                            categories = Find_Cat,
                            region = Find_Region,
                            Telephone=model.Telephone,
                            images = filleName
                        };
                        Rep_Annonces.Add(Ann);
                        return RedirectToAction(nameof(Index));
                    }
                }
            catch(Exception ex)
            {
                    return View(ex.Message);
            }
        
        var list = new Create_AnnonceViewModel()
        {
            region = Rep_Region.List(),
            ville = Rep_Ville.List(),
            categories = Rep_Categories.List()
        };
                return View(model);
            }
            return RedirectToAction("login", "Account");
        }


        // GET: Annoces/Edit/5
        public ActionResult Edit(int id)
            {
                if (SignInManager.IsSignedIn(User))
                {
                var find_Ann = Rep_Annonces.Find(id);
                var model = new Create_AnnonceViewModel()
                {
                    Ann_Email = find_Ann.Ann_Email,
                    Ann_Texte = find_Ann.Ann_Texte,
                    categories = Rep_Categories.List(),
                    id_Cat = find_Ann.categories.id,
                    id_region = find_Ann.region.id,
                    id_Ville = find_Ann.ville.id,
                    region = Rep_Region.List(),
                    ville = Rep_Ville.List(),
                    Telephone = find_Ann.Telephone,
                    images = find_Ann.images,
                    pos = find_Ann.pos,
                    Code_Postal = find_Ann.Code_Postal
                };
                return View(model);
            }
            return RedirectToAction("login", "Account");
        }

        // POST: Annoces/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Create_AnnonceViewModel model)
        {
            string file_Name = "";
            int pos = 0;
            if (SignInManager.IsSignedIn(User))
            {
                try
            {
                    var find_Cat = Rep_Categories.Find(model.id_Cat);
                    var find_Ann = Rep_Annonces.Find(id);
                    var find_Reg = Rep_Region.Find(model.id_region);
                    var find_ville = Rep_Ville.Find(model.id_Ville);
                    model.images = find_Ann.images;
                    if (model.file == null)
                    {
                        file_Name = find_Ann.images;
                    }
                    else
                    {
                        string chemain = Path.Combine(Hosting.WebRootPath, "Image_Ann");
                        file_Name = model.file.FileName;
                        string new_Path = Path.Combine(chemain, file_Name);
                        string old_Path = Path.Combine(chemain, model.images);
                        if (new_Path != old_Path)
                        {
                            System.IO.File.Delete(old_Path);
                            model.file.CopyTo(new FileStream(new_Path, FileMode.Create));
                        }
                    }
                   
                    var user = await UserManager.GetUserAsync(User);
                    if (user.UserType == "Admin")
                    {
                        pos = 2;
                    }
                    else
                    {
                        pos = 1;
                    }

                    find_Ann.Ann_Email = model.Ann_Email;
                    find_Ann.Code_Postal = model.Code_Postal;
                    find_Ann.Ann_Texte = model.Ann_Texte;
                    find_Ann.pos = pos;
                    find_Ann.ville = find_ville;
                    find_Ann.categories = find_Cat;
                    find_Ann.region = find_Reg;
                    find_Ann.Telephone = model.Telephone;
                    find_Ann.images = file_Name;
                    Rep_Annonces.Update(id, find_Ann);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                    model.categories = Rep_Categories.List();
                    model.region = Rep_Region.List();
                    model.ville = Rep_Ville.List();
                return View(model);
                }
            }
            return RedirectToAction("login", "Account");

        }

        // GET: Annoces/Delete/5
        public ActionResult Delete(int id)
        {
            if (SignInManager.IsSignedIn(User))
            {
                return View(find_Annonces(id));
            }
            return RedirectToAction("login", "Account");
        }

        // POST: Annoces/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            if (SignInManager.IsSignedIn(User))
            {
                try
            {
                Rep_Annonces.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
                }
            }
            return RedirectToAction("login", "Account");

        }
        public Annonces find_Annonces(int id)
        {
            var find = Rep_Annonces.Find(id);
            return find;
        }
    }
}