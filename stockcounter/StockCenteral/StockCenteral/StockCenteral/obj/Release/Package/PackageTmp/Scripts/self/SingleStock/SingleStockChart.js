$(function () {
    Highcharts.setOptions(ThemesHightcharts_colume_darkblue);
    
    $('#SingleStockChartTDDC').highcharts({
        //options: MySkin,
        chart: {
          
            type: 'column'
        },
        title: {
            text: '持有籌碼比率圖表'
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
        
        series: [
            
            { name: '1-999(股)', data: [0.00], visible: false },
            { name: '1-5(張)', data: [0.00], visible: false },
            { name: '5-10(張)', data: [0.00], visible: false },
            { name: '10-15(張)', data: [0.00], visible: false },
            { name: '15-20(張)', data: [0.00], visible: false },
            { name: '20-30(張)', data: [0.00], visible: false },
            { name: '30-40(張)', data: [0.00], visible: false },
            { name: '40-50(張)', data: [0.00], visible: false },
            { name: '50-100(張)', data: [0.00], visible: false },
            { name: '100-200(張)', data: [0.00], visible: false },
            { name: '200-400(張)', data: [0.00], visible: false },
            { name: '400-600(張)', data: [0.00], visible: false },
            { name: '600-800(張)', data: [0.00], visible: false },
            { name: '800-1000(張)', data: [0.00], visible: false },
            { name: '1000以上(張)', data: [0.00] }]
    });
    //var opt = {
    //    "oLanguage": { "sUrl": "dataTables.zh-tw.txt" },
    //    "bJQueryUI": true
    //};
    var oTable = $("#StorageTDDCTable").dataTable();
    oTable.on('click','tr',function() {
        var row = oTable.fnGetData(this);

    });
});

function SetSingleStockChart(Param) {


    //------我們家的網址 
    var root = "http://" + location.host;
    //if (location.host.indexOf("localhost") == -1) {
    //    root = "http://" + location.host + "/" + location.pathname.split('/')[1];
    //}

    $.ajax({
        type: 'POST',
        url: root + "/api/SingleStockApi/GetSingleStockChart",
        data: Param,  //------------------------參數丟入
        success: function (data) {

            var TargetChart = $('#SingleStockChartTDDC').highcharts();
            TargetChart.xAxis[0].setCategories(data.xAxis);
            TargetChart.series[0].setData(data.seriesLevel1);
            TargetChart.series[1].setData(data.seriesLevel2);
            TargetChart.series[2].setData(data.seriesLevel3);
            TargetChart.series[3].setData(data.seriesLevel4);
            TargetChart.series[4].setData(data.seriesLevel5);
            TargetChart.series[5].setData(data.seriesLevel6);
            TargetChart.series[6].setData(data.seriesLevel7);
            TargetChart.series[7].setData(data.seriesLevel8);
            TargetChart.series[8].setData(data.seriesLevel9);
            TargetChart.series[9].setData(data.seriesLevel10);
            TargetChart.series[10].setData(data.seriesLevel11);
            TargetChart.series[11].setData(data.seriesLevel12);
            TargetChart.series[12].setData(data.seriesLevel13);
            TargetChart.series[13].setData(data.seriesLevel14);
            TargetChart.series[14].setData(data.seriesLevel15);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            JL().fatal("【" + textStatus + "】:" + jqXHR.responseText + " ==> " + errorThrown);
        },
    });
}
