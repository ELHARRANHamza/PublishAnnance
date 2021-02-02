using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Annonce_Asp.netCore.Models;
using Annonce_Asp.netCore.Models.Repository;
using Annonce_Asp.netCore.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Annonce_Asp.netCore.Controllers
{
    public class HomeController : Controller
    {
        public SignInManager<AppUsers> SignInManager { get; }
        public Repository<Annonces> Rep_Ann { get; }
        public Repository<Categories> Rep_Cat { get; }
        public Repository<Region> Rep_Region { get; }
        public Repository<Ville> Rep_Ville { get; }
        public Repository<Latest_News> Rep_News { get; }

        public HomeController(SignInManager<AppUsers> signInManager,
            Repository<Annonces> Rep_Ann,
            Repository<Categories> Rep_Cat,
            Repository<Region> Rep_region,
            Repository<Ville> rep_Ville,
             Repository<Latest_News> rep_News
            )
        {
            SignInManager = signInManager;
            this.Rep_Ann = Rep_Ann;
            this.Rep_Cat = Rep_Cat;
            Rep_Region = Rep_region;
            Rep_Ville = rep_Ville;
            Rep_News = rep_News;
        }
        // GET: Home
        public ActionResult Index()
        {

            var Listee = new Liste_AnnViewModel()
            {
                GetAnnonces = Rep_Ann.List().Where(a => a.pos == 2).ToList(),
                GetCategories = Rep_Cat.List(),
                news = Rep_News.List().OrderByDescending(n => n.date_Publiciter).ToList()
            };
            return View(Listee);

        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            if (SignInManager.IsSignedIn(User))
            {
                var find_Ann = Rep_Ann.Find(id);
                return View(find_Ann);
            }
            return RedirectToAction("login", "Account");
        }
        public ActionResult Details_Cat(int id)
        {
            if (SignInManager.IsSignedIn(User))
            {
                var find_Cat = Rep_Cat.Find(id);
                var List_Ville = Rep_Ville.List();
                var List_Region = Rep_Region.List();
                var Liste_Cat = Rep_Cat.List().Where(c => c.id != id).ToList();
                var list_Ann = Rep_Ann.List().Where(a => a.categories == find_Cat).OrderByDescending(a => a.date_Publiciter).ToList();
                var model = new Details_CatViewModel()
                {
                    id = id,
                    Cat = find_Cat,
                    GetVilles = List_Ville,
                    GetContreCat = Liste_Cat,
                    GetRegions = List_Region,
                    GetAnnonces = list_Ann

                };
                return View(model);
            }
            return RedirectToAction("login", "Account");
        }
        public ActionResult Details_Users(string id)
        {
            var annonces = Rep_Ann.List().Where(a => a.users.Id == id);
            var Listee = new Liste_AnnViewModel()
            {
                GetAnnonces = annonces.ToList(),
                GetCategories = Rep_Cat.List()
            };
            return View(Listee);
        }

        public ActionResult Search_Region(int id, int? id1)
        {
            if (SignInManager.IsSignedIn(User))
            {
                var Reg = Rep_Region.List().SingleOrDefault(r => r.id == id1);
                var find_Cat = Rep_Cat.Find(id);
                var List_Ville = Rep_Ville.List();
                var List_Region = Rep_Region.List();
                var Liste_Cat = Rep_Cat.List().Where(c => c.id != id).ToList();
                var list_Ann = Rep_Ann.List().Where(a => a.categories == find_Cat && a.region == Reg).OrderByDescending(a => a.date_Publiciter).ToList();

                var model = new Details_CatViewModel()
                {
                    Cat = find_Cat,
                    GetVilles = List_Ville,
                    GetContreCat = Liste_Cat,
                    GetRegions = List_Region,
                    GetAnnonces = list_Ann

                };
                return View("Details_Cat", model);
            }
            return RedirectToAction("login", "Account");
        }

        public ActionResult Search_Ville(int id, int? id1)
        {
            if (SignInManager.IsSignedIn(User))
            {
                var ville = Rep_Ville.List().SingleOrDefault(r => r.id == id1);
                var find_Cat = Rep_Cat.Find(id);
                var List_Ville = Rep_Ville.List();
                var List_Region = Rep_Region.List();
                var Liste_Cat = Rep_Cat.List().Where(c => c.id != id).ToList();
                var list_Ann = Rep_Ann.List().Where(a => a.categories == find_Cat && a.ville == ville).OrderByDescending(a => a.date_Publiciter).ToList();

                var model = new Details_CatViewModel()
                {
                    Cat = find_Cat,
                    GetVilles = List_Ville,
                    GetContreCat = Liste_Cat,
                    GetRegions = List_Region,
                    GetAnnonces = list_Ann

                };
                return View("Details_Cat", model);
            }
            return RedirectToAction("login", "Account");
        }
        public ActionResult Categorie()
        {
            if (SignInManager.IsSignedIn(User))
            {
                var cat = Rep_Cat.List();
                return View(cat);
            }
            return RedirectToAction("login", "Account");
        }
        public ActionResult Search(string Cherche)
        {

            var findAnnonce = Rep_Ann.List().Where(a => a.users.Email == Cherche
            || a.ville.ville_Name == Cherche
            || a.categories.nom_Cat == Cherche ||
            a.users.nom == Cherche ||
            a.users.prenom == Cherche ||
            a.Code_Postal == Cherche);
            IList<Annonces> annonces;
            if (Cherche ==null)
            {
                annonces = Rep_Ann.List().Where(a => a.pos == 2).ToList();
            }
            else
            {
              annonces=findAnnonce.ToList();
            }
            var Listee = new Liste_AnnViewModel()
            {
                GetAnnonces = annonces,
                GetCategories = Rep_Cat.List(),
                news = Rep_News.List().OrderByDescending(n => n.date_Publiciter).ToList()
            };
            return View("Index", Listee);

        }
    }
}