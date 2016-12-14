using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.POCO.TDDC_CounterController
{
    [Table("Dividend_1")]//--資料表名稱
    public class TDDC_CounterController
    {
        [Display(Name = "時間")]//----------------顯示的名稱
        [StringLength(50)]//------------------------------長度 20
        [Column(TypeName = "NVarchar", Order = 0)]//------型態
        [Key]
        public string Time { get; set; } //-------------存取名稱 (要與資料庫相同名稱才能對應)
        [Display(Name = "帳號")]//----------------顯示的名稱
        [StringLength(50)]//------------------------------長度 20
        [Column(TypeName = "NVarchar", Order = 1)]//------型態
        [Key]
        public string Name { get; set; } //-------------存取名稱 (要與資料庫相同名稱才能對應)

        [Display(Name = "金錢")]//----------------顯示的名稱
        [StringLength(18)]//------------------------------長度 20
        [Column(TypeName = "int")]//------型態
        [Key]
        public int Dividend { get; set; } //-------------存取名稱 (要與資料庫相同名稱才能對應)
    }
}
