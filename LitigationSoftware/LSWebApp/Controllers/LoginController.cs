using Newtonsoft.Json;
using Ninject;
using LS.Models;
//using LSWebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net;
using System.Net.Sockets;
using System.Web.Security;
using LSWebApp.Models;

namespace LSWebApp.Controllers
{
    public class LoginController : ControllerBase
    {
        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor

        public LoginController()
        {
            _Kernel = new StandardKernel();
            _Kernel.Load(new LS.Modules.SignInModule());
        }

        #endregion

        // GET: Login
        public ActionResult Index()
        {
            return View(new UserLogin());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Signin(UserLogin user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/LoginAPI/PostUserLogin", content);

                if (Res.IsSuccessStatusCode)
                {
                    var objSignInResponse = JsonConvert.DeserializeObject<UserLogin>(Res.Content.ReadAsStringAsync().Result);
                    if (objSignInResponse.Id > 0)
                    {
                        Session[SESSION_LOGON_USER] = user;
                        Session["User"] = objSignInResponse;
                        FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);
                        //RedirectToAction("Index", "TaxReturn");

                        var model = new CompanyModel();
                        Res = await client.GetAsync("api/MasterAPI/GetCompanyCategoryList");

                        if (Res.IsSuccessStatusCode)
                            {
                                model.CompanyCategoriesList = JsonConvert.DeserializeObject<List<CompanyCategory>>(Res.Content.ReadAsStringAsync().Result);
                                return View("~/Views/TaxReturn/Index.cshtml", model);
                            }
                        else
                        {
                            user.Message = "Incorrect username or password";
                            return View("Index", user);
                        }

                    }
                    else
                    {
                        user.Message = "Incorrect username or password";
                        return View("Index", user);
                    }
                }
                else
                {
                    user.Message = "Incorrect username or password";
                    return View("Index", user);
                }
            }
        }

        [AllowAnonymous]
        public ActionResult UnAuthorizedAjaxCall(string message)
        {
            return Json(new { Success = false, Message = message }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult Signout()
        {
            Session[SESSION_LOGON_USER] = null;
            Session["User"] = null;
            return View("Index");
        }
    }
}