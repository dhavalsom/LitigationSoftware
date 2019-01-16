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
        [HttpGet]
        public ActionResult Index()
        {
            return View(new DashboardModel());
        }
        
        [HttpGet]
        public async Task<ActionResult> CreateCompany()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetCompanyCategoryList");
                var model = new CompanyModel();
                if (Res.IsSuccessStatusCode)
                {
                    model.CompanyCategoriesList = JsonConvert.DeserializeObject<List<CompanyCategory>>(Res.Content.ReadAsStringAsync().Result);
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
        public async Task<ActionResult> GetITReturnDetails(int? userId, int FYAYID
            , int? itsectionid, int? itreturnid, int? itsectioncategoryid)
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }

            ITReturnDetailsModel itrdetails = await CommonGetITReturnDetails(userId, selectedCompany.Id
                , selectedCompany.CompanyName, FYAYID, itsectionid, itreturnid, itsectioncategoryid);
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


                        Res = await client.GetAsync("api/MasterAPI/GetITSectionList?categoryId="+ (itrdetails.ITReturnDetailsObject.ITSectionCategoryID != 0 ? itrdetails.ITReturnDetailsObject.ITSectionCategoryID : itrdetails.ITSectionCategoryList.First().Id));
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
                    Res = await client.GetAsync("api/MasterAPI/GetITHeadMaster?IsTaxComputed=");
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
        public async Task<ActionResult> CompanyDashboard(int companyId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetCompanyList");
                if (Res.IsSuccessStatusCode)
                {
                    var selectedCompany = JsonConvert.DeserializeObject<List<Company>>
                        (Res.Content.ReadAsStringAsync().Result).Where(c => c.Id == companyId).FirstOrDefault();
                    if (selectedCompany != null)
                    {
                        HttpContext.Session["SelectedCompany"] = selectedCompany;
                    }
                }
            }
            return View(new CompanyDashboardModel() { CompanyObject = HttpContext.Session["SelectedCompany"]  as Company });
        }

        [HttpGet]
        public async Task<ActionResult> BusinessLossAnalysis()
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            var model = new BusinessLossAnalysisModel()
            {
                CompanyObject = HttpContext.Session["SelectedCompany"] as Company
            };
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("api/TaxReturnAPI/GetBusinessLossDetailsList?companyId="
                        + selectedCompany.Id + "&fyayId=&itSectionCategoryId=&businessLossDetailsId=");
                    var result = JsonConvert.DeserializeObject<BusinessLossDetailsResponse>(Res.Content.ReadAsStringAsync().Result);
                    if (result != null
                        && result.BusinessLossDetailsList != null
                        && result.BusinessLossDetailsList.Any())
                    {
                        model.BusinessLossDetailsList = result.BusinessLossDetailsList;
                        if (model.BusinessLossDetailsList.Any())
                        {
                            model.FYAYList = model.BusinessLossDetailsList
                                .Select(m => new
                                {
                                    Id = m.FYAYId,
                                    AssessmentYear = m.AssessmentYear,
                                    FinancialYear = m.FinancialYear
                                })
                                .Distinct()
                                .Select(m => new FYAY
                                {
                                    Id = m.Id,
                                    AssessmentYear = m.AssessmentYear,
                                    FinancialYear = m.FinancialYear
                                }).ToList();
                        }
                    }
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult FlowChart()
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            return View(new FlowChartModel()
            {
                CompanyObject = HttpContext.Session["SelectedCompany"] as Company
            });
        }

        [HttpGet]
        public ActionResult MatCreditStatus()
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            return View(new MatCreditStatusModel()
            {
                CompanyObject = HttpContext.Session["SelectedCompany"] as Company
            });
        }

        [HttpGet]
        public ActionResult SummaryReport()
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            return View(new SummaryReportModel()
            {
                CompanyObject = HttpContext.Session["SelectedCompany"] as Company
            });
        }

        [HttpGet]
        public ActionResult RDAnalysis()
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            return View(new RDAnalysisModel()
            {
                CompanyObject = HttpContext.Session["SelectedCompany"] as Company
            });
        }

        [HttpGet]
        public ActionResult LoadITReturnDetails(int fyayId, int itSectionCategoryId,
            int itSectionId)
        {
            Company selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            Session["CurrentITReturnDetails"] = new ITReturnDetails
            {
                FYAYID = fyayId,
                ITSectionID = itSectionId,
                ITSectionCategoryID = itSectionCategoryId
            };
            return RedirectToAction("ITReturnDetails");
        }

        [HttpGet]
        public async Task<ActionResult> ITReturnDetails()
        {
            Company selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            ITReturnDetailsHeaderModel itrdetails = new ITReturnDetailsHeaderModel
            {
                CompanyObject = selectedCompany
            };
            if (Session["CurrentITReturnDetails"] != null)
            {
                var currentITReturnDetails = Session["CurrentITReturnDetails"] as ITReturnDetails;
                itrdetails.FYAYId = currentITReturnDetails.FYAYID;
                itrdetails.ITSectionCategoryId = currentITReturnDetails.ITSectionCategoryID;
                itrdetails.ITSectionId = currentITReturnDetails.ITSectionID;
                Session["CurrentITReturnDetails"] = null;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetFYAYList");
                itrdetails.FYAYList = JsonConvert.DeserializeObject<List<FYAY>>
                    (Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/MasterAPI/GetITSectionCategoryList");
                itrdetails.ITSectionCategories = JsonConvert.DeserializeObject<List<ITSectionCategory>>
                    (Res.Content.ReadAsStringAsync().Result)
                    .OrderBy(its=>its.Id).ToList<ITSectionCategory>();
                Res = await client.GetAsync("api/MasterAPI/GetITSectionList?categoryId="                     
                    + (itrdetails.ITSectionCategoryId.HasValue 
                        ? itrdetails.ITSectionCategoryId.Value 
                        : itrdetails.ITSectionCategories.First().Id
                       )
                     );
                itrdetails.ITSectionList = JsonConvert.DeserializeObject<List<ITSection>>
                    (Res.Content.ReadAsStringAsync().Result);
                itrdetails.ITSectionListSource = new LitigationDDModel(
                   itrdetails.ITSectionList.Select(x =>
                    new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Description,
                        Selected = itrdetails.ITSectionId.HasValue && 
                        x.Id == itrdetails.ITSectionId.Value,
                    }).ToList()
                   , "ITReturnDetailsObject.ITSectionID"
                   , "manageITSection"
                   , "getITSections"
                );
            }
            return View(itrdetails);
        }

        [HttpGet]
        public async Task<ActionResult> ITReturnDetailsData(int fyayId
            , int? itSectionId, int? itReturnId, int? itSectionCategoryId)
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            ITReturnDetailsDataModel model = new ITReturnDetailsDataModel()
            {
                ITReturnDetailsObject = new ITReturnDetails
                {
                    CompanyID = selectedCompany.Id,
                    AddedBy = Session[SESSION_LOGON_USER] != null ? (Session[SESSION_LOGON_USER] as UserLogin).Id : 1,
                    Broughtforwardlosses = false,
                    FYAYID = fyayId,
                    ITSectionID = itSectionId.HasValue ? itSectionId.Value : 0,
                    ITSectionCategoryID = itSectionCategoryId.HasValue ? itSectionCategoryId.Value : 0,
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
                    HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetITSubHeadMaster?itHeadId=");
                    var itSubHeads = JsonConvert.DeserializeObject<List<ITSubHeadMaster>>(Res.Content.ReadAsStringAsync().Result);
                    Res = await client.GetAsync("api/MasterAPI/GetITHeadMaster?IsTaxComputed=");
                    var itHeads = JsonConvert.DeserializeObject<List<ITHeadMaster>>(Res.Content.ReadAsStringAsync().Result);

                    if (itSectionId.HasValue)
                    {
                        Res = await client.GetAsync("api/TaxReturnAPI/GetExistingITReturnDetailsList?companyId=" 
                            + selectedCompany.Id 
                            + "&fyayId=" + fyayId 
                            + "&itsectionid=" + itSectionId 
                            + "&itreturnid=" + itReturnId);
                        if (Res.IsSuccessStatusCode)
                        {
                            var response = JsonConvert.DeserializeObject<ITReturnDetailsListResponse>
                                (Res.Content.ReadAsStringAsync().Result);
                            if (response.ITReturnDetailsListObject.Count > 0)
                            {
                                model.ITReturnDetailsObject = response.ITReturnDetailsListObject.First();
                                Session["CurrentBusinessLossDetails"] = model.ITReturnDetailsObject;
                                Res = await client.GetAsync("api/TaxReturnAPI/GetITReturnDocumentsList?companyId=&fyayId=&itReturnDetailsId="
                                    + model.ITReturnDetailsObject.Id + "&itHeadId=&itReturnDocumentId=");
                                if (Res.IsSuccessStatusCode)
                                {
                                    var objITReturnDocumentsResponse = JsonConvert.DeserializeObject<ITReturnDocumentsResponse>
                                        (Res.Content.ReadAsStringAsync().Result);
                                    model.ITReturnDocumentList = new Dictionary<string, List<ITReturnDocumentsDisplay>>();
                                    foreach (var item in objITReturnDocumentsResponse.ITReturnDocumentsList)
                                    {
                                        if (model.ITReturnDocumentList.ContainsKey(item.PropertyName))
                                        {
                                            model.ITReturnDocumentList[item.PropertyName].Add(item);
                                        }
                                        else
                                        {
                                            model.ITReturnDocumentList.Add(item.PropertyName,
                                                new List<ITReturnDocumentsDisplay> { item });
                                        }
                                    }
                                    model.ITHeadDocumentsUploaderModels = new Dictionary<string, ITHeadDocumentsUploaderModel>();
                                    foreach (var itHead in itHeads)
                                    {
                                        if (itHead.CanAddDocuments)
                                        {
                                            if (model.ITReturnDocumentList.ContainsKey(itHead.PropertyName))
                                            {
                                                model.ITHeadDocumentsUploaderModels.Add(itHead.PropertyName
                                                    , new ITHeadDocumentsUploaderModel(model.ITReturnDocumentList[itHead.PropertyName]
                                                    , itHead, model.ITReturnDetailsObject));
                                            }
                                            else
                                            {
                                                model.ITHeadDocumentsUploaderModels.Add(itHead.PropertyName
                                                    , new ITHeadDocumentsUploaderModel(new List<ITReturnDocumentsDisplay>()
                                                    , itHead, model.ITReturnDetailsObject));
                                            }
                                        }
                                    }
                                }
                                
                                Res = await client.GetAsync("api/TaxReturnAPI/GetSPIncomeDetailsList?itReturnDetailsId="
                                    + model.ITReturnDetailsObject.Id + "&itHeadId=");
                                if (Res.IsSuccessStatusCode)
                                {
                                    var objSPIncomeDetailsResponse = JsonConvert.DeserializeObject<SPIncomeDetailsResponse>
                                        (Res.Content.ReadAsStringAsync().Result);
                                    model.SPIncomeDetailsList = new Dictionary<string, List<SPIncomeDetails>>();
                                    foreach (var item in objSPIncomeDetailsResponse.SPIncomeDetailsList)
                                    {
                                        if (model.SPIncomeDetailsList.ContainsKey(item.PropertyName))
                                        {
                                            model.SPIncomeDetailsList[item.PropertyName].Add(item);
                                        }
                                        else
                                        {
                                            model.SPIncomeDetailsList.Add(item.PropertyName,
                                                new List<SPIncomeDetails> { item });
                                        }
                                    }
                                    model.ITHeadSpecialIncomeModels = new Dictionary<string, ITHeadSpecialIncomeModel>();
                                    foreach (var itHead in itHeads)
                                    {
                                        if (itHead.IsSpecialIncomeEnabled)
                                        {
                                            if (model.SPIncomeDetailsList.ContainsKey(itHead.PropertyName))
                                            {
                                                model.ITHeadSpecialIncomeModels.Add(itHead.PropertyName
                                                    , new ITHeadSpecialIncomeModel(model.SPIncomeDetailsList[itHead.PropertyName]
                                                    , itHead, model.ITReturnDetailsObject));
                                            }
                                            else
                                            {
                                                model.ITHeadSpecialIncomeModels.Add(itHead.PropertyName
                                                    , new ITHeadSpecialIncomeModel(new List<SPIncomeDetails>() { new SPIncomeDetails()}
                                                    , itHead, model.ITReturnDetailsObject));
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Res = await client.GetAsync("api/MasterAPI/GetITSectionCategoryList");
                                model.ITReturnDetailsObject.ITSectionCategoryDesc = JsonConvert.DeserializeObject<List<ITSectionCategory>>
                                    (Res.Content.ReadAsStringAsync().Result)
                                    .Where(l=>l.Id == model.ITReturnDetailsObject.ITSectionCategoryID)
                                    .First().Description;
                                Res = await client.GetAsync("api/MasterAPI/GetITSectionList?categoryId="
                                    + model.ITReturnDetailsObject.ITSectionCategoryID);
                                model.ITReturnDetailsObject.ITSectionDescription = 
                                    JsonConvert.DeserializeObject<List<ITSection>>
                                    (Res.Content.ReadAsStringAsync().Result)
                                    .Where(l => l.Id == model.ITReturnDetailsObject.ITSectionID)
                                    .First().Description; ;
                            }
                        }
                    }

                    model.PopulateITHeadMasters(itHeads, itSubHeads
                        , model.ITReturnDetailsObject != null 
                            ? model.ITReturnDetailsObject.Id 
                            : (int?)null);

                    if (!model.ITReturnDetailsObject.IsReturn)
                    {
                        List<ITReturnDetailsExtension> itSubHeadValues = new List<ITReturnDetailsExtension>();

                        Res = await client.GetAsync("api/TaxReturnAPI/GetExistingITReturnDetailsExtension?itreturnid=" + model.ITReturnDetailsObject.Id);
                        if (Res.IsSuccessStatusCode)
                        {
                            if (JsonConvert.DeserializeObject<List<ITReturnDetailsExtension>>(Res.Content.ReadAsStringAsync().Result).Count > 0)
                            {
                                itSubHeadValues = JsonConvert.DeserializeObject<List<ITReturnDetailsExtension>>(Res.Content.ReadAsStringAsync().Result);
                            }
                        }

                        if (itSubHeadValues.Count > 0)
                        {
                            foreach (var item in model.ExtensionList)
                            {
                                foreach (var subvalue in itSubHeadValues)
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
            return PartialView(model);
        }

        [HttpGet]
        public async Task<ActionResult> ExistingITReturnDetails()
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            ITReturnDetailsModel itrdetails = new ITReturnDetailsModel
            {
                ITReturnDetailsObject = new ITReturnDetails
                {
                    CompanyID = selectedCompany.Id,
                    CompanyName = selectedCompany.CompanyName,
                    Broughtforwardlosses = false
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
        public async Task<ActionResult> LitigationAndSimulation()
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            LitigationAndSimulationModel lAndSModel = new LitigationAndSimulationModel()
            {
                CompanyObject = HttpContext.Session["SelectedCompany"] as Company
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/TaxReturnAPI/GetLitigationAndSimulation?companyId="+ selectedCompany.Id);
                if (Res.IsSuccessStatusCode)
                {
                    lAndSModel.ITReturnDetailsListObject = JsonConvert.DeserializeObject<ITReturnDetailsListResponse>
                        (Res.Content.ReadAsStringAsync().Result).ITReturnDetailsListObject;
                    foreach (var itReturn in lAndSModel.ITReturnDetailsListObject)
                    {
                        itReturn.Extensions = new List<ITReturnDetailsExtension>();
                        Res = await client.GetAsync("api/TaxReturnAPI/GetExistingITReturnDetailsExtension?itreturnid=" + itReturn.Id);
                        if (Res.IsSuccessStatusCode)
                        {
                            itReturn.Extensions = JsonConvert.DeserializeObject<List<ITReturnDetailsExtension>>(Res.Content.ReadAsStringAsync().Result);
                            lAndSModel.ITReturnDetailExtensions.AddRange(itReturn.Extensions);
                        }
                    }
                    Res = await client.GetAsync("api/TaxReturnAPI/GetLAndSCommentList?itSubHeadId=&companyId=" + selectedCompany.Id);
                    if (Res.IsSuccessStatusCode)
                    {
                        lAndSModel.LAndSCommentList = (JsonConvert.DeserializeObject<LAndSCommentsResponse>(Res.Content.ReadAsStringAsync().Result)).LAndSCommentsList;
                    }
                    Res = await client.GetAsync("api/MasterAPI/GetITSubHeadMaster?itHeadId=");
                    var itSubHeads = JsonConvert.DeserializeObject<List<ITSubHeadMaster>>(Res.Content.ReadAsStringAsync().Result);
                    Res = await client.GetAsync("api/MasterAPI/GetITHeadMaster?IsTaxComputed=");
                    var itHeads = JsonConvert.DeserializeObject<List<ITHeadMaster>>(Res.Content.ReadAsStringAsync().Result);
                    lAndSModel.PopulateITHeadMasters(itHeads, itSubHeads);
                }
            }
            return View(lAndSModel);
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
                    foreach (var itReturn in itrdetail.ITReturnDetailsListObject)
                    {
                        itReturn.Extensions = new List<ITReturnDetailsExtension>();
                        Res = await client.GetAsync("api/TaxReturnAPI/GetExistingITReturnDetailsExtension?itreturnid=" + itReturn.Id);
                        if (Res.IsSuccessStatusCode)
                        {
                            itReturn.Extensions = JsonConvert.DeserializeObject<List<ITReturnDetailsExtension>>(Res.Content.ReadAsStringAsync().Result);
                        }
                    }
                    Res = await client.GetAsync("api/MasterAPI/GetITSubHeadMaster?itHeadId=");
                    var itSubHeads = JsonConvert.DeserializeObject<List<ITSubHeadMaster>>(Res.Content.ReadAsStringAsync().Result);
                    Res = await client.GetAsync("api/MasterAPI/GetITHeadMaster?IsTaxComputed=false");
                    var itHeads = JsonConvert.DeserializeObject<List<ITHeadMaster>>(Res.Content.ReadAsStringAsync().Result);
                    itrdetail.PopulateITHeadMasters(itHeads, itSubHeads);
                }
            }

            return PartialView("ExistingSectionWiseDetails", itrdetail);
        }

        [HttpGet]
        public async Task<ActionResult> TaxCalculationSheet(int companyId, int fyayId)
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }

            ITReturnDetailsListModel itrdetail = new ITReturnDetailsListModel()
            {
                CompanyId = companyId,
                FYAYId = fyayId,
            };
            decimal? foreignDividend = 0;
            StandardDataModel standardDataModelObject = new StandardDataModel();
            StandardData standardrefData = new StandardData();
            SurchargeDataModel surchargeDataModelObject = new SurchargeDataModel();
            SurchargeData surchargerefData = new SurchargeData();
            decimal? STCGSpecialIncome = 0;
            decimal? Surchargerate = 0;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/TaxReturnAPI/GetExistingITReturnDetailsList?companyId=" + companyId + "&fyayId=" + fyayId + "&itsectionid=0&itreturnid=0");
                if (Res.IsSuccessStatusCode)
                {
                    itrdetail.ITReturnDetailsListObject = JsonConvert.DeserializeObject<ITReturnDetailsListResponse>(Res.Content.ReadAsStringAsync().Result).ITReturnDetailsListObject;
                    foreach (var itReturn in itrdetail.ITReturnDetailsListObject)
                    {
                        itReturn.Extensions = new List<ITReturnDetailsExtension>();
                        Res = await client.GetAsync("api/TaxReturnAPI/GetExistingITReturnDetailsExtension?itreturnid=" + itReturn.Id);
                        if (Res.IsSuccessStatusCode)
                        {
                            itReturn.Extensions = JsonConvert.DeserializeObject<List<ITReturnDetailsExtension>>(Res.Content.ReadAsStringAsync().Result);
                        }

                        
                    }
                    Res = await client.GetAsync("api/MasterAPI/GetITSubHeadMaster?itHeadId=");
                    var itSubHeads = JsonConvert.DeserializeObject<List<ITSubHeadMaster>>(Res.Content.ReadAsStringAsync().Result);
                    Res = await client.GetAsync("api/MasterAPI/GetITHeadMaster?IsTaxComputed=");
                    var itHeads = JsonConvert.DeserializeObject<List<ITHeadMaster>>(Res.Content.ReadAsStringAsync().Result);
                    itrdetail.PopulateITHeadMasters(itHeads, itSubHeads);

                    Res = await client.GetAsync("api/MasterAPI/GetStandardData?FYAYID="+fyayId+"&standarddataId=");

                    if (Res.IsSuccessStatusCode)
                    {
                        standardDataModelObject.StandardDataObjectList = JsonConvert.DeserializeObject<List<StandardData>>(Res.Content.ReadAsStringAsync().Result);
                        standardrefData = standardDataModelObject.StandardDataObjectList.FirstOrDefault();
                    }

                    Res = await client.GetAsync("api/MasterAPI/GetSurchargeData?FYAYID=" + fyayId + "&surchargedataId=&entitycategorytypeid="+ selectedCompany.CategoryID);

                    if (Res.IsSuccessStatusCode)
                    {
                        surchargeDataModelObject.SurchargeDataObjectList = JsonConvert.DeserializeObject<List<SurchargeData>>(Res.Content.ReadAsStringAsync().Result);
                        //surchargerefData = surchargeDataModelObject.SurchargeDataObjectList.FirstOrDefault();
                    }

                    foreach (var itReturn in itrdetail.ITReturnDetailsListObject)
                    {
                        //Statement of Taxes

                        Res = await client.GetAsync("api/TaxReturnAPI/GetSPIncomeDetailsList?itReturnDetailsId="
                                    + itReturn.Id + "&itHeadId="+ itHeads.Where(x => x.PropertyName == "IncomeFromOtherSources").FirstOrDefault().Id);
                        if (Res.IsSuccessStatusCode)
                        {
                            var objSPIncomeDetailsResponse = JsonConvert.DeserializeObject<SPIncomeDetailsResponse>
                                        (Res.Content.ReadAsStringAsync().Result);

                            foreach (var item in objSPIncomeDetailsResponse.SPIncomeDetailsList)
                            {
                                foreignDividend = item.SPIncomeDescription.ToUpper().Trim().Replace(" ", "") == "FOREIGNDIVIDEND" ? (item.SPIncomeValue.HasValue?item.SPIncomeValue:0) : 0;
                            }
                        }

                        Res = await client.GetAsync("api/TaxReturnAPI/GetSPIncomeDetailsList?itReturnDetailsId="
                                    + itReturn.Id + "&itHeadId=" + itHeads.Where(x => x.PropertyName == "IncomefromCapGainsSTCG").FirstOrDefault().Id);
                        if (Res.IsSuccessStatusCode)
                        {
                            var objSPIncomeDetailsResponse = JsonConvert.DeserializeObject<SPIncomeDetailsResponse>
                                        (Res.Content.ReadAsStringAsync().Result);

                            foreach (var item in objSPIncomeDetailsResponse.SPIncomeDetailsList)
                            {
                                STCGSpecialIncome = STCGSpecialIncome + ((item.SPIncomeValue.HasValue ? item.SPIncomeValue : 0) * (item.TaxRate/100));
                            }
                        }

                        Res = await client.GetAsync("api/TaxReturnAPI/GetSPIncomeDetailsList?itReturnDetailsId="
                                    + itReturn.Id + "&itHeadId=" + itHeads.Where(x => x.PropertyName == "SelfAssessmentTax").FirstOrDefault().Id);
                        if (Res.IsSuccessStatusCode)
                        {
                            var objSPIncomeDetailsResponse = JsonConvert.DeserializeObject<SPIncomeDetailsResponse>
                                        (Res.Content.ReadAsStringAsync().Result);

                            if (objSPIncomeDetailsResponse.SPIncomeDetailsList.Count>0)
                                itrdetail.SelfAssessmentList = objSPIncomeDetailsResponse.SPIncomeDetailsList;
                        }

                        Res = await client.GetAsync("api/TaxReturnAPI/GetSPIncomeDetailsList?itReturnDetailsId="
                                    + itReturn.Id + "&itHeadId=" + itHeads.Where(x => x.PropertyName == "RegularAssessment").FirstOrDefault().Id);
                        if (Res.IsSuccessStatusCode)
                        {
                            var objSPIncomeDetailsResponse = JsonConvert.DeserializeObject<SPIncomeDetailsResponse>
                                        (Res.Content.ReadAsStringAsync().Result);

                            if (objSPIncomeDetailsResponse.SPIncomeDetailsList.Count > 0)
                                itrdetail.RegularAssessmentList = objSPIncomeDetailsResponse.SPIncomeDetailsList;
                        }

                        itReturn.TotalIncomeasperRegProvisions = itReturn.GetTotalComputedValue(itHeads.Where(x => x.PropertyName == "IncomefromSalary").FirstOrDefault())
                            + itReturn.GetTotalComputedValue(itHeads.Where(x => x.PropertyName == "HousePropIncome").FirstOrDefault())
                            + itReturn.GetTotalComputedValue(itHeads.Where(x => x.PropertyName == "IncomefromCapGainsLTCG").FirstOrDefault())
                            + itReturn.GetTotalComputedValue(itHeads.Where(x => x.PropertyName == "IncomefromCapGainsSTCG").FirstOrDefault())
                            + itReturn.GetTotalComputedValue(itHeads.Where(x => x.PropertyName == "IncomefromBusinessProf").FirstOrDefault())
                            + itReturn.GetTotalComputedValue(itHeads.Where(x => x.PropertyName == "IncomefromSpeculativeBusiness").FirstOrDefault())
                            + itReturn.GetTotalComputedValue(itHeads.Where(x => x.PropertyName == "IncomeFromOtherSources").FirstOrDefault())
                            - itReturn.GetTotalComputedValue(itHeads.Where(x => x.PropertyName == "DeductChapterVIA").FirstOrDefault());

                        itReturn.TaxOnTotalIncome = ((itReturn.TotalIncomeasperRegProvisions- foreignDividend)*(standardrefData.BasicTaxRate/100))+ STCGSpecialIncome;
                        //itReturn.SurchargeTax = ()

                    foreach(var item in surchargeDataModelObject.SurchargeDataObjectList)
                        {
                            if (itReturn.TaxOnTotalIncome >= (item.surchargefromthreshold.HasValue?item.surchargefromthreshold:0) && itReturn.TaxOnTotalIncome <= (item.surchargetothreshold.HasValue?item.surchargetothreshold:0))
                            {
                                Surchargerate = item.surchargerate;
                                break;
                            }
                        }

                        if (Surchargerate > 0)
                            itReturn.SurchargeTax = itReturn.TaxOnTotalIncome * (Surchargerate / 100);
                        else
                            itReturn.SurchargeTax = 0;

                        if (itReturn.SurchargeTax > 0)
                            itReturn.EducationCess = (itReturn.TaxOnTotalIncome + itReturn.SurchargeTax) * (standardrefData.EducationCess / 100);
                        else
                            itReturn.EducationCess = 0;

                       
                        if (itReturn.ProfitUS115JB < 0)
                            itReturn.MATTaxOnTotalIncome = 0;
                        else
                            itReturn.MATTaxOnTotalIncome = (itReturn.ProfitUS115JB.HasValue?itReturn.ProfitUS115JB:0) * (standardrefData.MATRate / 100);

                        Surchargerate = 0;//reset surchargerate to calculate surcharge for MAT

                        foreach (var item in surchargeDataModelObject.SurchargeDataObjectList)
                        {
                            if (itReturn.MATTaxOnTotalIncome >= (item.surchargefromthreshold.HasValue ? item.surchargefromthreshold : 0) && itReturn.MATTaxOnTotalIncome <= (item.surchargetothreshold.HasValue ? item.surchargetothreshold : 0))
                            {
                                Surchargerate = item.surchargerate;
                                break;
                            }
                        }

                        if (Surchargerate > 0)
                            itReturn.MATSurchargeTax = itReturn.MATTaxOnTotalIncome * (Surchargerate / 100);
                        else
                            itReturn.MATSurchargeTax = 0;

                        if (itReturn.SurchargeTax > 0)
                            itReturn.MATEducationCess = (itReturn.MATTaxOnTotalIncome + itReturn.MATSurchargeTax) * (standardrefData.EducationCess / 100);
                        else
                            itReturn.MATEducationCess = 0;

                        if((itReturn.TaxOnTotalIncome + itReturn.SurchargeTax + itReturn.EducationCess) > (itReturn.MATTaxOnTotalIncome + itReturn.MATSurchargeTax + itReturn.MATEducationCess))
                        {
                            itReturn.Taxliability = (itReturn.TaxOnTotalIncome + itReturn.SurchargeTax + itReturn.EducationCess);
                        }
                        else
                        {
                            itReturn.Taxliability = (itReturn.MATTaxOnTotalIncome + itReturn.MATSurchargeTax + itReturn.MATEducationCess);
                        }

                        itReturn.TotalTaxPaid = (itReturn.AdvanceTax1installment.HasValue ? itReturn.AdvanceTax1installment : 0) +
                                                (itReturn.AdvanceTax2installment.HasValue ? itReturn.AdvanceTax2installment : 0) +
                                                (itReturn.AdvanceTax3installment.HasValue ? itReturn.AdvanceTax3installment : 0) +
                                                (itReturn.AdvanceTax4installment.HasValue ? itReturn.AdvanceTax4installment : 0) +
                                                (itReturn.TCSPaidbyCompany.HasValue ? itReturn.TCSPaidbyCompany : 0) +
                                                (itReturn.TaxCollectedAtSource.HasValue ? itReturn.TaxCollectedAtSource : 0) +
                                                (itReturn.MATCredit.HasValue ? itReturn.MATCredit : 0);

                        itReturn.TotalInterest = (itReturn.InterestUS234A.HasValue ? itReturn.InterestUS234A : 0) +
                                                 (itReturn.InterestUS234B.HasValue ? itReturn.InterestUS234B : 0) +
                                                 (itReturn.InterestUS234C.HasValue ? itReturn.InterestUS234C : 0) +
                                                 (itReturn.InterestUS234D.HasValue ? itReturn.InterestUS234D : 0) +
                                                 (itReturn.InterestUS244A.HasValue ? itReturn.InterestUS244A : 0) +
                                                 (itReturn.InterestUS220.HasValue ? itReturn.InterestUS220 : 0);

                        itReturn.TaxPayable = itReturn.Taxliability - itReturn.TotalTaxPaid + itReturn.TotalInterest;
                        itReturn.NetDemand = 0;
                        foreach (var selfassmtval in itrdetail.SelfAssessmentList)
                        {
                            itReturn.NetDemand = itReturn.NetDemand + (selfassmtval.SPIncomeValue.HasValue ? selfassmtval.SPIncomeValue : 0);
                        }

                        foreach (var regassmtval in itrdetail.RegularAssessmentList)
                        {
                            itReturn.NetDemand = itReturn.NetDemand + (regassmtval.SPIncomeValue.HasValue ? regassmtval.SPIncomeValue : 0);
                        }

                        itReturn.NetDemand = itReturn.TaxPayable - (itReturn.NetDemand + (itReturn.RefundAdjusted.HasValue ? itReturn.RefundAdjusted:0) + (itReturn.RefundReceived.HasValue ? itReturn.RefundReceived:0));
                    }


                }
                
            }

            return PartialView("TaxCalculationSheet", itrdetail);
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

                Session["CurrentITReturnDetails"] = objITHeadDocumentsUploaderModel.ObjITReturnDetails;
                return RedirectToAction("ITReturnDetails");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpsertITReturnDetails(ITReturnDetailsDataModel itReturn
            , FormCollection form)
        {
            foreach (var control in form.Keys)
            {
                if (control.ToString().StartsWith("txtITSubHead_"))
                {
                    decimal value;
                    if (decimal.TryParse(form[control.ToString()].ToString(), out value))
                    {
                        if (value != 0)
                        {
                            itReturn.ExtensionList.Add(new ITReturnDetailsExtension
                            {
                                Id = itReturn.ITReturnDetailsObject.Id,
                                ITSubHeadValue = value,
                                ITSubHeadId = int.Parse(control.ToString().Replace("txtITSubHead_", ""))
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
                    if (itReturn.ITReturnDetailsObject.Broughtforwardlosses.HasValue
                        && itReturn.ITReturnDetailsObject.Broughtforwardlosses.Value)
                    {
                        return RedirectToAction("BusinessLossDetails");
                    }
                    else
                    {
                        Session["CurrentITReturnDetails"] = itReturn.ITReturnDetailsObject;
                        return RedirectToAction("ITReturnDetails");
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = Res.Content.ReadAsStringAsync().Result;
                }
                return RedirectToAction("ITReturnDetails");
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
        public async Task<ActionResult> GetITReturnDocuments(int? fyayId, int? itHeadId)
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }

            ITReturnDocumentsModel model = new ITReturnDocumentsModel()
            {
                CompanyId = selectedCompany.Id,
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
                Res = await client.GetAsync("api/MasterAPI/GetITHeadMaster?IsTaxComputed=");
                model.ITHeadList = JsonConvert.DeserializeObject<List<ITHeadMaster>>(Res.Content.ReadAsStringAsync().Result);
            }

            return View("ITReturnDocuments", model);
        }

        [HttpGet]
        public async Task<ActionResult> GetITReturnDocumentList( int? fyayId
            , int? itReturnDetailsId, int? itHeadId, int? itReturnDocumentId)
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            ITReturnDocumentListModel model = new ITReturnDocumentListModel()
            {
                CompanyId = selectedCompany.Id,
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
                     + selectedCompany.Id + "&fyayId=" + fyayId
                     + "&itReturnDetailsId=" + itReturnDetailsId
                     + "&itHeadId=" + itHeadId
                     + "&itReturnDocumentId=" + itReturnDocumentId);
                model.ITReturnDocumentsList = (JsonConvert.DeserializeObject<ITReturnDocumentsResponse>(Res.Content.ReadAsStringAsync().Result)).ITReturnDocumentsList;
            }

            return PartialView("ITReturnDocumentList", model);
        }

        [HttpGet]
        public async Task<ActionResult> GetComplianceList(int? fyayId)
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }

            ComplianceListModel model = new ComplianceListModel()
            {
                CompanyId = selectedCompany.Id,
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

        [HttpGet]
        public async Task<ActionResult> GetStandardData()
        {
            StandardDataModel standardDataModelObject = new StandardDataModel();
            var model = new List<StandardData>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetFYAYList");
                if (Res.IsSuccessStatusCode)
                {
                    standardDataModelObject.FYAYList = JsonConvert.DeserializeObject<List<FYAY>>(Res.Content.ReadAsStringAsync().Result);
                }

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Res = await client.GetAsync("api/MasterAPI/GetStandardData?surchargedataId=");

                if (Res.IsSuccessStatusCode)
                {
                    standardDataModelObject.StandardDataObjectList = JsonConvert.DeserializeObject<List<StandardData>>(Res.Content.ReadAsStringAsync().Result);
                }
            }
            //return Json(model, JsonRequestBehavior.AllowGet);
            return View(standardDataModelObject);
        }

        [HttpPost]
        public async Task<ActionResult> InsertUpdateLAndSComments(List<LAndSComments> landsComments)
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                foreach (var landsComment in landsComments)
                {
                    landsComment.CompanyId = selectedCompany.Id;
                    landsComment.Active = true;
                    var json = JsonConvert.SerializeObject(landsComment);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PostAsync
                        ("api/TaxReturnAPI/InsertUpdateLAndSComments", content);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> InsertUpdateITHeadSpecialIncome
           (ITHeadSpecialIncomeModel objITHeadSpecialIncomeModel, FormCollection form)
        {
            foreach (var control in form.Keys)
            {
                if (control.ToString().StartsWith("SPIncomeDescription_"))
                {
                    var id = int.Parse(control.ToString().Split('_')[1]);
                    decimal value;
                    DateTime date;
                    var spIncome = new SPIncomeDetails
                    {
                        Id = id > 0 ? id : 0,
                        ITHeadId = objITHeadSpecialIncomeModel.ObjSPIncomeDetails.ITHeadId,
                        ITReturnDetailsId = objITHeadSpecialIncomeModel.ObjSPIncomeDetails.ITReturnDetailsId,
                        SPIncomeValue = decimal.TryParse(form["SPIncomeValue_" + id.ToString()].ToString(), out value) ? value : (decimal?)null,
                        TaxRate = decimal.TryParse(form["TaxRate_" + id.ToString()].ToString(), out value) ? value : (decimal?)null,
                        SPIncomeDescription = form["SPIncomeDescription_" + id.ToString()].ToString(),
                        SPIncomeDate = DateTime.TryParse(form["SPIncomeDate_" + id.ToString()].ToString(), out date) ? date : (DateTime?)null                        
                    };
                    using (var client = new HttpClient())
                    {
                        var json = JsonConvert.SerializeObject(spIncome);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage Res = await client.PostAsync("api/TaxReturnAPI/InsertUpdateSPIncomeDetails", content);
                        ITReturnDocumentsResponse result = JsonConvert.DeserializeObject<ITReturnDocumentsResponse>(Res.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            Session["CurrentITReturnDetails"] = objITHeadSpecialIncomeModel.ObjITReturnDetails;
            return RedirectToAction("ITReturnDetails");
        }

        [HttpGet]
        public async Task<ActionResult> BusinessLossDetails()
        {
            Company selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            ITReturnDetails itReturnDetails = Session["CurrentBusinessLossDetails"] as ITReturnDetails;
            if (selectedCompany == null
                || itReturnDetails == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            BusinessLossDetailsHeaderModel businessLossDetails = new BusinessLossDetailsHeaderModel
            {
                CompanyObject = selectedCompany,
                FYAYObject = new FYAY { Id = itReturnDetails.FYAYID },
                ITSectionCategoryObject = new ITSectionCategory { Id = itReturnDetails.ITSectionCategoryID }
            };
            //Session["CurrentBusinessLossDetails"] = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetFYAYList");
                businessLossDetails.FYAYObject = JsonConvert.DeserializeObject<List<FYAY>>
                    (Res.Content.ReadAsStringAsync().Result)
                    .Where(f => f.Id == businessLossDetails.FYAYObject.Id).FirstOrDefault();
                Res = await client.GetAsync("api/MasterAPI/GetITSectionCategoryList");
                businessLossDetails.ITSectionCategoryObject = JsonConvert.DeserializeObject<List<ITSectionCategory>>
                    (Res.Content.ReadAsStringAsync().Result)
                    .Where(bl => bl.Id == businessLossDetails.ITSectionCategoryObject.Id)
                    .FirstOrDefault();
            }
            return View(businessLossDetails);
        }

        [HttpGet]
        public async Task<ActionResult> BusinessLossDetailsData(int? fyayId
            , int? itSectionCategoryId, int? businessLossDetailsId)
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            BusinessLossDetailsDataModel model = new BusinessLossDetailsDataModel()
            {
                BusinessLossDetailsObject = new BusinessLossDetails
                {
                    CompanyId = selectedCompany.Id,
                    AddedBy = Session[SESSION_LOGON_USER] != null ? (Session[SESSION_LOGON_USER] as UserLogin).Id : 1,
                    Id = businessLossDetailsId.HasValue ? businessLossDetailsId.Value : 0,
                    FYAYId = fyayId.HasValue ? fyayId.Value : 0 ,
                    ITSectionCategoryId = itSectionCategoryId.HasValue ? itSectionCategoryId.Value : 0,
                }
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("api/TaxReturnAPI/GetBusinessLossDetailsList?companyId="
                        + selectedCompany.Id + "&fyayId=" + (fyayId.HasValue ? fyayId.Value.ToString() : "")
                        + "&itSectionCategoryId=" + (itSectionCategoryId.HasValue ? itSectionCategoryId.Value.ToString() : "")
                        + "&businessLossDetailsId=" + (businessLossDetailsId.HasValue ? businessLossDetailsId.Value.ToString() : ""));
                    var result = JsonConvert.DeserializeObject<BusinessLossDetailsResponse>(Res.Content.ReadAsStringAsync().Result);
                    if(result != null )
                    {
                        if (result.BusinessLossDetailsList != null 
                            && result.BusinessLossDetailsList.Where(bl => bl.IsCurrentYear).Any())
                        {
                            model.BusinessLossDetailsObject = result.BusinessLossDetailsList.Where(bl => bl.IsCurrentYear).First();
                            model.BusinessLossDetailsObject.ModifiedBy = Session[SESSION_LOGON_USER] != null ? (Session[SESSION_LOGON_USER] as UserLogin).Id : 1;
                        }
                        if (result.BusinessLossDetailsList != null
                            && model.BusinessLossDetailsObject.Id == 0
                            && result.BusinessLossDetailsList.Where(bl => !bl.IsCurrentYear).Any())
                        {
                            var prevYearBLObject = result.BusinessLossDetailsList.Where(bl => !bl.IsCurrentYear).First();
                            model.BusinessLossDetailsObject.IncomeCapGainsLTCG_BF = prevYearBLObject.IncomeCapGainsLTCG_Total;
                            model.BusinessLossDetailsObject.IncomeCapGainsSTCG_BF = prevYearBLObject.IncomeCapGainsSTCG_Total;
                            model.BusinessLossDetailsObject.IncomeBusinessProf_BF = prevYearBLObject.IncomeBusinessProf_Total;
                            model.BusinessLossDetailsObject.IncomeSpeculativeBusiness_BF = prevYearBLObject.IncomeSpeculativeBusiness_Total;
                            model.BusinessLossDetailsObject.UnabsorbedDepreciation_BF = prevYearBLObject.UnabsorbedDepreciation_Total;
                            model.BusinessLossDetailsObject.HousePropIncome_BF = prevYearBLObject.HousePropIncome_Total;
                            model.BusinessLossDetailsObject.IncomeOtherSources_BF = prevYearBLObject.IncomeOtherSources_Total;
                        }
                        if (result.ITReturnDetailsObject != null)
                        {
                            if (result.ITReturnDetailsObject.IncomefromCapGainsLTCG < 0)
                            {
                                model.BusinessLossDetailsObject.IncomeCapGainsLTCG_CY = result.ITReturnDetailsObject.IncomefromCapGainsLTCG * -1;
                            }
                            if (result.ITReturnDetailsObject.IncomefromCapGainsSTCG < 0)
                            {
                                model.BusinessLossDetailsObject.IncomeCapGainsSTCG_CY = result.ITReturnDetailsObject.IncomefromCapGainsSTCG * -1;
                            }
                            if (result.ITReturnDetailsObject.IncomefromBusinessProf < 0)
                            {
                                model.BusinessLossDetailsObject.IncomeBusinessProf_CY = result.ITReturnDetailsObject.IncomefromBusinessProf * -1;
                            }
                            if (result.ITReturnDetailsObject.IncomefromSpeculativeBusiness < 0)
                            {
                                model.BusinessLossDetailsObject.IncomeSpeculativeBusiness_CY = result.ITReturnDetailsObject.IncomefromSpeculativeBusiness * -1;
                            }
                            if (result.ITReturnDetailsObject.HousePropIncome < 0)
                            {
                                model.BusinessLossDetailsObject.HousePropIncome_CY = result.ITReturnDetailsObject.HousePropIncome * -1;
                            }
                            if (result.ITReturnDetailsObject.IncomeFromOtherSources < 0)
                            {
                                model.BusinessLossDetailsObject.IncomeOtherSources_CY = result.ITReturnDetailsObject.IncomeFromOtherSources * -1;
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> InsertUpdateBusinessLossDetails
            (BusinessLossDetailsDataModel businessLossDetails)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(businessLossDetails.BusinessLossDetailsObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync("api/TaxReturnAPI/InsertUpdateBusinessLossDetails", content);
                BusinessLossDetailsResponse result = new BusinessLossDetailsResponse();
                if (Res.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<BusinessLossDetailsResponse>(Res.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = Res.Content.ReadAsStringAsync().Result;
                }
                return RedirectToAction("BusinessLossDetails");
            }
        }

        [HttpGet]
        public async Task<ActionResult> MATCreditDetails()
        {
            Company selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            MATCreditDetails matCreditDetails = Session["CurrentMATCreditDetails"] as MATCreditDetails;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            MATCreditDetailsHeaderModel model = new MATCreditDetailsHeaderModel
            {
                CompanyObject = selectedCompany,
                FYAYId = matCreditDetails != null ? matCreditDetails.FYAYId : (int?) null,
                ITSectionCategoryId = matCreditDetails != null ? matCreditDetails.ITSectionCategoryId : (int?)null,
            };
            Session["CurrentMATCreditDetails"] = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/MasterAPI/GetFYAYList");
                model.FYAYList = JsonConvert.DeserializeObject<List<FYAY>>
                    (Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/MasterAPI/GetITSectionCategoryList");
                model.ITSectionCategories = JsonConvert.DeserializeObject<List<ITSectionCategory>>
                    (Res.Content.ReadAsStringAsync().Result).OrderBy(its=>its.Id).ToList<ITSectionCategory>();
            }
            return View(model);
        }

        
        [HttpGet]
        public async Task<ActionResult> MATCreditDetailsData(int? fyayId
            , int? itSectionCategoryId, int? matCreditDetailsId)
        {
            var selectedCompany = HttpContext.Session["SelectedCompany"] as Company;
            if (selectedCompany == null)
            {
                return RedirectToAction("GetCompanyList");
            }
            MATCreditDetailsDataModel model = new MATCreditDetailsDataModel()
            {
                MATCreditDetailsObject = new MATCreditDetails
                {
                    CompanyId = selectedCompany.Id,
                    AddedBy = Session[SESSION_LOGON_USER] != null ? (Session[SESSION_LOGON_USER] as UserLogin).Id : 1,
                    Id = matCreditDetailsId.HasValue ? matCreditDetailsId.Value : 0,
                    FYAYId = fyayId.HasValue ? fyayId.Value : 0,
                    ITSectionCategoryId = itSectionCategoryId.HasValue ? itSectionCategoryId.Value : 0,
                }
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("api/TaxReturnAPI/GetMATCreditDetailsList?companyId="
                        + selectedCompany.Id + "&fyayId=" + (fyayId.HasValue ? fyayId.Value.ToString() : "")
                        + "&itSectionCategoryId=" + (itSectionCategoryId.HasValue ? itSectionCategoryId.Value.ToString() : "")
                        + "&matCreditDetailsId=" + (matCreditDetailsId.HasValue ? matCreditDetailsId.Value.ToString() : ""));
                    var result = JsonConvert.DeserializeObject<MATCreditDetailsResponse>(Res.Content.ReadAsStringAsync().Result);
                    if (result != null)
                    {
                        if (result.MATCreditDetailsList != null
                            && result.MATCreditDetailsList.Any())
                        {
                            model.MATCreditDetailsObject = result.MATCreditDetailsList.First();
                            model.MATCreditDetailsObject.ModifiedBy = Session[SESSION_LOGON_USER] != null 
                                                      ? (Session[SESSION_LOGON_USER] as UserLogin).Id : 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> InsertUpdateMATCreditDetails
            (MATCreditDetailsDataModel matCreditDetails)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(matCreditDetails.MATCreditDetailsObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync("api/TaxReturnAPI/InsertUpdateMATCreditDetails", content);
                MATCreditDetailsResponse result = new MATCreditDetailsResponse();
                if (Res.IsSuccessStatusCode)
                {
                    Session["CurrentMATCreditDetails"] = matCreditDetails.MATCreditDetailsObject;
                    result = JsonConvert.DeserializeObject<MATCreditDetailsResponse>
                            (Res.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = Res.Content.ReadAsStringAsync().Result;
                }
                return RedirectToAction("MATCreditDetails");
            }
        }
        #endregion
    }
}