using LS.Models;

namespace LSWebApp.Models
{
    public class BusinessLossDetailsDataModel
    {
        #region Properties
        public BusinessLossDetails BusinessLossDetailsObject { get; set; }
        #endregion

        #region Constructors
        public BusinessLossDetailsDataModel()
        {
            BusinessLossDetailsObject = new BusinessLossDetails();
        }
        #endregion

        #region Methods
        #endregion
    }
}