using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.NewsBoard
{
    /// <summary>
    /// 最新消息的留言板資訊
    /// </summary>
    public class NewsBoardViewModel
    {
        public System.Guid Guid { get; set; }


        [Required]
        [Display(Name = "種類")]
        [StringLength(50, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 1)]
        public string Kind { get; set; }

        [Display(Name = "標題")]
        [StringLength(100, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "訊息")]
        [StringLength(800, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 1)]
        [Required]
        public string Message { get; set; }

        [Display(Name = "最後更新時間")]
        [DataType(DataType.DateTime, ErrorMessage = "請輸入正確的日期時間")]
        [Required]
        public System.DateTime Datetime { get; set; }

        [Display(Name = "是否顯示資訊")]
        [Required]
        public bool ShowInfom { get; set; }

        [Display(Name = "備註")]
        [StringLength(500, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 1)]
        [Required]
        public string Note { get; set; }

    }
}
