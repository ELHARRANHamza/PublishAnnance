using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Annonce_Asp.netCore.Models;
using Annonce_Asp.netCore.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace Annonce_Asp.netCore.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<AppUsers> UserManager { get; }
        public SignInManager<AppUsers> SignInManager { get; }

        public AccountController(UserManager<AppUsers> userManager,SignInManager<AppUsers> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpGet]
        public ActionResult Registre()
        {
            return View();
        }

        // GET: Account/Details/5
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Registre(Registre_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUsers()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    nom=model.nom,
                    prenom=model.prenom
                };
                var result = await UserManager.CreateAsync(user, model.password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index","Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]

        // GET: Account/Create
        public ActionResult login()
        {
            return View();
        }
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(loginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }
        public async Task<IActionResult> logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("login");
        }
        public ActionResult Contact(Email_Models email_)
        {
            //using (var message = new MailMessage())
            //{
            //    message.To.Add(new MailAddress("to@email.com", "To Name"));
            //    message.From = new MailAddress("from@email.com", "From Name");
            //    message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
            //    message.Bcc.Add(new MailAddress("bcc@email.com", "BCC Name"));
            //    message.Subject = email_.Subject;
            //    message.Body = email_.body;
            //    message.IsBodyHtml = true;

            //    using (var client = new SmtpClient("smtp.gmail.com"))
            //    {
            //        client.Port = 587;
            //        client.Credentials = new NetworkCredential("send-address@gmail.com", "password");
            //        client.EnableSsl = true;
            //        client.Send(email_.body);
            //    }
            //}
                return View();
        }
    }
}