if (window.chartFunctions === undefined) {
	window.chartFunctions = [];
}

var chartContainerDiv = "#divChart";

$(document).ready(function () {
	displayChart();
});
function displayChart() {
	if (window.chartFunctions !== undefined && window.chartFunctions.length > 0) {
		var chartItem = null; //window.chartFunctions.find(ch => ch.chartId == chartId);
		$.each(window.chartFunctions, function(idx, item) {
			if (item.chartId == chartId) {
				chartItem = item;
			}
		});
		if (chartItem != null) {
			if (chartItem.fn) {
				chartItem.fn.apply();
			}
		}
	}
}
$('#btnBack').click(function () {
	window.location = '/CompanyDashboard/';
});
$('#noOfYears').change(function () {
	displayChart();
});

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
						$.each(response.CompetitorTaxRates, function (idx, item) {
							var srItem = null;
							if (series.length == 0) {
								srItem = { name: item.CompetitorName, data: [] };
								series.push(srItem);
							} else {
								$.each(series, function (idx, sr) {
									if (sr.name == item.CompetitorName) {
										srItem = sr;
									}
								});								
							}
							if (srItem) {
								srItem.data.push(item.TaxRate);
							}
							var isYearFound = false;
							$.each(finYears, function (idx, fy) {
								if (fy == item.FinancialYear) {
									isYearFound = true;
								}
							});
							if (!isYearFound) {
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
						$.each(response.ITReturnProvisions, function (idx, item) {
							series[0].data.push(item.TaxProvisions);
							series[1].data.push(item.TaxAssets);
							series[2].data.push(item.ContingentLiabilities);

							var isYearFound = false;
							$.each(finYears, function (idx, fy) {
								if (fy == item.FinancialYear) {
									isYearFound = true;
								}
							});
							if (!isYearFound) {
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