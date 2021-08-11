using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class TableInfoDal
    {

        public  List<TableInfo> GetList(TableInfo ti)
        {
            string sql = "select ti.*,hi.HTitle from tableinfo ti" +
            " inner join hallinfo hi" +
            " on ti.THallId=hi.Hid" +
            " where ti.TIsDelete=0";

            List<SQLiteParameter> listP = new List<SQLiteParameter>();
            if (ti.THallId > 0)
            {
                sql += " and ti.THallId=@hid";
                listP.Add(new SQLiteParameter("@hid", ti.THallId));
            }

            if (ti.IsFreeSearch > -1)
            {
                sql += " and ti.TIsFree=@isFree ";
                listP.Add(new SQLiteParameter("@isfree", ti.IsFreeSearch));
            }
            DataTable dt = SqliteHelper.GetList(sql,listP.ToArray());

            List<TableInfo> list = new List<TableInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new TableInfo()
                {
                    Tid = Convert.ToInt32(row["tid"]),
                    THallId = Convert.ToInt32(row["thallid"]),
                    TTitle = row["ttitle"].ToString(),
                    HallTitle = row["htitle"].ToString(),
                    TIsFree = Convert.ToBoolean(row["tisFree"])
                });

            }

            return list;
        }
        
        public int Insert(TableInfo ti)
        {
            string sql = "insert into tableinfo(ttitle,thallid,tisfree,tisDelete) values(@title,@hid,@isfree,0)";

            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@title",ti.TTitle),
                new SQLiteParameter("@hid",ti.THallId),
                new SQLiteParameter("@isFree",ti.TIsFree)
            };

            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public int Update(TableInfo ti)
        {
            string sql = "update tableinfo set tTitle=@title,thallid=@hid,tisFree=@isFree where tid=@id";

            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@title",ti.TTitle),
                new SQLiteParameter("@hid",ti.THallId),
                new SQLiteParameter("@isFree",ti.TIsFree),
                new SQLiteParameter("@id",ti.Tid)
            };

            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public int Delete(int id)
        {
            string sql = "update tableinfo set tIsDelete=1 where tid=@id";

            SQLiteParameter p = new SQLiteParameter("@id", id);

            return SqliteHelper.ExecuteNonQuery(sql, p);
        }
    }
}
