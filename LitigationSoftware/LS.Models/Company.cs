using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Models
{
    public class Company:BaseEntity
    {
        public string CompanyName { get; set; }
        public string PANNumber { get; set; }
        public string LoggedInUserID { get; set; }
    }
}
