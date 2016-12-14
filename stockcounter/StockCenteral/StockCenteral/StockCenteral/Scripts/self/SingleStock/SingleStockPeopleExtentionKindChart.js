$(function () {
  

    $('#SingleStockPeopleExtentionKindChartTDDC').highcharts({

        chart: {
            type: 'column'
        },
        title: {
            text: '股東人數圖表_使用者自訂群組'
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
                text: 'Rainfall (人)'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.0f} 人</b></td></tr>',
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
        series: [{ name: '自訂1', data: [0], visible: true },
            { name: '自訂2', data: [0], visible: true },
            { name: '自訂3', data: [0], visible: true },
            { name: '自訂4', data: [0], visible: true },
            { name: '自訂5', data: [0], visible: true }
        ]
    });
 
});

function SetSingleStockPeopleExtentionKindChartTDDC(Param) {

    var MaxGroup = 5;//群組數量

    var obj = [];
    var tempGroupName = [];
    for (var i = 1; i < MaxGroup + 1; i++)
    {
        var tempStateList = [];
        //get checkbox
        for (var j = 1 ; j < 16; j++) {
            tempStateList.push($("#autopeoplekind_" + i + "_" + j).is(":checked"));
        }
        obj.push(tempStateList)
        tempGroupName.push($("#autopeoplekind_name_" + i).val());
    }

    var StoragePlaceParam = {
        'TableName': Param.TableName, //--使用者查詢的TableName
        'Date_Start': Param.Date_Start,
        'Date_End': Param.Date_End,
        'Combination': obj,
        'GroupName': tempGroupName,
        'GroupCount': 5
    };


    //------我們家的網址 
    var root = "http://" + location.host;
    //if (location.host.indexOf("localhost") == -1) {
    //    root = "http://" + location.host + "/" + location.pathname.split('/')[1];
    //}

    $.ajax({
        type: 'POST',
        url: root + "/api/SingleStockApi/GetSingleStockPeopleExtentionKindChart",
        data: StoragePlaceParam,  //------------------------參數丟入
        success: function (data) {
            //指定hightChart
            var TargetChart = $('#SingleStockPeopleExtentionKindChartTDDC').highcharts();
            //放進日期
            TargetChart.xAxis[0].setCategories(data.xAxis);
            for (var i = 0 ; i < 5; i++) {
                //更新名稱
                TargetChart.series[i].update({ name: data.GroupName[i] },false);
                //將群組資料放進
                TargetChart.series[i].setData(data.Detail[i]);
            }
            //重新繪製
            TargetChart.redraw();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            JL().fatal("【" + textStatus + "】:" + jqXHR.responseText + " ==> " + errorThrown);
        },
    });
}
