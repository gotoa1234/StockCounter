
// 使用工具說明：使用javascript => kendo Ui plugin 
// 用途：當使用者登入後 會依照權限顯示該帳號可以登入的檔案文件上傳系統
// 製作日期： 2016/03/08

//編輯頁面的呼叫
function kenouiEditFormInit(Guid)
{
    var kedoform = $("#newwindow").data("kendoWindow");
    kedoform.setOptions({
        contents: root + "/Manager/KendoManagerEditForm?Guid=" + Guid,
    });
    //重新Refresh 編輯內容
    kedoform.refresh({ url: root + "/Manager/KendoManagerEditForm?Guid=" + Guid}).open();

};


//新增頁面的呼叫
function kenouiAddFormInit() {
    var kedoform = $("#newwindow").data("kendoWindow");
    kedoform.setOptions({
        contents: root + "/Manager/KendoManagerAddForm",
    });
    //重新Refresh 編輯內容
    kedoform.refresh({ url: root + "/Manager/KendoManagerAddForm?Guid=null" }).open();

};

//初始化資訊
function KedoInit()
{  


    $("#newwindow").kendoWindow({
        title: "更新欄位資料",
        width: '50%',
        height: '100%',
        position: { top: 0, left: 0 },
        actions: ["minimize", "Maximize", "close"],
        close: function (e) {
            $("#newwindow").hide();
        },
        visible:false,
       // content: root + "/Manager/KendoManagerEditForm?Guid=" + Guid
    });
    
}
