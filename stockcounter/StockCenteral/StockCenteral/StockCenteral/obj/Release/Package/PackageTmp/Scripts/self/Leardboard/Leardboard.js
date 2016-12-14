$(function () {

    DatetimePicker_Initinal();
});
//Datetimepicker 初始化
function DatetimePicker_Initinal() {
    // last yaer
    var dateNow = new Date();
    var lastyear = dateNow.getTime() - 1000 * 60 * 60 * 24 * 42;
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

//顯示button載入
function showBlockUI() {
    $.blockUI({
        message: '<table><tr><td valign="middle" style="height:50px" class="main"><i class="fa fa-spinner fa-pulse fa-2x fa-fw"></i> 籌碼分析中,請稍候...</td></tr></table>',
        css: {
            width: '250px',
            height: '50px'
        }
    });
}
