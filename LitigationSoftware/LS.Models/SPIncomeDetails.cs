using System;
using System.Collections.Generic;

namespace LS.Models
{
    public class SPIncomeDetails : BaseEntity
    {
        #region Properties
        public int ITReturnDetailsId { get; set; }
        public int ITHeadId { get; set; }
        public string SPIncomeDescription { get; set; }
        public decimal? SPIncomeValue { get; set; }
        public decimal? TaxRate { get; set; }
        public DateTime? SPIncomeDate { get; set; }
        #endregion

        #region Display Properties
        public string ITHeadDescription { get; set; }
        public string PropertyName { get; set; }
        #endregion

        #region Serialization
        public bool ShouldSerializeSPIncomeDescription()
        {
            return !String.IsNullOrEmpty(SPIncomeDescription);
        }

        public bool ShouldSerializeSPIncomeValue()
        {
            return SPIncomeValue.HasValue;
        }

        public bool ShouldSerializeTaxRate()
        {
            return TaxRate.HasValue;
        }

        public bool ShouldSerializeSPIncomeDate()
        {
            return SPIncomeDate.HasValue;
        }
        
        #endregion

        #region Methods
        #endregion
    }

    public class SPIncomeDetailsResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<SPIncomeDetails> SPIncomeDetailsList { get; set; }
        public SPIncomeDetailsResponse()
        {
            SPIncomeDetailsList = new List<SPIncomeDetails>();
        }
    }
}
