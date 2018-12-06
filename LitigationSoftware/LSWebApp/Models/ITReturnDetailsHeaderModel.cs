using LS.Models;
using LSWebApp.Infrastructure;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class ITReturnDetailsHeaderModel : ViewModelBase
    {
        #region Properties
        public Company CompanyObject { get; set; }
        public List<FYAY> FYAYList { get; set; }
        public int? FYAYId { get; set; }
        public int? ITSectionCategoryId { get; set; }
        public int? ITSectionId { get; set; }
        public List<ITSectionCategory> ITSectionCategories { get; set; }
        public LitigationDDModel ITSectionListSource { get; set; }
        public List<ITSection> ITSectionList { get; set; }
        #endregion

        #region Constructors
        public ITReturnDetailsHeaderModel() : base(Pages.ITReturnDetailsPage)
        {            
        }
        #endregion

        #region Methods
        #endregion
    }
}