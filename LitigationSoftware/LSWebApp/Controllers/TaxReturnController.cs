using LS.Models;
using LSWebApp.Infrastructure;
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
using System.Web.Routing;

namespace LSWebApp.Controllers
{
    [LitigationAuthorizeAttribute]
    public class TaxReturnController : ControllerBase
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
        //[HttpGet]
        public ActionResult Index()
        {
            var model = new CompanyCategoryModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetCompanyCategoryList");

                //if (Res.IsSuccessStatusCode)
                {
                    //model.CompanyCategories = JsonConvert.DeserializeObject<List<CompanyCategory>>(Res.Content.ReadAsStringAsync().Result);
                    //return View(model);
                }
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCompany(CompanyModel comp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(comp.companyObject);
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
                            return View("GetCompanyList", model);
                        }
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
        public async Task<ActionResult> GetITReturnDetails(int? userId, int companyId, string companyname,int FYAYID, int? itsectionid, int? itreturnid,int? itsectioncategoryid)
        {
            ITReturnDetailsModel itrdetails = await CommonGetITReturnDetails(userId, companyId, companyname, FYAYID, itsectionid, itreturnid, itsectioncategoryid);
            return View(itrdetails);
        }

        private async Task<ITReturnDetailsModel> CommonGetITReturnDetails(int? userId, int companyId
            , string companyname, int FYAYID, int? itsectionid, int? itreturnid, int? itsectioncategoryid)
        {
            ITReturnDetailsModel itrdetails = new ITReturnDetailsModel
            {
                ITReturnDetailsObject = new ITReturnDetails
                {
                    CompanyID = companyId,
                    CompanyName = companyname,
                    AddedBy = userId,
                    Broughtforwardlosses = false,
                    FYAYID = FYAYID,
                    ITSectionID = itsectionid.HasValue ? itsectionid.Value : 0,
                    ITSectionCategoryID = itsectioncategoryid.HasValue ? itsectioncategoryid.Value : 0,
                    IsReturn = true
                }
            };

            try
            {
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
                    Res = await client.GetAsync("api/MasterAPI/GetITSectionCategoryList");
                    if (Res.IsSuccessStatusCode)
                    {
                        itrdetails.ITSectionCategoryList = JsonConvert.DeserializeObject<List<ITSectionCategory>>(Res.Content.ReadAsStringAsync().Result);
                        itrdetails.ITSectionCategorySelectItems = itrdetails.ITSectionCategoryList.Select(x =>
                           new SelectListItem()
                           {
                               Value = x.Id.ToString(),
                               Text = x.Description,
                               Selected = x.Id == itrdetails.ITReturnDetailsObject.ITSectionCategoryID,
                           }).ToList();


                        Res = await client.GetAsync("api/MasterAPI/GetITSectionList?categoryId="+ (itrdetails.ITReturnDetailsObject.ITSectionCategoryID != 0 ? itrdetails.ITReturnDetailsObject.ITSectionCategoryID : 0 ));
                        if (Res.IsSuccessStatusCode)
                        {
                            itrdetails.ITSectionList = JsonConvert.DeserializeObject<List<ITSection>>(Res.Content.ReadAsStringAsync().Result);
                            itrdetails.ITSectionListSource = new LitigationDDModel(
                               itrdetails.ITSectionList.Select(x =>
                                new SelectListItem()
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Description,
                                    Selected = x.Id == itrdetails.ITReturnDetailsObject.ITSectionID,
                                }).ToList()
                               , "ITReturnDetailsObject.ITSectionID"
                               , "manageITSection"
                               , "getITSections"
                            );
                            
                        }

                    }

                    Res = await client.GetAsync("api/MasterAPI/GetITSubHeadMaster?itHeadId=");
                    var itSubHeads = JsonConvert.DeserializeObject<List<ITSubHeadMaster>>(Res.Content.ReadAsStringAsync().Result);
                    Res = await client.GetAsync("api/MasterAPI/GetITHeadMaster");
                    var itHeads = JsonConvert.DeserializeObject<List<ITHeadMaster>>(Res.Content.ReadAsStringAsync().Result);

                    if (itsectionid.HasValue)
                    {
                        Res = await client.GetAsync("api/TaxReturnAPI/GetExistingITReturnDetailsList?companyId=" + companyId + "&fyayId=" + FYAYID + "&itsectionid=" + itsectionid + "&itreturnid=" + itreturnid);
                        if (Res.IsSuccessStatusCode)
                        {
                            if (JsonConvert.DeserializeObject<ITReturnDetailsListResponse>(Res.Content.ReadAsStringAsync().Result).ITReturnDetailsListObject.Count > 0)
                            {
                                itrdetails.ITReturnDetailsObject = JsonConvert.DeserializeObject<ITReturnDetailsListResponse>
                                    (Res.Content.ReadAsStringAsync().Result).ITReturnDetailsListObject.First<ITReturnDetails>();
                                Res = await client.GetAsync("api/TaxReturnAPI/GetITReturnDocumentsList?companyId=&fyayId=&itReturnDetailsId="
                                    + itrdetails.ITReturnDetailsObject.Id + "&itHeadId=&itReturnDocumentId=");
                                if (Res.IsSuccessStatusCode)
                                {
                                    var objITReturnDocumentsResponse = JsonConvert.DeserializeObject<ITReturnDocumentsResponse>
                                        (Res.Content.ReadAsStringAsync().Result);
                                    itrdetails.ITReturnDocumentList = new Dictionary<string, List<ITReturnDocumentsDisplay>>();
                                    foreach (var item in objITReturnDocumentsResponse.ITReturnDocumentsList)
                                    {
                                        if (itrdetails.ITReturnDocumentList.ContainsKey(item.PropertyName))
                                        {
                                            itrdetails.ITReturnDocumentList[item.PropertyName].Add(item);
                                        }
                                        else
                                        {
                                            itrdetails.ITReturnDocumentList.Add(item.PropertyName,
                                                new List<ITReturnDocumentsDisplay> { item });
                                        }
                                    }
                                    itrdetails.ITHeadDocumentsUploaderModels = new Dictionary<string, ITHeadDocumentsUploaderModel>();
                                    foreach (var itHead in itHeads)
                                    {
                                        if (itHead.CanAddDocuments)
                                        {
                                            if (itrdetails.ITReturnDocumentList.ContainsKey(itHead.PropertyName))
                                            {
                                                itrdetails.ITHeadDocumentsUploaderModels.Add(itHead.PropertyName
                                                    , new ITHeadDocumentsUploaderModel(itrdetails.ITReturnDocumentList[itHead.PropertyName]
                                                    , itHead, itrdetails.ITReturnDetailsObject));
                                            }
                                            else
                                            {
                                                itrdetails.ITHeadDocumentsUploaderModels.Add(itHead.PropertyName
                                                    , new ITHeadDocumentsUploaderModel(new List<ITReturnDocumentsDisplay>()
                                                    , itHead, itrdetails.ITReturnDetailsObject));
                                            }
                                        }
                                    }
                                }
                            }

                            //itrdetails.ITReturnDetailsObject.IsReturn = itrdetails.ITSectionList.Where(x => x.Id == itrdetails.ITReturnDetailsObject.ITSectionID)
                            //                                                .Select(x => x.IsReturn).First();
                            
                        }
                    }

                    itrdetails.PopulateITHeadMasters(itHeads, itSubHeads, itrdetails.ITReturnDetailsObject.Id);

                    if (!itrdetails.ITReturnDetailsObject.IsReturn)
                    {
                        List<ITReturnDetailsExtension> itSubHeadValues = new List<ITReturnDetailsExtension>();

                        Res = await client.GetAsync("api/TaxReturnAPI/GetExistingITReturnDetailsExtension?itreturnid=" + itrdetails.ITReturnDetailsObject.Id);
                        if (Res.IsSuccessStatusCode)
                        {
                            if (JsonConvert.DeserializeObject<List<ITReturnDetailsExtension>>(Res.Content.ReadAsStringAsync().Result).Count > 0)
                            {
                                itSubHeadValues = JsonConvert.DeserializeObject<List<ITReturnDetailsExtension>>(Res.Content.ReadAsStringAsync().Result);
                            }
                        }
 
                        if (itSubHeadValues.Count > 0)
                        {
                            foreach(var item in itrdetails.ExtensionList)
                            {
                                foreach(var subvalue in itSubHeadValues)
                                {
                                    if (subvalue.ITReturnDetailsId == item.ITReturnDetailsId && subvalue.ITSubHeadId == item.ITSubHeadId)
                                        item.ITSubHeadValue = subvalue.ITSubHeadValue;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return itrdetails;
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
        public async Task<ActionResult> GetITSectionList(int categoryId)
        {
            var model = new List<ITSection>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetITSectionList?categoryId="+ categoryId);

                if (Res.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<List<ITSection>>(Res.Content.ReadAsStringAsync().Result);
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> ExistingITReturnDetails(ITReturnDetails itrdetails1)
        {
            ITReturnDetailsModel itrdetails = new ITReturnDetailsModel
            {
                ITReturnDetailsObject = new ITReturnDetails
                {
                    CompanyID = itrdetails1.CompanyID,
                    CompanyName = itrdetails1.CompanyName,
                    AddedBy = itrdetails1.AddedBy,
                    Broughtforwardlosses = false
                    //IncomefromBusinessProf = false,
                    //RevisedReturnFile = false
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
            }

                return View(itrdetails);
        }

        [HttpGet]
        public async Task<ActionResult> SearchITReturnDetails(int companyId, int fyayId)
        {
            ITReturnDetailsListModel itrdetail = new ITReturnDetailsListModel()
            {
                CompanyId = companyId,
                FYAYId = fyayId,
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/TaxReturnAPI/GetExistingITReturnDetailsList?companyId=" + companyId + "&fyayId=" + fyayId + "&itsectionid=0&itreturnid=0");
                if (Res.IsSuccessStatusCode)
                {
                    itrdetail.ITReturnDetailsListObject = JsonConvert.DeserializeObject<ITReturnDetailsListResponse>(Res.Content.ReadAsStringAsync().Result).ITReturnDetailsListObject;
                }
            }

            return PartialView("ExistingSectionWiseDetails", itrdetail);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteITReturnDocuments(ITReturnDocuments itReturnDocuments)
        {
            itReturnDocuments.DeletedBy = (Session[SESSION_LOGON_USER] as UserLogin).Id;
            string relativePath = "/" + itReturnDocuments.FilePath;
            string path = Server.MapPath("~/ITReturnDetailsDocumentsUpload" + relativePath);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(itReturnDocuments);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync("api/TaxReturnAPI/DeleteITReturnDocuments", content);
                ITReturnDocumentsResponse result = JsonConvert.DeserializeObject<ITReturnDocumentsResponse>(Res.Content.ReadAsStringAsync().Result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> InsertUpdateITReturnDocuments
            (ITHeadDocumentsUploaderModel objITHeadDocumentsUploaderModel)
        {
            string relativePath = "/" + objITHeadDocumentsUploaderModel.ObjITReturnDocuments.ITReturnDetailsId.ToString() + "/"
                + objITHeadDocumentsUploaderModel.ObjITReturnDocuments.ITHeadId.ToString() + "/";
            string path = Server.MapPath("~/ITReturnDetailsDocumentsUpload" + relativePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            objITHeadDocumentsUploaderModel.ITHeadFile.SaveAs(path + Path.GetFileName(objITHeadDocumentsUploaderModel.ITHeadFile.FileName));
            objITHeadDocumentsUploaderModel.ObjITReturnDocuments.FileName = Path.GetFileName(objITHeadDocumentsUploaderModel.ITHeadFile.FileName);
            objITHeadDocumentsUploaderModel.ObjITReturnDocuments.FilePath = relativePath + Path.GetFileName(objITHeadDocumentsUploaderModel.ITHeadFile.FileName);
            objITHeadDocumentsUploaderModel.ObjITReturnDocuments.AddedBy = 1;
            objITHeadDocumentsUploaderModel.ObjITReturnDocuments.ModifiedBy = 1;
            objITHeadDocumentsUploaderModel.ObjITReturnDocuments.Active = true;

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(objITHeadDocumentsUploaderModel.ObjITReturnDocuments);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync("api/TaxReturnAPI/InsertUpdateITReturnDocuments", content);
                ITReturnDocumentsResponse result = JsonConvert.DeserializeObject<ITReturnDocumentsResponse>(Res.Content.ReadAsStringAsync().Result);

                return RedirectToAction("GetITReturnDetails"
                    , new RouteValueDictionary(new
                    {
                        userId = objITHeadDocumentsUploaderModel.ObjITReturnDocuments.AddedBy,
                        companyId = objITHeadDocumentsUploaderModel.ObjITReturnDetails.CompanyID,
                        companyname = objITHeadDocumentsUploaderModel.ObjITReturnDetails.CompanyName,
                        FYAYID = objITHeadDocumentsUploaderModel.ObjITReturnDetails.FYAYID,
                        itsectionid = objITHeadDocumentsUploaderModel.ObjITReturnDetails.ITSectionID,
                        itreturnid = objITHeadDocumentsUploaderModel.ObjITReturnDocuments.ITReturnDetailsId
                    }));
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
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
                ITReturnComplexModel itrcomplexmodel = new ITReturnComplexModel();
                itrcomplexmodel.ITReturnDetailsObject = itReturn.ITReturnDetailsObject;
                itrcomplexmodel.ExtensionList = itReturn.ExtensionList;
                var json = JsonConvert.SerializeObject(itrcomplexmodel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync("api/TaxReturnAPI/InsertorUpdateITReturnDetails", content);
                ITReturnComplexAPIModelResponse result = new ITReturnComplexAPIModelResponse();
                if (Res.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<ITReturnComplexAPIModelResponse>(Res.Content.ReadAsStringAsync().Result);
                    return RedirectToAction("GetITReturnDetails"
                    , new RouteValueDictionary(new
                    {
                        userId = itrcomplexmodel.ITReturnDetailsObject.AddedBy,
                        companyId = itrcomplexmodel.ITReturnDetailsObject.CompanyID,
                        companyname = itrcomplexmodel.ITReturnDetailsObject.CompanyName,
                        FYAYID = itrcomplexmodel.ITReturnDetailsObject.FYAYID,
                        itsectionid = itrcomplexmodel.ITReturnDetailsObject.ITSectionID,
                        itreturnid = result.ITReturnDetailsObject.Id
                    }));
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = Res.Content.ReadAsStringAsync().Result;
                }
                return View("GetITReturnDetails", itReturn);
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
        public async Task<ActionResult> GetITReturnDocuments(int? companyId, int? fyayId, int? itHeadId)
        {
            ITReturnDocumentsModel model = new ITReturnDocumentsModel()
            {
                CompanyId = companyId,
                FYAYId = fyayId,
                ITHeadId = itHeadId
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetFYAYList");
                model.FYAYList = JsonConvert.DeserializeObject<List<FYAY>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/MasterAPI/GetCompanyList");
                model.CompanyList = JsonConvert.DeserializeObject<List<Company>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/MasterAPI/GetITHeadMaster");
                model.ITHeadList = JsonConvert.DeserializeObject<List<ITHeadMaster>>(Res.Content.ReadAsStringAsync().Result);
            }

            return View("ITReturnDocuments", model);
        }

        [HttpGet]
        public async Task<ActionResult> GetITReturnDocumentList(int? companyId, int? fyayId
            , int? itReturnDetailsId, int? itHeadId, int? itReturnDocumentId)
        {
            ITReturnDocumentListModel model = new ITReturnDocumentListModel()
            {
                CompanyId = companyId,
                FYAYId = fyayId,
                ITHeadId = itHeadId,
                ITReturnDetailsId = itReturnDetailsId,
                ITReturnDocumentId = itReturnDocumentId
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/TaxReturnAPI/GetITReturnDocumentsList?companyId="
                     + companyId + "&fyayId=" + fyayId
                     + "&itReturnDetailsId=" + itReturnDetailsId
                     + "&itHeadId=" + itHeadId
                     + "&itReturnDocumentId=" + itReturnDocumentId);
                model.ITReturnDocumentsList = (JsonConvert.DeserializeObject<ITReturnDocumentsResponse>(Res.Content.ReadAsStringAsync().Result)).ITReturnDocumentsList;
            }

            return PartialView("ITReturnDocumentList", model);
        }

        [HttpGet]
        public async Task<ActionResult> GetComplianceList(int? companyId, int? fyayId)
        {
            ComplianceListModel model = new ComplianceListModel()
            {
                CompanyId = companyId,
                FYAYId = fyayId
            };
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
                model.ComplianceListSource = new LitigationDDModel(
                       model.ComplianceList.Select(x =>
                        new SelectListItem() { Value = x.Id.ToString(), Text = x.Description }).ToList()
                       , "ObjComplianceDocuments.ComplianceId"
                       , "manageCompliance"
                       , "getCompliance"
                    );
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
            string relativePath = "/" + objComplianceListModel.CompanyName + "/"
                + objComplianceListModel.FinancialYear + "/";
            string path = Server.MapPath("~/ComplianceDocumentsUpload" + relativePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            objComplianceListModel.ReportFile.SaveAs(path + Path.GetFileName(objComplianceListModel.ReportFile.FileName));
            objComplianceListModel.ObjComplianceDocuments.FileName = Path.GetFileName(objComplianceListModel.ReportFile.FileName);
            objComplianceListModel.ObjComplianceDocuments.FilePath = relativePath + Path.GetFileName(objComplianceListModel.ReportFile.FileName);
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

                ComplianceListModel model = new ComplianceListModel()
                {
                    CompanyId = objComplianceListModel.ObjComplianceDocuments.CompanyId,
                    FYAYId = objComplianceListModel.ObjComplianceDocuments.FYAYId
                };
                Res = await client.GetAsync("api/MasterAPI/GetFYAYList");
                model.FYAYList = JsonConvert.DeserializeObject<List<FYAY>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/MasterAPI/GetCompanyList");
                model.CompanyList = JsonConvert.DeserializeObject<List<Company>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/MasterAPI/GetComplianceMaster?complianceId=");
                model.ComplianceList = JsonConvert.DeserializeObject<List<ComplianceMaster>>(Res.Content.ReadAsStringAsync().Result);
                model.ComplianceListSource = new LitigationDDModel(
                       model.ComplianceList.Select(x =>
                        new SelectListItem() { Value = x.Id.ToString(), Text = x.Description }).ToList()
                       , "ObjComplianceDocuments.ComplianceId"
                       , "manageCompliance"
                       , "getCompliance"
                    );
                return View("ComplianceList", model);
            }
        }

        [HttpGet]
        public ActionResult CheckComplianceDocument(string companyName, string financialYear, string fileName)
        {
            var result = false;
            string relativePath = "/" + companyName + "/" + financialYear + "/" + fileName;
            string path = Server.MapPath("~/ComplianceDocumentsUpload" + relativePath);
            if (System.IO.File.Exists(path))
            {
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteComplianceDocuments(ComplianceDocuments complianceDocuments)
        {
            complianceDocuments.DeletedBy = (Session[SESSION_LOGON_USER] as UserLogin).Id;
            string relativePath = "/" + complianceDocuments.FilePath;
            string path = Server.MapPath("~/ComplianceDocumentsUpload" + relativePath);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(complianceDocuments);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync("api/TaxReturnAPI/DeleteComplianceDocuments", content);
                ComplianceDocumentsResponse result = JsonConvert.DeserializeObject<ComplianceDocumentsResponse>(Res.Content.ReadAsStringAsync().Result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> InsertUpdateComplianceMaster(ComplianceMaster objComplianceMaster)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(objComplianceMaster);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync("api/MasterAPI/InsertUpdateComplianceMaster", content);
                ITSectionResponse result = new ITSectionResponse()
                {
                    IsSuccess = Res.IsSuccessStatusCode,
                    Message = Res.Content.ReadAsStringAsync().Result
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetComplianceMasterList()
        {
            var model = new List<ComplianceMaster>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetComplianceMaster?complianceId=");

                if (Res.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<List<ComplianceMaster>>(Res.Content.ReadAsStringAsync().Result);
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}