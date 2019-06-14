using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Models
{
	public class ChartData
	{
		public int Id { get; set; }
		public string SeriesName { get; set; }
		public string CategoryName { get; set; }
		public decimal Value { get; set; }
	}

	public class ChartDataResponse
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
		public List<ChartData> Data { get; set; }
		public ChartDataResponse()
		{
			this.Data = new List<ChartData>();
		}
	}

	public class ChartDataModel
	{
		public int ChartId { get; set; }
		public IDictionary<string, object> ChartParams { get; set; }
	}

}
