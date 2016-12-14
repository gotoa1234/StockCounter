using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.Account
{
    /// <summary>
    /// 註冊個人資訊
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// 帳號
        /// </summary>
        [Required]
        [Display(Name = "登入帳號")]
        [StringLength(40, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 1)]
        public string Account { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "登入密碼")]
        [StringLength(40, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 1)]
        public string Password { get; set; }
        //public int UserLevel { get; set; }
        //public System.Guid GUID { get; set; }
        /// <summary>
        /// 使用者名字
        /// </summary>
        [Required]
        [StringLength(40, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 1)]
        [Display(Name = "使用者名字")]
        public string UserName { get; set; }
        /// <summary>
        /// 使用者Mail
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(40)]
        [Display(Name = "使用者Mail")]
        public string UserMail { get; set; }
        /// <summary>
        /// 家用電話
        /// </summary>
        [StringLength(40)]
        [Display(Name = "家用電話")]
        public string UserPhone { get; set; }
        /// <summary>
        /// 手機電話
        /// </summary>
        [StringLength(40)]
        [Display(Name = "手機電話")]
        public string UserCellPhone { get; set; }
        /// <summary>
        /// 家裡住址
        /// </summary>
        [StringLength(40)]
        [Display(Name = "家裡住址")]
        public string UserAddress { get; set; }
        //public System.DateTime UserRegisterDate { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [StringLength(40)]
        [Compare("Password", ErrorMessage = "密碼和確認密碼不相符。")]
        public string ConfirmPassword { get; set; }
    }
}
