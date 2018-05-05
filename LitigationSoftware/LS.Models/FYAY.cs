using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Models
{
    public class FYAY : BaseEntity
    {
        public string FinancialYear { get; set; }
        public string AssessmentYear { get; set; }
        public bool IsDefault { get; set; }
    }
}
