using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BETApplicationMVC.Shopify.Models;
using BETApplicationMVC.Shopify.Data;
using BETApplicationMVC.Shopify.Logic;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BETApplicationMVC.Shopify.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public const string AffiliateSessionKey = "Affiliate_Key";
      
         Customer_Service customer_Service;

        public AccountController()
        {
         
            this.customer_Service = new Customer_Service();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;

        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
           
            var result = await customer_Service.LogUser(model);


            if (result)
            {
                TempData["name"] = model.Email;
                return RedirectToAction("Landpage", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }


        private Task LogUser(string email, string password, bool rememberMe, bool shouldLockout)
        {
            throw new NotImplementedException();
        }

       
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                System.Web.HttpContext.Current.Session[AffiliateSessionKey] = id;
            }
            else
                System.Web.HttpContext.Current.Session[AffiliateSessionKey] = "";
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                    model.UserName = model.Email;
                    var result = await customer_Service.AddCustomer(model); //serManager.CreateAsync(user, model.Password);
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Login");
        }

      
     

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }
   }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
      


        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

   

        public ActionResult Details()
        {
            if (!String.IsNullOrEmpty(User.Identity.Name))
            {
                Customer_Service customer = new Customer_Service();
                if (customer.GetCustomers().FirstOrDefault(x => x.Email == User.Identity.Name) != null)
                {
                    return View(customer.GetCustomers().FirstOrDefault(x => x.Email == User.Identity.Name));
                }
                else
                    return RedirectToAction("Login");
            }
            else
                return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}