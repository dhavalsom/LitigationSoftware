using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Models
{
    public class Company:BaseEntity
    {
        public string CompanyName { get; set; }
        [RegularExpression(@"/[A-Z]{5}\d{4}[A-Z]{1}/", ErrorMessage = "* Invalid PAN Number")]
        public string PANNumber { get; set; }
        public string LoggedInUserID { get; set; }
        public int CategoryID { get; set; }
    }

    public class CompanyCategory:BaseEntity
    {
        public string CategoryDesc { get; set; }
    }
}
