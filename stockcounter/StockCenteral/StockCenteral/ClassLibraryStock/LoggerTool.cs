using ClassLibraryStock.OriClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryStock
{
    /// <summary>
    /// 紀錄Logger到DB
    /// </summary>
    public class LoggerTool
    {
        /// <summary>
        /// 初始化Logger資料表
        /// </summary>
        public LoggerTool()
        {
            string connetionString = null;
            SqlConnection con;
            SqlCommand command;
            connetionString = ConfigurationManager.ConnectionStrings["EocConnection"].ToString();
            con = new SqlConnection(connetionString);
            string CreateTable = "CREATE TABLE Logger (Level nvarchar(10) not null , Date datetime not null, Message nvarchar(500) not null , Stack nvarchar(250))"; ;
            try
            {
                con.Open();
                command = new SqlCommand(CreateTable, con);
                command.ExecuteReader();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }

        /// <summary>
        /// 將Logger寫入DB
        /// </summary>
        /// <param name="Data"></param>
        public void LoggerTool_Add(Logger Data)
        {

            using (SqlConnection openCon = new SqlConnection(ConfigurationManager.ConnectionStrings["EocConnection"].ToString()))
            {
                string saveStaff = "INSERT into Logger (Level , Date , Message , Stack) VALUES (@level,@date,@message,@stack)";

                using (SqlCommand querySaveStaff = new SqlCommand(saveStaff))
                {
                    querySaveStaff.Connection = openCon;
                    querySaveStaff.Parameters.Add("@level", SqlDbType.NVarChar, 10).Value = Data.Level;
                    querySaveStaff.Parameters.Add("@date", SqlDbType.DateTime2, 50).Value = Data.Date;
                    querySaveStaff.Parameters.Add("@message", SqlDbType.NVarChar, 500).Value = Data.Message;
                    querySaveStaff.Parameters.Add("@stack", SqlDbType.NVarChar, 250).Value = Data.Stack;
                    openCon.Open();
                    querySaveStaff.ExecuteNonQuery();
                    openCon.Close();
                }
            }
        }

    }
}
