var root;
$(function () {
    //功能一：讀取本地網址 : 目的是為了API可以撈取到資料
    root = "http://" + location.host;
    //if (location.host.indexOf("localhost") == -1) {
    //    root = "http://" + location.host + "/" + location.pathname.split('/')[1];
    //}
    //功能二: 讓頁面上呈現當前連線狀態
    if (navigator.onLine == true) {
        document.getElementById("LinkStatus").innerHTML = "Online";
    } else {
        document.getElementById("LinkStatus").innerHTML = "Offline";
    }
    //功能三：menu 選單展開位置
    //$("li").removeClass("active treeview");
    //$("#" + location.pathname.split('/')[1]).addClass("active treeview");

    function stripTrailingSlash(str) {
        if (str.substr(-1) == '/') {
            return str.substr(0, str.length - 1);
        }
        return str;
    }

    var url = window.location.pathname;
    var activePage = stripTrailingSlash(url);

    $('.treeview-menu li a').each(function () {
        var currentPage = stripTrailingSlash($(this).attr('href'));

        if (activePage == currentPage) {
            $(this).parent().addClass('active');
        }
    });

});

//每個列印按鈕使用到的功能
function PrintDataTable(TableID) {
    bdhtml = window.document.body.outerHTML;

    //基本畫面模式
    sprnstr = "<!--startprint-->";

    eprnstr = "<!--endprint-->";

    prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);

    prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));


    //--以下是使用到的資料
    prnhtml = document.getElementById(TableID).outerHTML;

    //資料塞進
    window.document.body.innerHTML = prnhtml;

    //列印吧
    window.print();

}

//每個下載按鈕使用到的功能
function SaveCSV(TableID) {
    var downloadLink = document.createElement("a");
    //下載的.csv 檔案名稱
    downloadLink.download = TableID + ".csv";
    //預設的內容 ※當檔案名稱無法辨識時
    downloadLink.innerHTML = "Download File";

    //===資料內容的組成===

    //先取得要存成CSV檔案的表格
    var table1 = document.getElementById(TableID);

    //設定一個字串變數
    var CombineCsvString = "";

    //將該表格的每個欄位中的字逐一放入
    for (var i = 0; i < table1.rows.length; i++) {
        for (var j = 0; j < table1.rows[i].cells.length; j++) {
            //放入到字串中 並且如果是到該行最後一筆資料要加入\n換行 否則 加入, 分隔
            CombineCsvString += (table1.rows[i].cells[j].innerHTML + (j == table1.rows[i].cells.length - 1 ? "\n" : ","));
        }
    }

    //將字串 存成CSV code
    var code = encodeURIComponent(CombineCsvString);

    //將資料設定內容
    if (window.webkitURL != null) {
        //IE 11以上
        //Windows系統的格式
        if (navigator.appVersion.indexOf("Win") == -1) {
            downloadLink.href = "data:application/csv;charset=utf-8," + code;
        }
        else {
            downloadLink.href = "data:application/csv;charset=utf-8,%EF%BB%BF" + code;
        }
        downloadLink.click();
    } else {
        //IE 11以下
        SaveAs("\uFEFF" + CombineCsvString, TableID, "csv", "application/octet-stream");
    }


}

//存檔
function SaveAs(Data, FileName, FileType, MineType) {
    var blob = new Blob([Data], { type: MineType });
    saveAs(blob, FileName + "." + FileType);
}
