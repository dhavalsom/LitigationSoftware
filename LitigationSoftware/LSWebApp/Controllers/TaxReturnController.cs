﻿using LS.Models;
using LSWebApp.Models;
using Newtonsoft.Json;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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

        #region Methods
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
        public async Task<ActionResult> GetITReturnDetails(int userId, int companyId, string companyname)
        {
            ITReturnDetailsModel itrdetails = new ITReturnDetailsModel
            {
                ITReturnDetailsObject = new ITReturnDetails
                {
                    CompanyID = companyId,
                    CompanyName = companyname,
                    AddedBy = userId,
                    IncomefromBusinessProf = false,
                    RevisedReturnFile = false
                }
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetFYAYList");
                if (Res.IsSuccessStatusCode)
                {
                    itrdetails.FYAYList = JsonConvert.DeserializeObject<List<FYAY>>(Res.Content.ReadAsStringAsync().Result);
                }
                Res = await client.GetAsync("api/MasterAPI/GetITSubHeadMaster?itHeadId=");
                var itSubHeads = JsonConvert.DeserializeObject<List<ITSubHeadMaster>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/MasterAPI/GetITHeadMaster");
                var itHeads = JsonConvert.DeserializeObject<List<ITHeadMaster>>(Res.Content.ReadAsStringAsync().Result);

                Res = await client.GetAsync("api/MasterAPI/GetITSectionList");
                if (Res.IsSuccessStatusCode)
                {
                    itrdetails.ITSectionList = JsonConvert.DeserializeObject<List<ITSection>>(Res.Content.ReadAsStringAsync().Result);
                    itrdetails.ITSectionListSource = new LitigationDDModel(
                       itrdetails.ITSectionList.Select(x =>
                        new SelectListItem() { Value = x.Id.ToString(), Text = x.Description }).ToList()
                       , "ITReturnDetailsObject.ITSectionID"
                       , "manageITSection"
                       , "getITSections"
                    );
                }
                itrdetails.PopulateITHeadMasters(itHeads, itSubHeads);
            }
            return View(itrdetails);
        }

        [HttpGet]
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

        [HttpGet]
        public async Task<ActionResult> GetITSectionList()
        {
            var model = new List<ITSection>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetITSectionList");

                if (Res.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<List<ITSection>>(Res.Content.ReadAsStringAsync().Result);
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertorUpdateITReturnDetails(ITReturnDetailsModel itReturn, FormCollection form)
        {
            foreach (var control in form.Keys)
            {
                if (control.ToString().StartsWith("txtITSubHead_"))
                {
                    decimal value;
                    if (decimal.TryParse(form[control.ToString()].ToString(), out value)) {
                        if (value != 0) {
                            itReturn.ExtensionList.Add(new ITReturnDetailsExtension
                            {
                                Id = itReturn.ITReturnDetailsObject.Id,
                                ITSubHeadValue = value,
                                ITSubHeadId =int.Parse(control.ToString().Replace("txtITSubHead_", ""))
                            });
                        }
                    }
                }
            }
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(itReturn.ITReturnDetailsObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync("api/TaxReturnAPI/InsertorUpdateITReturnDetails", content);
                ITReturnDetailsResponse result = new ITReturnDetailsResponse();
                if (Res.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;
                    result.Message = Res.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = Res.Content.ReadAsStringAsync().Result;

                }
                return View("ITReturnResponse", result);
            }
        }

        [HttpPost]
        public async Task<ActionResult> InsertUpdateITSection(ITSection objITSection)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(objITSection);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync("api/MasterAPI/InsertUpdateITSection", content);
                ITSectionResponse result = new ITSectionResponse()
                {
                    IsSuccess = Res.IsSuccessStatusCode,
                    Message = Res.Content.ReadAsStringAsync().Result
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> InsertUpdateITSubHeadMaster(ITSubHeadMaster objITSubHeadMaster)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(objITSubHeadMaster);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync("api/MasterAPI/InsertUpdateITSubHeadMaster", content);
                ITSubHeadMasterResponse result = JsonConvert.DeserializeObject<ITSubHeadMasterResponse>(Res.Content.ReadAsStringAsync().Result);                
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetComplianceList()
        {
            ComplianceListModel model = new ComplianceListModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetFYAYList");
                model.FYAYList = JsonConvert.DeserializeObject<List<FYAY>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/MasterAPI/GetCompanyList");
                model.CompanyList = JsonConvert.DeserializeObject<List<Company>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/MasterAPI/GetComplianceMaster?complianceId=");
                model.ComplianceList = JsonConvert.DeserializeObject<List<ComplianceMaster>>(Res.Content.ReadAsStringAsync().Result);
            }

            return View("ComplianceList", model);
        }

        [HttpGet]
        public async Task<ActionResult> GetComplianceDocumentList(int companyId, int fyayId)
        {
            ComplianceDocumentListModel model = new ComplianceDocumentListModel()
            {
                CompanyId = companyId,
                FYAYId = fyayId
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetComplianceMaster?complianceId=");
                model.ComplianceList = JsonConvert.DeserializeObject<List<ComplianceMaster>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/TaxReturnAPI/GetComplianceDocumentsList?companyId=" + companyId + "&fyayId=" + fyayId
                     + "&complianceId=&complianceDocumentId=");
                model.ComplianceDocumentList = JsonConvert.DeserializeObject<ComplianceDocumentsResponse>(Res.Content.ReadAsStringAsync().Result).ComplianceDocumentsList;
            }

            return PartialView("ComplianceDocumentList", model);
        }

        [HttpPost]
        public async Task<ActionResult> InsertUpdateComplianceDocuments(ComplianceListModel objComplianceListModel)
        {
            string path = Server.MapPath("~/ComplianceDocumentsUpload/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            objComplianceListModel.ReportFile.SaveAs(path + Path.GetFileName(objComplianceListModel.ReportFile.FileName));
            objComplianceListModel.ObjComplianceDocuments.FileName = objComplianceListModel.ReportFile.FileName;
            objComplianceListModel.ObjComplianceDocuments.FilePath = path + objComplianceListModel.ReportFile.FileName;
            objComplianceListModel.ObjComplianceDocuments.AddedBy = 1;
            objComplianceListModel.ObjComplianceDocuments.ModifiedBy = 1;
            objComplianceListModel.ObjComplianceDocuments.Active = true;

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(objComplianceListModel.ObjComplianceDocuments);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync("api/TaxReturnAPI/InsertUpdateComplianceDocuments", content);
                ComplianceDocumentsResponse result = JsonConvert.DeserializeObject<ComplianceDocumentsResponse>(Res.Content.ReadAsStringAsync().Result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}