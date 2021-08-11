using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public static class SqliteHelper
    {
        public static string connStr = ConfigurationManager.ConnectionStrings["cater"].ConnectionString;

        public static DataTable GetList(string sql,params SQLiteParameter[] ps)
        {
            //构造连接对象
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {         
                //构造桥接器对象
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql,conn);
                //数据表对象
                adapter.SelectCommand.Parameters.AddRange(ps);
                DataTable table = new DataTable();
                //将数据存到table中 
                adapter.Fill(table);
                //返回数据表
                return table;
            }
        }

        public static int ExecuteNonQuery(string sql,params SQLiteParameter[] ps)
        {
            using(SQLiteConnection conn=new SQLiteConnection(connStr))
            {
                SQLiteCommand cmd = new SQLiteCommand(sql,conn);
                cmd.Parameters.AddRange(ps);//对数据库使用参数进行赋值
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string sql, params SQLiteParameter[] ps)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddRange(ps);//对数据库使用参数进行赋值
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}
