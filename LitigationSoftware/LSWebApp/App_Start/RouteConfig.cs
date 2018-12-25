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
                name: "manageLAndSComments",
                url: "manageLAndSComments",
                defaults: new { controller = "TaxReturn", action = "InsertUpdateLAndSComments", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "deletetITReturnDocument",
                url: "deletetITReturnDocument",
                defaults: new { controller = "TaxReturn", action = "DeleteITReturnDocuments", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "itreturndetaildocuments",
                url: "itreturndetaildocuments",
                defaults: new { controller = "TaxReturn", action = "GetITReturnDocumentList", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "createcompany",
                url: "createcompany",
                defaults: new { controller = "TaxReturn", action = "CreateCompany"
                    , id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "getcompanies",
                url: "getcompanies",
                defaults: new { controller = "TaxReturn", action = "GetCompanyList"
                    , id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "inputsheet",
               url: "inputsheet",
               defaults: new
               {
                   controller = "TaxReturn",
                   action = "ITReturnDetails"
               }
           );

            routes.MapRoute(
               name: "bldetails",
               url: "bldetails",
               defaults: new
               {
                   controller = "TaxReturn",
                   action = "BusinessLossDetails"
               }
           );

            routes.MapRoute(
                name: "computationsheet",
                url: "computationsheet",
                defaults: new
                {
                    controller = "TaxReturn",
                    action = "ExistingITReturnDetails"
                }
            );

            routes.MapRoute(
                name: "dashboard",
                url: "dashboard",
                defaults: new
                {
                    controller = "TaxReturn",
                    action = "Index"
                    ,
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "rdanalysis",
                url: "rdanalysis",
                defaults: new
                {
                    controller = "TaxReturn",
                    action = "RDAnalysis"
                    ,
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "summaryreport",
                url: "summaryreport",
                defaults: new
                {
                    controller = "TaxReturn",
                    action = "SummaryReport"
                    ,
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "flowchart",
                url: "flowchart",
                defaults: new
                {
                    controller = "TaxReturn",
                    action = "FlowChart"
                    ,
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "blanalysis",
                url: "blanalysis",
                defaults: new
                {
                    controller = "TaxReturn",
                    action = "BusinessLossAnalysis"
                    ,
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "lands",
                url: "lands",
                defaults: new
                {
                    controller = "TaxReturn",
                    action = "LitigationAndSimulation"
                    ,
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "matcredit",
                url: "matcredit",
                defaults: new
                {
                    controller = "TaxReturn",
                    action = "MatCreditStatus",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "companydashboard",
                url: "companydashboard/{companyId}",
                defaults: new
                {
                    controller = "TaxReturn",
                    action = "CompanyDashboard",
                    id = UrlParameter.Optional
                }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
