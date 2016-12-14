using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockCenteral.Helpers
{
    public static class Myhelp
    {
        //https://unsplash.it/200/200?random
        /// <summary>
        /// 將傳進的width 跟 height 轉成img隨機字串圖片
        /// </summary>
        /// <param name="html">Extension Html</param>
        /// <param name="width">寬</param>
        /// <param name="height">高</param>
        /// <returns></returns>
        public static MvcHtmlString MyimageURL(this HtmlHelper html, int width, int height)
        {
            return new MvcHtmlString(string.Format("<img src='https://unsplash.it/{0}/{1}/?random' />", width, height));
            //return $"<img src=\"https://unsplash.it/{width}/{height}/?random\" alt=\"...\">";

        }
    }
}