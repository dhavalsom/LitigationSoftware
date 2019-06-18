using LS.Models;
using LSWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
		public ActionResult Charts(int chartId)
		{
			var model = new ChartModel()
			{
				CompanyObject = HttpContext.Session["SelectedCompany"] as Company,
				ChartId = chartId
			};
			return View(model);
		}

		[HttpGet]
		public async Task<ActionResult> ITReturnProvisions(int companyId, int noOfYears)
		{
			ITReturnProvisionReportResponse resModel = null;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage Res = await client.GetAsync("api/CompanyDashboardAPI/ITReturnProvisions?companyId=" + companyId.ToString() + "&NoOfYears=" + noOfYears.ToString());
				if (Res.IsSuccessStatusCode)
				{
					resModel = JsonConvert.DeserializeObject<ITReturnProvisionReportResponse>(Res.Content.ReadAsStringAsync().Result);
				}
			}
			return Json(resModel, JsonRequestBehavior.AllowGet);
		}
		
		[HttpPost]
		public async Task<ActionResult> ChartData(ChartDataViewModel model)
		{
			ChartDataResponse resModel = null;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				var json = JsonConvert.SerializeObject(model);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				HttpResponseMessage Res = await client.PostAsync("api/CompanyDashboardAPI/ChartData", content);
				if (Res.IsSuccessStatusCode)
				{
					resModel = JsonConvert.DeserializeObject<ChartDataResponse>(Res.Content.ReadAsStringAsync().Result);
				}
			}
			return Json(resModel, JsonRequestBehavior.AllowGet);
		}


	}
}