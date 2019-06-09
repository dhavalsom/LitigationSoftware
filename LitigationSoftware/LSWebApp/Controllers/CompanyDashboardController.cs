using LS.Models;
using LSWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LSWebApp.Controllers
{
    public class CompanyDashboardController : ControllerBase
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View(new CompanyDashboardModel() { CompanyObject = HttpContext.Session["SelectedCompany"]  as Company });
        }

		[HttpGet]
		public async Task<ActionResult> CompetitorTaxRates(int companyId)
		{
			CompetitorTaxRateReportResponse resModel = null;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage Res = await client.GetAsync("api/CompanyDashboardAPI/CompetitorTaxRates?companyId=" + companyId.ToString());
				if (Res.IsSuccessStatusCode)
				{
					resModel = JsonConvert.DeserializeObject<CompetitorTaxRateReportResponse>(Res.Content.ReadAsStringAsync().Result);					
				}
			}
			return Json(resModel,JsonRequestBehavior.AllowGet);
		}
	}
}