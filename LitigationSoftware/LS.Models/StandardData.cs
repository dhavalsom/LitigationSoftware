using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LS.Models
{
    public class StandardData:BaseEntity
    {
        public int FYAYID { get; set; }
        public string FinancialYear { get; set; }
        public string AssessmentYear { get; set; }
        public decimal? BasicTaxRate { get; set; }
        public decimal? MATRate { get; set; }
        public decimal? EducationCess { get; set; }
    }
}
