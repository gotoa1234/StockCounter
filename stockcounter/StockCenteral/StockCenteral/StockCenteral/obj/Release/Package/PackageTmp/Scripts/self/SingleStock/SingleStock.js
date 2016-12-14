$(function () {

    //綁定查詢按鈕的事件
    $("#TddcTable_clickEvent").bind("click", function () {

        CallDataTable(document.getElementById('TDDC_NoList').value, document.getElementById('Tddc_TextBox').value, document.getElementById('datepicker_start').value, document.getElementById('datepicker_end').value);

        CallPeopleDataTable(document.getElementById('TDDC_NoList').value, document.getElementById('Tddc_TextBox').value, document.getElementById('datepicker_start').value, document.getElementById('datepicker_end').value);

    });

    //Datetimepicker 初始化
    DatetimePicker_Initinal();

});
//--集保戶股票分散比例表
function CallDataTable(findstring, QueryStockNo, Datestart, Dateend) {
    var QueryKey = findstring;
    if (QueryStockNo != "") {
        QueryKey = "TDDC_" + QueryStockNo;
    }
    var StoragePlaceParam = {
        'TableName': QueryKey, //--使用者查詢的TableName
        'Date_Start': Datestart,
        'Date_End': Dateend
    };
    $('#StorageTDDCTable').dataTable({


        "data": StorageTDDCGetDB(StoragePlaceParam),
        "pageLength": -1,//每個分頁筆數
        "aLengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "bLengthChange": true,//選擇查詢筆數
        "bFilter": true, //------打開搜尋 
        "bDestroy": true,//-----因為在同個頁面 所以每當載入時需要重新啟動資源
        //"scrollY": "200px",
        //"scrollX": "100px",
        "columns": [
            //{ "data": "Date", "visible": false },
            { "data": "ShowDate", "visible": true },
            { "data": "Level_1" },
            { "data": "Level_2" },
            { "data": "Level_3" },
            { "data": "Level_4" },
            { "data": "Level_5" },
            { "data": "Level_6" },
            { "data": "Level_7" },
            { "data": "Level_8" },
            { "data": "Level_9" },
            { "data": "Level_10" },
            { "data": "Level_11" },
            { "data": "Level_12" },
            { "data": "Level_13" },
            { "data": "Level_14" },
            { "data": "Level_15" }
        ],
        "order": [[0, "asc"]],//預設排序欄位 以第0個 colume 排序 asc
        //"rowCallback": function( row, data ) {
        //    if ($.inArray(data.DT_RowId, selected) !== -1) {
        //        alert(row + " , " + data);
        //        $(row).addClass('selected');
        //    }
        //},
        "language": {
            "sProcessing": "處理中...",
            "sLengthMenu": "顯示 _MENU_ 項結果",
            "sZeroRecords": "沒有匹配結果",
            "sInfo": "顯示第 _START_ 至 _END_ 項結果，共 _TOTAL_ 項",
            "sInfoEmpty": "顯示第 0 至 0 項結果，共 0 項",
            "sInfoFiltered": "(從 _MAX_ 項結果過濾)",
            "sInfoPostFix": "",
            "sSearch": "關鍵字搜索:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "首頁",
                "sPrevious": "上頁",
                "sNext": "下頁",
                "sLast": "尾頁"
            }

        },
        //--當回傳資料為空時(查詢結果是沒有資料)
        "aoColumnDefs": [{ sDefaultContent: '', aTargets: ['_all'] }],
    });

    //$('#StorageTDDCTable').on('click', function () {


    //    alert(table.rows('.selected').data().length );

    //});
}
function StorageTDDCGetDB(QueryParam) {
    //------我們家的網址 
    var root = "http://" + location.host;
    //if (location.host.indexOf("localhost") == -1) {
    //    root = "http://" + location.host + "/" + location.pathname.split('/')[1];
    //}
    //JL().info("123");
    //------攜帶參數
    $.ajax({
        type: 'POST',
        url: root + "/api/SingleStockApi/GetSingleStockMippingName",
        data: QueryParam,  //------------------------參數丟入
        success: function (data) {
            $('#TDDC_QueryResult').text("查詢結果：" + data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            JL().fatal("【" + textStatus + "】:" + jqXHR.responseText + " ==> " + errorThrown);
        },
    });

    $.ajax({
        type: 'POST',
        url: root + "/api/SingleStockApi/GetSingleStockDB",
        data: QueryParam,  //------------------------參數丟入
        success: function (data) {


            var jobDataTable = $("#StorageTDDCTable").dataTable();
            jobDataTable.fnClearTable();
            jobDataTable.fnAddData(data);

            //並且將搜尋的Json資料傳給 chart
            SetSingleStockChart(QueryParam);
            //搜尋結果傳遞給 分群chart
            SetSingleStockKindChart(QueryParam);
            //呼叫使用者自訂chart
            SetSingleStockExtentionKindChartTDDC(QueryParam);

            //設定指定查詢股票的 Iframe
            var kindstr = QueryParam.TableName.replace("TDDC_", "");//將TDDC_ 移除變為 個股代號
            var nowY = new Date().getFullYear() - 1911;//取得當前年度

            $('#innerframe').attr('src', ("http://stock.wearn.com/asale.asp?year=" + nowY + "&kind=" + kindstr));

            //設定指定查詢股票的 股價資訊
              $('#innerframe_price').attr('src', "http://histock.tw/stock/tchart.aspx?no=" + kindstr + "&m=b");
            //$('#innerframe_price').attr('src', "http://so.cnyes.com/JavascriptGraphic/chartstudy.aspx?country=tw&market=tw&code=" + kindstr + "&divwidth=990&divheight=330");
            //外資資料 預設寫死

            //公司基本資料 Iframe
            $('#SingleStock_CompanyFrame').attr('src', ("http://goodinfo.tw/StockInfo/BasicInfo.asp?STOCK_ID=" + kindstr));

            //股東會日程 Iframe
            $('#SingleStock_BossMettingFrame').attr('src', ("http://goodinfo.tw/StockInfo/StockHolderSchedule.asp?STOCK_ID=" + kindstr));

        },
        error: function (jqXHR, textStatus, errorThrown) {
            JL().fatal("【" + textStatus + "】:" + jqXHR.responseText + " ==> " + errorThrown);
        },
    });
}

//--集保戶股東分散比例表
function CallPeopleDataTable(findstring, QueryStockNo, Datestart, Dateend) {


    var QueryKey = findstring;
    if (QueryStockNo != "") {
        QueryKey = "TDDC_" + QueryStockNo;
    }
    var StoragePlaceParam = {
        'TableName': QueryKey, //--使用者查詢的TableName
        'Date_Start': Datestart,
        'Date_End': Dateend
    };
    $('#StorageTDDCPeopleTable').dataTable({

        "responsive": true,
        "data": StorageTDDCPeopleGetDB(StoragePlaceParam),
        "pageLength": 10,//每個分頁筆數
        "aLengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "bLengthChange": true,//選擇查詢筆數
        "bFilter": true, //------打開搜尋 
        "bDestroy": true,//-----因為在同個頁面 所以每當載入時需要重新啟動資源
        //"scrollY": "200px",
        //"scrollX": "100px",
        "columns": [
            //{ "data": "Date", "visible": false },
            { "data": "ShowDate", "visible": true },
            { "data": "Level_1" },
            { "data": "Level_2" },
            { "data": "Level_3" },
            { "data": "Level_4" },
            { "data": "Level_5" },
            { "data": "Level_6" },
            { "data": "Level_7" },
            { "data": "Level_8" },
            { "data": "Level_9" },
            { "data": "Level_10" },
            { "data": "Level_11" },
            { "data": "Level_12" },
            { "data": "Level_13" },
            { "data": "Level_14" },
            { "data": "Level_15" }
        ],
        "order": [[0, "asc"]],//預設排序欄位 以第0個 colume 排序 asc
        "language": {
            "sProcessing": "處理中...",
            "sLengthMenu": "顯示 _MENU_ 項結果",
            "sZeroRecords": "沒有匹配結果",
            "sInfo": "顯示第 _START_ 至 _END_ 項結果，共 _TOTAL_ 項",
            "sInfoEmpty": "顯示第 0 至 0 項結果，共 0 項",
            "sInfoFiltered": "(從 _MAX_ 項結果過濾)",
            "sInfoPostFix": "",
            "sSearch": "關鍵字搜索:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "首頁",
                "sPrevious": "上頁",
                "sNext": "下頁",
                "sLast": "尾頁"
            }

        },
        //--當回傳資料為空時(查詢結果是沒有資料)
        "aoColumnDefs": [{ sDefaultContent: '', aTargets: ['_all'] }],
    });
}
function StorageTDDCPeopleGetDB(QueryParam) {
    //------我們家的網址 
    var root = "http://" + location.host;
    //if (location.host.indexOf("localhost") == -1) {
    //    root = "http://" + location.host + "/" + location.pathname.split('/')[1];
    //}

    //------攜帶參數
    $.ajax({
        type: 'POST',
        url: root + "/api/SingleStockApi/GetSingleStockPeopleDB",
        data: QueryParam,  //------------------------參數丟入
        success: function (data) {

            var jobDataTable = $("#StorageTDDCPeopleTable").dataTable();
            jobDataTable.fnClearTable();
            jobDataTable.fnAddData(data);

            //並且將搜尋的Json資料傳給 chart
            SetSingleStockPeopleChart(QueryParam);
            //搜尋結果傳遞給 分群chart
            SetSingleStockPeopleKindChart(QueryParam);
            //呼叫使用者自訂chart
            SetSingleStockPeopleExtentionKindChartTDDC(QueryParam);

        },
        error: function (jqXHR, textStatus, errorThrown) {
            JL().fatal("【" + textStatus + "】:" + jqXHR.responseText + " ==> " + errorThrown);
        },
    });


}


//Datetimepicker 初始化
function DatetimePicker_Initinal() {
    // last yaer
    var dateNow = new Date();
    var lastyear = dateNow.getTime() - 1000 * 60 * 60 * 24 * 360;
    dateNow.setTime(lastyear);
    //start
    $("#datepicker_start").datepicker();
    $("#datepicker_start").datepicker('setDate', dateNow);
    $('#datepicker_start').datepicker('option', 'dateFormat', 'yy/mm/dd');
    //end
    $("#datepicker_end").datepicker();
    $("#datepicker_end").datepicker('setDate', new Date());
    $('#datepicker_end').datepicker('option', 'dateFormat', 'yy/mm/dd');


}

