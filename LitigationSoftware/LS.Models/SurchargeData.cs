using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Models
{
    public class SurchargeData:BaseEntity
    {
        public int FYAYID { get; set; }
        public string FinancialYear { get; set; }
        public string AssessmentYear { get; set; }
        public decimal? surchargefromthreshold { get; set; }
        public decimal? surchargetothreshold { get; set; }
        public decimal? surchargerate { get; set; }
        public int? entitycategorytypeid { get; set; }
    }
}
