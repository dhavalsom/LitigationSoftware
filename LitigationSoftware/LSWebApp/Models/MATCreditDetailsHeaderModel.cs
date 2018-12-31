using LS.Models;
using LSWebApp.Infrastructure;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class MATCreditDetailsHeaderModel : ViewModelBase
    {
        #region Properties
        public Company CompanyObject { get; set; }
        public int? FYAYId { get; set; }
        public int? ITSectionCategoryId { get; set; }
        public List<FYAY> FYAYList { get; set; }
        public List<ITSectionCategory> ITSectionCategories { get; set; }
        #endregion

        #region Constructors
        public MATCreditDetailsHeaderModel() : base(Pages.ITReturnDetailsPage)
        {            
        }
        #endregion

        #region Methods
        #endregion
    }
}