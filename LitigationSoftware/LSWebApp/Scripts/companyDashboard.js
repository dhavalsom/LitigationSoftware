if (window.chartFunctions === undefined) {
	window.chartFunctions = [];
}

var chartContainerDiv = "#divChart";
var chartSerieColors = ['#b71c1c', '#1a237e', '#004d40', '#03a9f4', '#ff9800', '#fad84a', '#4caf50'];
var commonKendoChartOption = {
	title: {
		text: "Chart Name"
	},
	seriesColors: chartSerieColors,
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
		title: {
			text: ""
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
	chartId: 2, fn: function () {		
		var kendoChartOption = {
			title: {
				text: "Y-o-Y trends in Effective Tax Rates"
			},
			seriesColors: chartSerieColors,
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
	chartId: 1, fn: function () {
		var kendoChartOption = commonKendoChartOption;
		kendoChartOption.title.text = "Y-o-Y trends in Effective Tax Rates";
		kendoChartOption.valueAxis.title.text = "Tax Rates";
		kendoChartOption.categoryAxis.title.text = "Financial Years";
		kendoChartOption.seriesDefaults.type = "line";
		renderChart(kendoChartOption, { ChartId: chartId, ChartParams: { 'COMPANY_ID': companyId } });
	}
});

window.chartFunctions.push({
	chartId: 3, fn: function () {
		var kendoChartOption = commonKendoChartOption;
		kendoChartOption.title.text = "ADVANCE TAX : Y-o-Y trends in estimating income";
		kendoChartOption.seriesDefaults.type = "column";
		renderChart(kendoChartOption);
	}
});

window.chartFunctions.push({
	chartId: 4, fn: function () {
		var kendoChartOption = commonKendoChartOption;
		kendoChartOption.title.text = "How tax liabilities is discharged (amounts are in INR milion)";
		kendoChartOption.seriesDefaults.type = "column";
		kendoChartOption.seriesDefaults.stack = true;
		renderChart(kendoChartOption);
	}
});

window.chartFunctions.push({
	chartId: 5, fn: function () {
		var kendoChartOption = commonKendoChartOption;
		kendoChartOption.title.text = "TDS CREDIT : Degree of utilization";
		kendoChartOption.seriesDefaults.type = "column";
		renderChart(kendoChartOption);
	}
});

window.chartFunctions.push({
	chartId: 6, fn: function () {
		var kendoChartOption = commonKendoChartOption;
		kendoChartOption.title.text = "Y-o-Y trends in additions by Field Officers";
		kendoChartOption.seriesDefaults.type = "column";
		renderChart(kendoChartOption);
	}
});


window.chartFunctions.push({
	chartId: 7, fn: function () {
		var kendoChartOption = commonKendoChartOption;
		kendoChartOption.title.text = "Top litigious tax issues";
		kendoChartOption.seriesDefaults.type = "column";
		kendoChartOption.seriesDefaults.stack = true;
		renderChart(kendoChartOption);
	}
});

function getDefaultChartModel() {
	var chartDataModel = {
		ChartId: chartId,
		ChartParams: {
			'COMPANY_ID': companyId,
			'NO_OF_YEARS': $('#noOfYears').val()
		}
	};
	return chartDataModel;
}
function renderChart(kendoChartOption, chartDataModel) {
	if (chartDataModel === undefined || chartDataModel === null) {
		chartDataModel = getDefaultChartModel();
	}
	getChartData(chartDataModel, function (response) {
		loadChart(kendoChartOption, response);
	});
}
function loadChart(chartOption, response) {
	if (response != null && !!response.IsSuccess) {
		var series = getSerieArray(response.Data, 'SeriesName', 'Value');
		var finYears = getUniqueValArray(response.Data, 'CategoryName');
		chartOption.series = series;
		chartOption.categoryAxis.categories = finYears;
		setCategoryAxisLabelRotation(finYears.length, chartOption);
	}
	$(chartContainerDiv).kendoChart(chartOption);
}
function getChartData(postData, onSuccess) {
	$.ajax({
		type: 'POST',
		url: '/CompanyDashboard/ChartData',
		data: JSON.stringify(postData),
		success: function (response) {
			if (onSuccess) {
				onSuccess.apply(null, [response]);
			}
		},
		contentType: "application/json",
		dataType: 'json'
	});
}
