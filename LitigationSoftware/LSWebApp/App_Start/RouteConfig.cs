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
               name: "login",
               url: "login",
               defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "getITSections",
               url: "getITSections",
               defaults: new { controller = "TaxReturn", action = "GetITSectionList", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "getCompliance",
               url: "getCompliance",
               defaults: new { controller = "TaxReturn", action = "GetComplianceMasterList", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "manageITSection",
                url: "manageITSection",
                defaults: new { controller = "TaxReturn", action = "InsertUpdateITSection", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "manageCompliance",
               url: "manageCompliance",
               defaults: new { controller = "TaxReturn", action = "InsertUpdateComplianceMaster", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "compliancedocuments",
                url: "compliancedocuments",
                defaults: new { controller = "TaxReturn", action = "GetComplianceDocumentList", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "checkcompliancedocuments",
                url: "checkcompliancedocuments",
                defaults: new { controller = "TaxReturn", action = "CheckComplianceDocument", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "compliances",
                url: "compliances",
                defaults: new { controller = "TaxReturn", action = "GetComplianceList", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "deletecompliance",
                url: "deletecompliance",
                defaults: new { controller = "TaxReturn", action = "DeleteComplianceDocuments", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "insertupdatecompliance",
                url: "insertupdatecompliance",
                defaults: new { controller = "TaxReturn", action = "InsertUpdateComplianceDocuments", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "manageITSubHead",
                url: "manageITSubHead",
                defaults: new { controller = "TaxReturn", action = "InsertUpdateITSubHeadMaster", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
