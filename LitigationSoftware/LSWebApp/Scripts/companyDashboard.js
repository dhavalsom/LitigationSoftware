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

function getUniqueValArray(data, valProp) {
	var list = [];
	if (data != null && data.length > 0) {		
		$.each(data, function (idx, item) {
			var isFound = false;
			$.each(list, function (idx, val) {
				if (val == item[valProp]) {
					isFound = true;
				}
			});
			if (!isFound) {
				list.push(item[valProp]);
			}
		});		
	}
	return list;
}

function getSerieArray(data, nameProp, dataProp) {
	var series = [];

	if (data != null && data.length > 0) {
		$.each(data, function (idx, item) {
			var srItem = null;
			$.each(series, function (idx, sr) {
				if (sr.name == item[nameProp]) {
					srItem = sr;
				}
			});
			if (srItem == null) {
				srItem = { name: item[nameProp], data: [] };
				series.push(srItem);
			}
			if (srItem) {
				srItem.data.push(item[dataProp]);
			}
		});
	}

	return series;
}

function setCategoryAxisLabelRotation(catLen, chartOpt) {
	if (catLen > 7) {
		chartOpt.categoryAxis.labels.rotation = -90;
	}
}

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
						var series = getSerieArray(response.CompetitorTaxRates, 'CompetitorName', 'TaxRate');
						var finYears = getUniqueValArray(response.CompetitorTaxRates, 'FinancialYear');
						kendoChartOption.series = series;
						kendoChartOption.categoryAxis.categories = finYears;
						setCategoryAxisLabelRotation(finYears.length, kendoChartOption);
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
						});
						finYears = getUniqueValArray(response.ITReturnProvisions, 'FinancialYear');
						kendoChartOption.series = series;
						kendoChartOption.categoryAxis.categories = finYears;
						setCategoryAxisLabelRotation(finYears.length, kendoChartOption);
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
	chartId: 3, fn: function () {
		var kendoChartOption = {
			title: {
				text: "ADVANCE TAX : Y-o-Y trends in estimating income"
			},
			legend: {
				position: "bottom"
			},
			chartArea: {
				background: ""
			},
			seriesDefaults: {
				type: "column",
				style: "smooth"
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
				url: '/CompanyDashboard/QuarterlyAdvanceTaxes?companyId=' + companyId + '&noOfYears=' + noOfYears,
				success: function (response) {
					if (response != null && !!response.IsSuccess) {
						var series = getSerieArray(response.AdvanceTaxes, 'Quarter', 'AdvanceTax');
						var finYears = getUniqueValArray(response.AdvanceTaxes, 'FinancialYear');
						kendoChartOption.series = series;
						kendoChartOption.categoryAxis.categories = finYears;
						setCategoryAxisLabelRotation(finYears.length, kendoChartOption);
					}
					$(chartContainerDiv).kendoChart(kendoChartOption);
				},
				contentType: "application/json",
				dataType: 'json'
			});
		})();
	}
});