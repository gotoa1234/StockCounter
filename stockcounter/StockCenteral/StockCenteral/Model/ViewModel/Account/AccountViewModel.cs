using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.Account
{
    /// <summary>
    /// 首頁登入使用的帳號密碼ViewModel
    /// </summary>
    public class AccountViewModel
    {
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [Required]
        [Display(Name = "帳號")]
        [StringLength(100, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 1)]
        public string Username { get; set; }

        /// <summary>
        /// 登入帳號
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        [StringLength(100, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 1)]
        public string Password { get; set; }

        

    }
}
