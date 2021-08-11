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
    public class DishInfoDal
    {
        public List<DishInfo> GetList(DishInfo di)
        {
            string sql = "select di.*,dti.dtitle as typetitle from dishinfo di" +
                " inner join dishtypeinfo dti" +
                " on di.dtypeid=dti.did" +
                " where di.dIsDelete=0";
            List<SQLiteParameter> listP = new List<SQLiteParameter>();
            if (!string.IsNullOrEmpty(di.DTitle))
            {
                sql += " and di.dtitle like @title";
                listP.Add(new SQLiteParameter("@title", "%" + di.DTitle + "%"));
            }
            if (di.DTypeId > 0)
            {
                sql += " and di.dtypeid=@tid";
                listP.Add(new SQLiteParameter("@tid", di.DTypeId));
            }
            if (!string.IsNullOrEmpty(di.DChar))
            {
                sql += " and di.dchar like @dchar";
                listP.Add(new SQLiteParameter("@dchar", "%"+di.DChar+ "%"));
            }
             
            DataTable dt = SqliteHelper.GetList(sql,listP.ToArray());

            List<DishInfo> list = new List<DishInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DishInfo()
                {
                    Did = Convert.ToInt32(row["did"]),
                    DTitle = row["dtitle"].ToString(),
                    DChar= row["dchar"].ToString(),
                    DPrice = Convert.ToDecimal(row["dprice"]),
                    DTypeId  = Convert.ToInt32(row["dtypeid"]),
                    TypeTitle = row["typetitle"].ToString(),
                });
            }
            return list;
        }

        public int Insert(DishInfo di)
        {
            string sql = "insert into dishinfo(dtitle,dprice,dtypeid,dchar,dIsDelete) values (@title,@price,@tid,@char,0)";
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@title",di.DTitle),
                new SQLiteParameter("@price",di.DPrice),
                new SQLiteParameter("@tid",di.DTypeId),
                new SQLiteParameter("@char",di.DChar)
            };

            return SqliteHelper.ExecuteNonQuery(sql, ps);

        }

        public int Update(DishInfo di)
        {
            string sql = "update dishinfo set dtitle=@title,dprice=@price,dchar=@char,dtypeid=@tid where did=@id";

            SQLiteParameter[] ps = {
                new SQLiteParameter("@title",di.DTitle),
                new SQLiteParameter("@price",di.DPrice),
                new SQLiteParameter("@tid",di.DTypeId),
                new SQLiteParameter("@char",di.DChar),
                new SQLiteParameter("@id",di.Did)
            };

            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public int Delete(int id)
        {
            string sql = "update dishinfo set dIsDelete=1 where did=@id";

            SQLiteParameter p = new SQLiteParameter("@id", id);

            return SqliteHelper.ExecuteNonQuery(sql, p);
        }
    }
}
