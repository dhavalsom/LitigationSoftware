if (window.chartFunctions === undefined) {
	window.chartFunctions = [];
}

var chartContainerDiv = "#divChart";

window.chartFunctions.push({
	chartId: 1, fn: function () {
		var kendoChartOption = {
			title: {
				text: "Y-o-Y trends in Effective Tax Rates"
			},
			legend: {
				position: "bottom"
			},
			chartArea: {
				background: ""
			},
			seriesDefaults: {
				type: "line",
				style: "smooth"
			},
			series: [],
			valueAxis: {
				labels: {
					format: "{0}%"
				},
				line: {
					visible: false
				},
				axisCrossingValue: -10,
				title: {
					text: "Tax Rates"
				}
			},
			categoryAxis: {
				categories: [],
				majorGridLines: {
					visible: false
				},
				labels: {
					rotation: "auto"
				},
				title: {
					text: "Financial Years"
				}
			},
			tooltip: {
				visible: true,
				format: "{0}%",
				template: "#= series.name #: #= value #"
			}
		};
		(function () {
			$.ajax({
				type: 'GET',
				url: '/CompanyDashboard/CompetitorTaxRates?companyId=' + companyId,
				success: function (response) {
					if (response != null && !!response.IsSuccess) {
						var series = [];
						var finYears = [];
						$.each(response.CompetitorTaxRates, (idx, item) => {
							var srItem = null;
							if (series.length == 0 || series.findIndex(sr => sr.name == item.CompetitorName) == -1) {
								srItem = { name: item.CompetitorName, data: [] };
								series.push(srItem);
							} else {
								srItem = series.find(sr => sr.name == item.CompetitorName);
							}
							srItem.data.push(item.TaxRate);

							if (finYears.findIndex(fy => fy == item.FinancialYear) == -1) {
								finYears.push(item.FinancialYear);
							}
						});
						kendoChartOption.series = series;
						kendoChartOption.categoryAxis.categories = finYears;
						if (finYears.length > 7) {
							kendoChartOption.categoryAxis.labels.rotation = -90;
						}
					}
					$(chartContainerDiv).kendoChart(kendoChartOption);
				},
				contentType: "application/json",
				dataType: 'json'
			});
		})();
	}
});

window.chartFunctions.push({
	chartId: 2, fn: function () {		
		var kendoChartOption = {
			title: {
				text: "Y-o-Y trends in Effective Tax Rates"
			},
			legend: {
				position: "bottom"
			},
			chartArea: {
				background: ""
			},
			series: [],
			valueAxis: {
				labels: {
					format: "{0}"
				},
				line: {
					visible: false
				},
				axisCrossingValue: -10
			},
			categoryAxis: {
				categories: [],
				majorGridLines: {
					visible: false
				},
				labels: {
					rotation: "auto"
				},
				title: {
					text: "Financial Years"
				}
			},
			tooltip: {
				visible: true,
				format: "{0}%",
				template: "#= series.name #: #= value #"
			}
		};
		(function () {
			var noOfYears = $('#noOfYears').val();
			$.ajax({
				type: 'GET',
				url: '/CompanyDashboard/ITReturnProvisions?companyId=' + companyId + "&noOfYears=" + noOfYears,
				success: function (response) {
					if (response != null && !!response.IsSuccess) {
						var series = [{ type: "column", name: "Tax Provisions", data: [] }, { type: "column", name: "Tax Assets", data: [] }, { type: "line", name: "Contingent Liabilities", data: [] }];
						var finYears = [];
						$.each(response.ITReturnProvisions, (idx, item) => {
							series[0].data.push(item.TaxProvisions);
							series[1].data.push(item.TaxAssets);
							series[2].data.push(item.ContingentLiabilities);

							if (finYears.findIndex(fy => fy == item.FinancialYear) == -1) {
								finYears.push(item.FinancialYear);
							}
						});
						kendoChartOption.series = series;
						kendoChartOption.categoryAxis.categories = finYears;
						if (finYears.length > 7) {
							kendoChartOption.categoryAxis.labels.rotation = -90;
						}
					}
					$(chartContainerDiv).kendoChart(kendoChartOption);
				},
				contentType: "application/json",
				dataType: 'json'
			});
		})();
	}
});