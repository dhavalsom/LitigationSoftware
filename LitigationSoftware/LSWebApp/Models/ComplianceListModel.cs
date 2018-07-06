﻿using LS.Models;
using System.Collections.Generic;
using System.Web;

namespace LSWebApp.Models
{
    public class ComplianceListModel
    {
        #region Proeprties
        
        public List<FYAY> FYAYList { get; set; }
        public List<Company> CompanyList { get; set; }
        public List<ComplianceMaster> ComplianceList { get; set; }
        public HttpPostedFileBase ReportFile { get; set; }
        public ComplianceDocuments ObjComplianceDocuments { get; set; }
        public ComplianceDocumentListModel ObjComplianceDocumentListModel { get; set; }

        #endregion

        #region Constructors
        public ComplianceListModel()
        {
        }
        #endregion
    }
}