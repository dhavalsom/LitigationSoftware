using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Models
{
	public class ITReturnProvisionReport
	{
		public int FYAYId { get; set; }
		public string FinancialYear { get; set; }
		public decimal TaxProvisions { get; set; }
		public decimal TaxAssets { get; set; }
		public decimal ContingentLiabilities { get; set; }
	}

	public class ITReturnProvisionReportResponse
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
		public List<ITReturnProvisionReport> ITReturnProvisions { get; set; }
		public ITReturnProvisionReportResponse()
		{
			this.ITReturnProvisions = new List<ITReturnProvisionReport>();
		}
	}

}
