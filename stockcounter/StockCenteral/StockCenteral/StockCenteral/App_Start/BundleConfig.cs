using System.Web;
using System.Web.Optimization;

namespace StockCenteral
{
    public class BundleConfig
    {
        // 如需「搭配」的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(                        "~/Scripts/jquery.validate*"));

            //Login JS
            bundles.Add(new ScriptBundle("~/bundles/Login").Include(
                    "~/Scripts/jquery.icheck.min.js"
                    ));


            #region  基本的所有需要用到的css
            bundles.Add(new StyleBundle("~/Content/BaseCss").Include(
                     "~/Content/bootstrap.min.css",
                     "~/Scripts/plugins/jvectormap/jquery-jvectormap-1.2.2.css", //--------------------------提供Jquery的座標矢量功能
                     "~/Scripts/plugins/datatables/dataTables.bootstrap.css", //----DataTable 我們使用的表格插件功能
                     "~/Scripts/dist/css/AdminLTE.min.css",  //----------------------------------------------本網站使用到的Twitter的模板
                     "~/Scripts/dist/css/skins/_all-skins.min.css",//-----------------------------------------這個與AdminLTE.min.css 相關聯，兩個CSS需要搭配使用
                     "~/Content/css/InputSelectDropdownListTool.css",//------------------------------DropDownList與Input結合的Css
                     "~/Scripts/plugins/datatables/dataTables.bootstrap.css"
                     ));
            //Login Css
            bundles.Add(new StyleBundle("~/Content/LoginCss").Include(
                "~/Content/bootstrap.min.css",
                "~/Scripts/dist/css/AdminLTE.min.css",
                "~/Scripts/plugins/iCheck/square/blue.css"
                ));

            #endregion


            #region  基本的所有需要用到的js

            bundles.Add(new ScriptBundle("~/bundles/BaseJquery").Include(
                         "~/Scripts/plugins/jQuery/jQuery-2.1.4.min.js",
                         "~/Scripts/bootstrap.min.js", //--------------------------------BootStrap3.3.2 版本
                         "~/Scripts/dist/js/app.min.js",//-------------------------------AdminLTE 模組的JS使用，本網站必備
                         "~/Scripts/plugins/sparkline/jquery.sparkline.min.js",//--------繪畫迷你圖表 ->波譜圖、圖表顏色自動變換等功能
                         "~/Scripts/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js", //矢量座標功能繪製
                         "~/Scripts/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",//矢量座標功能繪製
                         "~/Scripts/plugins/slimScroll/jquery.slimscroll.min.js",//------Jquery滾動調顏色 與功能
                         "~/Scripts/dist/js/demo.js", //-----------------------------------啟動AdminLTe的顯示功能
                         "~/Scripts/plugins/datatables/jquery.dataTables.min.js",//-------DataTable
                         "~/Scripts/plugins/datatables/dataTables.bootstrap.min.js", //-----DataTable
                         "~/Scripts/self/BaseFunction.js"//--------------------------------root 網址根節點位置
                         ));


            #endregion

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好實際執行時，請使用 http://modernizr.com 上的建置工具，只選擇您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/LoginCss").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/admin-lte/css/AdminLTE.min.css",
                "~/Content/iCheck/square/blue.css",
                "~/Content/login.css"
                ));


            #region 每個頁面獨立的js 功能
            //Datatable - 保管場所清冊

            bundles.Add(new ScriptBundle("~/bundles/PageToSingleStock").Include(
                   "~/Scripts/Self/SingleStock/SingleStock.js",
                   "~/Scripts/Self/SingleStock/SingleStockChart.js",
                   "~/Scripts/Self/SingleStock/SingleStockExtentionKindChart.js",
                   "~/Scripts/Self/SingleStock/SingleStockKindChart.js",
                   "~/Scripts/Self/SingleStock/SingleStockPeopleChart.js",
                   "~/Scripts/Self/SingleStock/SingleStockPeopleExtentionKindChart.js",
                   "~/Scripts/Self/SingleStock/SingleStockPeopleKindChart.js",
                   "~/Scripts/plugins/datatables/jquery.dataTables.min.js",
                   "~/Scripts/plugins/datatables/dataTables.bootstrap.min.js",
                   "~/Scripts/jquery-ui-1.11.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/PageToLeaderboard").Include(
                 "~/Scripts/self/Leardboard/Leardboard.js",
                 "~/Scripts/js/themes/ThemesHightcharts_Sparkline_Grid.js",
                 "~/Scripts/self/Leardboard/Leardboard_Sparkline.js",
                 "~/Scripts/self/Leardboard/LeaderBoardDataTable.js",
                 "~/Scripts/jquery.blockUI.js",
                 "~/Scripts/plugins/datatables/jquery.dataTables.min.js",
                 "~/Scripts/plugins/datatables/dataTables.bootstrap.min.js",
                 "~/Scripts/jquery-ui-1.11.4.min.js"));


            #endregion



            // 將 EnableOptimizations 設為 false 以進行偵錯。如需詳細資訊，
            // 請造訪 http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
