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

namespace LSWebApp.Controllers
{
    public class LoginController : Controller
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Signin(UserLogin user)
        {
            user.IPAddress = string.IsNullOrEmpty(user.IPAddress)?GetLocalIPAddress():user.IPAddress;

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
                    var objSignInResponse = JsonConvert.DeserializeObject<SignInResponse>(Res.Content.ReadAsStringAsync().Result);
                    if (objSignInResponse.IsPasswordVerified
                        && objSignInResponse.IsUserActive)
                    {
                        RedirectToAction("Index", "TaxReturn");
                        return View("~/Views/TaxReturn/Index.cshtml");
                        //if (objSignInResponse.TwoFactorAuthDone)
                        //{
                        //return View(objSignInResponse);
                        //}
                        //else
                        //{
                        //    //var model = new GetAccessCodeModel
                        //    //{
                        //    //    ObjSignInResponse = objSignInResponse,
                        //    //    IPAddress = user.IPAddress,
                        //    //    UserName = user.Username,
                        //    //    Method = "Email"
                        //    //};
                        //    //return View("GetAccessCode", model);
                        //    return View("Index");
                        //}
                    }
                    else
                        return View("LoginFailure", objSignInResponse);
                }
                else
                    return View("LoginFailure", new SignInResponse { IsPasswordVerified = false });

            }

        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}