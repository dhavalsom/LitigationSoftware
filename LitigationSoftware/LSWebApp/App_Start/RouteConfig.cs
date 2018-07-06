using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LSWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "getITSections",
               url: "getITSections",
               defaults: new { controller = "TaxReturn", action = "GetITSectionList", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "manageITSection",
                url: "manageITSection",
                defaults: new { controller = "TaxReturn", action = "InsertUpdateITSection", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "compliancedocuments",
                url: "compliancedocuments",
                defaults: new { controller = "TaxReturn", action = "GetComplianceDocumentList", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "compliances",
                url: "compliances",
                defaults: new { controller = "TaxReturn", action = "GetComplianceList", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "manageITSubHead",
                url: "manageITSubHead",
                defaults: new { controller = "TaxReturn", action = "InsertUpdateITSubHeadMaster", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
