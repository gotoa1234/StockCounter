$(function () {
    // Highcharts.setOptions(ThemesHightcharts_colume_darkblue);

    $('#SingleStockKindChartTDDC').highcharts({

        chart: {
            type: 'column'
        },
        title: {
            text: '籌碼圖表_群組分類(散戶50以下 中實戶50~800 大戶 800以上)'
        },
        subtitle: {
            text: '作者: 仙草奶綠'
        },
        xAxis: {
            categories: [
                '日期',
            ],
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Rainfall (%)'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.2f} %</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.1,
                borderWidth: 0
            }
        },
        series: [{ name: '50以下(張、股)', data: [0.00], visible: true },
            { name: '50-800(張)', data: [0.00], visible: true },
            { name: '800以上(張)', data: [0.00], visible: true }
        ]
    });

});

function SetSingleStockKindChart(Param) {


    //------我們家的網址 
    var root = "http://" + location.host;
    //if (location.host.indexOf("localhost") == -1) {
    //    root = "http://" + location.host + "/" + location.pathname.split('/')[1];
    //}

    $.ajax({
        type: 'POST',
        url: root + "/api/SingleStockApi/GetSingleStockKindChart",
        data: Param,  //------------------------參數丟入
        success: function (data) {

            var TargetChart = $('#SingleStockKindChartTDDC').highcharts();
            TargetChart.xAxis[0].setCategories(data.xAxis);
            TargetChart.series[0].setData(data.Retail);
            TargetChart.series[1].setData(data.Solid_household);
            TargetChart.series[2].setData(data.Large);


        },
        error: function (jqXHR, textStatus, errorThrown) {
            JL().fatal("【" + textStatus + "】:" + jqXHR.responseText + " ==> " + errorThrown);
        },
    });
}
