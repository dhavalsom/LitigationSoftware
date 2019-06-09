if (window.chartFunctions === undefined) {
	window.chartFunctions = [];
}

window.chartFunctions.push(function () {
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
			axisCrossingValue: -10
		},
		categoryAxis: {
			categories: [],
			majorGridLines: {
				visible: false
			},
			labels: {
				rotation: "auto"
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
				}
				$("#chartCTR").kendoChart(kendoChartOption);
			},
			contentType: "application/json",
			dataType: 'json'
		});
	})();
});