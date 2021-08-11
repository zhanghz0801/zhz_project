using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace Dal
{
    public class ManagerInfoDal
    {
        public List<ManagerInfo> GetList(ManagerInfo mi)
        {
            //构造集合对象
            List<ManagerInfo> list = new List<ManagerInfo>();
            //构造sql语句
            string sql = "select * from managerinfo";
            //SQLiteParameter[] ps = new SQLiteParameter[2];
            List<SQLiteParameter> listP = new List<SQLiteParameter>();
            //拼接查询条件
            if (mi != null)
            {
                sql += " where mname=@name and mpwd=@pwd";
                //ps[0] = new SQLiteParameter("@name", mi.MName);
                //ps[1] = new SQLiteParameter("@pwd", Md5Helper.GetMd5(mi.MPwd)); 
                listP.Add(new SQLiteParameter("@name", mi.MName));
                listP.Add(new SQLiteParameter("@pwd", Md5Helper.GetMd5(mi.MPwd)));
            }
        
            //执行查询，获取数据
            DataTable table=SqliteHelper.GetList(sql,listP.ToArray());           
           
            //遍历数据表中的行,将数据转存到集合中
            foreach(DataRow row in table.Rows)
            {
                list.Add(new ManagerInfo()
                {
                    Mid = Convert.ToInt32(row["mid"]),
                    MName = row["mname"].ToString(),
                    MPwd = row["mpwd"].ToString(),
                    MType = Convert.ToInt32(row["mtype"])
                });
            }
            return list;
        }

        public int Insert(ManagerInfo mi)
        {
            string sql = "insert into managerinfo(mname,mpwd,mtype) values(@name,@pwd,@type)";

            //数组的初始化器
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@name",mi.MName),
                new SQLiteParameter("@pwd", Md5Helper.GetMd5( mi.MPwd)),
                new SQLiteParameter("@type",mi.MType),
            };
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public int DeleteById(int id)
        {
            string sql = "delete from managerinfo where mid=@id";

            SQLiteParameter p = new SQLiteParameter("@id",id);

            return SqliteHelper.ExecuteNonQuery(sql,p);

        }

        public int Update(ManagerInfo mi)
        {
            List<SQLiteParameter> list = new List<SQLiteParameter>();
            string sql = "update managerinfo set mname=@name,";
            list.Add(new SQLiteParameter("@name", mi.MName));
            if (!mi.MPwd.Equals("******"))
            {
                sql += "mpwd=@pwd,";
                list.Add(new SQLiteParameter("@pwd", Md5Helper.GetMd5( mi.MPwd)));
            }
            sql+="mtype=@type where mid=@id";

            list.Add(new SQLiteParameter("@type", mi.MType));
            list.Add(new SQLiteParameter("@id", mi.Mid));

            return SqliteHelper.ExecuteNonQuery(sql, list.ToArray());//需要将list集合转换成数组

        }
    }
}
