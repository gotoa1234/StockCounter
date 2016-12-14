//$(function () {
//});

//function CallLeardBoardTable(Datestart, Dateend) {

//    var StoragePlaceParam = {
//        'TableName': 'empty', //--使用者查詢的TableName
//        'Date_Start': Datestart,
//        'Date_End': Dateend
//    };

//    $('#StorageLearderBoardTable').dataTable({


//        "data": StorageTDDCGetDB(StoragePlaceParam),
//        "pageLength": 10,//每個分頁筆數
//        "aLengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
//        "bLengthChange": true,//選擇查詢筆數
//        "bFilter": true, //------打開搜尋 
//        "bDestroy": false,//-----因為在同個頁面 所以每當載入時需要重新啟動資源
//        "columns": [
//            //{ "data": "Date", "visible": false },
//            { "data": "mapping_tablename", "visible": true },
//            { "data": "RetailRate" },
//            { "data": "LaregeRate" }
//        ],
//        "order": [[1, "asc"]],//預設排序欄位 以第0個 colume 排序 asc
//        //"rowCallback": function( row, data ) {
//        //    if ($.inArray(data.DT_RowId, selected) !== -1) {
//        //        alert(row + " , " + data);
//        //        $(row).addClass('selected');
//        //    }
//        //},
//        "language": {
//            "sProcessing": "處理中...",
//            "sLengthMenu": "顯示 _MENU_ 項結果",
//            "sZeroRecords": "沒有匹配結果",
//            "sInfo": "顯示第 _START_ 至 _END_ 項結果，共 _TOTAL_ 項",
//            "sInfoEmpty": "顯示第 0 至 0 項結果，共 0 項",
//            "sInfoFiltered": "(從 _MAX_ 項結果過濾)",
//            "sInfoPostFix": "",
//            "sSearch": "關鍵字搜索:",
//            "sUrl": "",
//            "oPaginate": {
//                "sFirst": "首頁",
//                "sPrevious": "上頁",
//                "sNext": "下頁",
//                "sLast": "尾頁"
//            }

//        },
//        //--當回傳資料為空時(查詢結果是沒有資料)
//        "aoColumnDefs": [{ sDefaultContent: '', aTargets: ['_all'] }],
//    });
//}
//function StorageTDDCGetDB(QueryParam , JsonObj) {

//    //alert('fuck');
//    //var jobDataTable = $("#StorageLearderBoardTable").dataTable();
//    //jobDataTable.fnClearTable();
//    //jobDataTable.fnAddData(JsonObj);

//    ////------我們家的網址 
//    //var root = "http://" + location.host;
//    //if (location.host.indexOf("localhost") == -1) {
//    //    root = "http://" + location.host + "/" + location.pathname.split('/')[1];
//    //}

//    ////------顯示等候中
//    //showBlockUI();
//    //$.ajax({
//    //    type: 'POST',
//    //    url: root + "/api/LeaderboardApi/GetLeaderboardTDDC",
//    //    data: QueryParam,  //------------------------參數丟入
//    //    success: function (data) {
//    //        //將BlockUI 關閉
//    //        $.unblockUI();

//    //        var jobDataTable = $("#StorageLearderBoardTable").dataTable();
//    //        jobDataTable.fnClearTable();
//    //        jobDataTable.fnAddData(data);

//    //    }
//    //});

//}

