using LS.Models;
using LSWebApp.Infrastructure;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class MatCreditStatusModel : ViewModelBase
    {
        public Company CompanyObject { get; set; }
        public List<MATCreditDetails> MATCreditDetailsList { get; set; }
        public List<FYAY> FYAYList { get; set; }

        public MatCreditStatusModel() : base(Pages.ITReturnDetailsPage)
        {
            MATCreditDetailsList = new List<MATCreditDetails>();
            FYAYList = new List<FYAY>();
        }
    }
}