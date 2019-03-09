using System.Web;
using System.Collections.Generic;
using LS.Models;

namespace LSWebApp.Models
{
    public class ITHeadDocumentsUploaderModel
    {
        public HttpPostedFileBase ITHeadFile { get; set; }
        public List<ITReturnDocumentsDisplay> DocumentList { get; set; }
        public ITHeadMaster ITHeadObject { get; set; }
        public ITReturnDocuments ObjITReturnDocuments { get; set; }
        public ITReturnDetails ObjITReturnDetails { get; set; }
        public List<DocumentCategoryMaster> DocumentCategoryList { get; set; }
        public List<SubDocumentCategoryMaster> SubDocumentCategoryList { get; set; }

        public ITHeadDocumentsUploaderModel(List<ITReturnDocumentsDisplay> documentList
            , ITHeadMaster itHeadObject, ITReturnDetails objITReturnDetails
            , List<DocumentCategoryMaster> documentCategoryList
            , List<SubDocumentCategoryMaster> subDocumentCategoryList)
        {
            ObjITReturnDocuments = new ITReturnDocuments();
            ObjITReturnDocuments.ITReturnDetailsId = objITReturnDetails.Id;
            this.DocumentList = documentList;
            this.ObjITReturnDetails = objITReturnDetails;
            this.DocumentCategoryList = documentCategoryList;
            this.SubDocumentCategoryList = subDocumentCategoryList;
        }

        public ITHeadDocumentsUploaderModel()
        {

        }
    }
}