using Newtonsoft.Json;
using Ninject;
using LS.Models;
using LSWebApp.Models;
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
    public class TaxReturnController : Controller
    {

        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor

        public TaxReturnController()
        {
            _Kernel = new StandardKernel();
            _Kernel.Load(new LS.Modules.SignInModule());
        }

        #endregion

        // GET: TaxReturn
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCompany(Company comp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(comp);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/MasterAPI/PostCreateCompany", content);
                var model = new CompanyList();
                if (Res.IsSuccessStatusCode)
                {
                    var objCompResponse = JsonConvert.DeserializeObject<bool>(Res.Content.ReadAsStringAsync().Result);
                    if (objCompResponse)
                    {
                        content = null;
                        Res = null;

                        Res = await client.GetAsync("api/MasterAPI/GetCompanyList");

                        if (Res.IsSuccessStatusCode)
                        {
                            model.Companies = JsonConvert.DeserializeObject<List<Company>>(Res.Content.ReadAsStringAsync().Result);

                           // RedirectToAction("CompanyList", "TaxReturn");
                            return View("GetCompanyList", model);
                        }

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
                        return View("CompanyRegistrationFailure", objCompResponse);
                    }
                    else
                        return View("CompanyRegistrationFailure", objCompResponse);
                }
                else
                    return View("CompanyRegistrationFailure");

            }
        }


        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult>GetITReturnDetails(int userId,int companyId,string companyname)
        {
            ITReturnDetailsModel itrdetails = new ITReturnDetailsModel();
            //ITReturnDetails itrdetails = new ITReturnDetails();
            itrdetails.ITReturnDetailsObject.CompanyID = companyId;
            itrdetails.ITReturnDetailsObject.CompanyName = companyname;
            itrdetails.ITReturnDetailsObject.AddedBy = userId;
            itrdetails.ITReturnDetailsObject.IncomefromBusinessProf = false;
            itrdetails.ITReturnDetailsObject.RevisedReturnFile = false;
            //var model = new FYAY();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetFYAYList");

                if (Res.IsSuccessStatusCode)
                {
                    itrdetails.FYAYList = JsonConvert.DeserializeObject<List<FYAY>>(Res.Content.ReadAsStringAsync().Result);
                    // RedirectToAction("CompanyList", "TaxReturn");
                }

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Res = await client.GetAsync("api/MasterAPI/GetITSectionList");

                if (Res.IsSuccessStatusCode)
                {
                    itrdetails.ITSectionList = JsonConvert.DeserializeObject<List<ITSection>>(Res.Content.ReadAsStringAsync().Result);
                    // RedirectToAction("CompanyList", "TaxReturn");
                }
            }
            

            return View(itrdetails);
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> GetCompanyList()
        {
            var model = new CompanyList();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetCompanyList");

                if (Res.IsSuccessStatusCode)
                {
                    model.Companies = JsonConvert.DeserializeObject<List<Company>>(Res.Content.ReadAsStringAsync().Result);
                    // RedirectToAction("CompanyList", "TaxReturn");
                }
            }
            return View("GetCompanyList", model);
        }
    }
}