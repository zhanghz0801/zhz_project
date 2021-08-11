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
    public class MemberInfoDal
    {
        public List<MemberInfo> GetList(MemberInfo mi)
        {
            string sql = "select mi.*,mti.mTitle,mti.mDiscount" +
                " from memberinfo mi" +
                " inner join membertypeinfo mti" +
                " on mi.mtypeid=mti.mid " +
                "  where mi.mIsDelete=0";
            List<SQLiteParameter> listP = new List<SQLiteParameter>();
            if (mi.Mid>0)
            {
                sql += " and mi.mid=@mid";
                listP.Add(new SQLiteParameter("@mid", +mi.Mid));
            }

            if (!string.IsNullOrEmpty(mi.MName))
            {
                sql += " and mi.mname like @name";
                listP.Add(new SQLiteParameter("@name", "%" + mi.MName + "%"));
            }
            if (!string.IsNullOrEmpty(mi.MPhone))
            {
                sql += " and mi.mphone like @phone";
                listP.Add(new SQLiteParameter("@phone", "%" + mi.MPhone + "%"));
            }
            DataTable dt = SqliteHelper.GetList(sql,listP.ToArray());

            List<MemberInfo> list = new List<MemberInfo>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MemberInfo()
                {
                    Mid = Convert.ToInt32(row["mid"]),
                    MName = row["mname"].ToString(),
                    MMoney = Convert.ToDecimal(row["mmoney"]),
                    MPhone = row["mphone"].ToString(),
                    MTypeId = Convert.ToInt32(row["mtypeid"]),
                    TypeTitle = row["mtitle"].ToString(),
                    TypeDiscount= Convert.ToDecimal(row["mDiscount"])
                });
            }
            return list;
        }

        public int Insert(MemberInfo mi)
        {
            string sql = "insert into memberinfo(mtypeid,mname,mphone,mmoney,misDelete) values (@tid,@name,@phone,@money,0)";
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@tid",mi.MTypeId),
                new SQLiteParameter("@name",mi.MName),
                new SQLiteParameter("@phone",mi.MPhone),
                new SQLiteParameter("@money",mi.MMoney)
            };

            return SqliteHelper.ExecuteNonQuery(sql, ps);
        
        }

        public int Update(MemberInfo mi)
        {
            string sql = "update memberinfo set mname=@name,mphone=@phone,mmoney=@money,mtypeid=@tid where mid=@id";

            SQLiteParameter[] ps = {
                new SQLiteParameter("@name",mi.MName),
                new SQLiteParameter("@phone",mi.MPhone),
                new SQLiteParameter("@money",mi.MMoney),
                new SQLiteParameter("@tid",mi.MTypeId),
                new SQLiteParameter("@id",mi.Mid),
            };

            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public int Delete(int id)
        {
            string sql = "update memberinfo set mIsDelete=1 where mid=@id";

            SQLiteParameter p = new SQLiteParameter("@id", id);
           
            return SqliteHelper.ExecuteNonQuery(sql, p);
        }
    }
}
