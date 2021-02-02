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
    public class Affichage_Latests_NewsController : Controller
    {
        public Repository<Latest_News> Repository { get; }
        public SignInManager<AppUsers> SignInManager { get; }

        public Affichage_Latests_NewsController(Repository<Latest_News> repository,
            SignInManager<AppUsers> signInManager)
        {
            Repository = repository;
            SignInManager = signInManager;
        }
        // GET: Affichage_Latests_News
        public ActionResult Index()
        {

            var List = Repository.List().OrderByDescending(a =>a.date_Publiciter);
            return View(List);
        }

        // GET: Affichage_Latests_News/Details/5
        public ActionResult Details(int id)
        {
            if (SignInManager.IsSignedIn(User))
            {
                var find_News = Repository.Find(id);
                return View(find_News);
            }
            return RedirectToAction("login","Account");
        }


    }
}