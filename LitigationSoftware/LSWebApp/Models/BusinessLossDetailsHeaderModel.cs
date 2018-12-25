using LS.Models;
using LSWebApp.Infrastructure;

namespace LSWebApp.Models
{
    public class BusinessLossDetailsHeaderModel : ViewModelBase
    {
        #region Properties
        public Company CompanyObject { get; set; }
        public FYAY FYAYObject { get; set; }
        public ITSectionCategory ITSectionCategoryObject { get; set; }
        #endregion

        #region Constructors
        public BusinessLossDetailsHeaderModel() : base(Pages.ITReturnDetailsPage)
        {            
        }
        #endregion

        #region Methods
        #endregion
    }
}