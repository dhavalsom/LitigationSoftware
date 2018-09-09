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

        public ITHeadDocumentsUploaderModel(List<ITReturnDocumentsDisplay> documentList
            , ITHeadMaster itHeadObject, ITReturnDetails objITReturnDetails)
        {
            ObjITReturnDocuments = new ITReturnDocuments();
            ObjITReturnDocuments.ITHeadId = itHeadObject.Id;
            ObjITReturnDocuments.ITReturnDetailsId = objITReturnDetails.Id;
            this.DocumentList = documentList;
            this.ITHeadObject = itHeadObject;
            this.ObjITReturnDetails = objITReturnDetails;
        }

        public ITHeadDocumentsUploaderModel()
        {

        }
    }
}