$(function () {

    ////表單初始化
    //initModal();
    //kedowindow init
    KedoInit();
    //初始化datatables
    InitDatatable();
    //綁定新增按鈕的onclick New_Rowdata
    $("#New_Rowdata").on("click", function (event) {

        _Addfun();
    });

});

///撈取資料
function NewBoardsEditGetDB() {
    //------我們家的網址 
    var root = "http://" + location.host;
    //if (location.host.indexOf("localhost") == -1) {
    //    root = "http://" + location.host + "/" + location.pathname.split('/')[1];
    //}

    //------攜帶參數
    $.ajax({
        type: 'Get',
        url: root + "/api/ManagerApi/GetManagerDB",
        data: null,  //------------------------參數丟入
        success: function (data) {
            var jobDataTable = $("#NewBoardsEditTable").dataTable();
            jobDataTable.fnClearTable();
            jobDataTable.fnAddData(data);

        },
        error: function (jqXHR, textStatus, errorThrown) {
            JL().fatal("【" + textStatus + "】:" + jqXHR.responseText + " ==> " + errorThrown);
        },
    });
}

//編輯資料列
function editFun(Guid) {

    //顯示KendoUi 編輯頁面
    $("#newwindow").show();
    kenouiEditFormInit(Guid);
}

//新增資料列
function _Addfun() {
    //顯示KendoUi 新增頁面
    $("#newwindow").show();
    kenouiAddFormInit();//不帶值
}

function deleteFun(guid, irow) {

    var QueryParam = {
        'Guid': guid, //--該筆資料的guid
    };

    //------攜帶參數
    $.ajax({
        type: 'POST',
        url: root + "/api/ManagerApi/Get_DeleteData",
        data: QueryParam,  //------------------------參數丟入
        success: function (data) {

            alert(data);
            var jobDataTable = $("#NewBoardsEditTable").dataTable();
            jobDataTable.fnDeleteRow(irow);

        },
        error: function (jqXHR, textStatus, errorThrown) {
            JL().fatal("【" + textStatus + "】:" + jqXHR.responseText + " ==> " + errorThrown);
        },
    });
}

function InitDatatable() {
    $('#NewBoardsEditTable').dataTable({
        "data": NewBoardsEditGetDB(),
        "pageLength": 50,//每個分頁筆數
        'bPaginate': true,
        "aLengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "bLengthChange": true,//選擇查詢筆數
        "bFilter": true, //------打開搜尋 
        "bDestroy": true,//-----因為在同個頁面 所以每當載入時需要重新啟動資源
        "aoColumns": [
        { "mDataProp": "Guid" },
        { "mDataProp": "Kind" },
        { "mDataProp": "Title" },
        { "mDataProp": "Message" },
        { "mDataProp": "Datetime" },
        { "mDataProp": "Note" },
        { "mDataProp": "ShowInfom" },
        {
            "mDataProp": "WorkItem",
            "fnCreatedCell": function (nTd, sData, oData, iRow, iCol) {

                $(nTd).html("<a href='javascript:void(0);' " +
                "onclick='editFun(\"" + oData.Guid + "\")'>編輯</a>&nbsp;&nbsp;")
                    .append("<a href='javascript:void(0);' onclick='deleteFun(\"" + oData.Guid + "\",\"" + iRow + "\")'>删除</a>");

            }
        },
        ],
        "fnCreatedRow": function (nRow, aData, iDataIndex) {
        },
        "order": [[4, "desc"]],//預設排序欄位 以第0個 colume 排序 asc
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