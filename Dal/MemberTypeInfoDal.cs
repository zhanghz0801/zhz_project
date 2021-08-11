using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Dal
{
    public class MemberTypeInfoDal
    {
        public List<MemberTypeInfo> GetList()
        {
            string sql = "select * from membertypeinfo where mIsDelete=0";
            DataTable dt = SqliteHelper.GetList(sql);
            List<MemberTypeInfo> list = new List<MemberTypeInfo>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MemberTypeInfo()
                {
                    Mid = Convert.ToInt32(row["mid"]),
                    MTitle = row["mtitle"].ToString(),
                    MDiscount = Convert.ToDecimal(row["mdiscount"])                
                });
            }
            return list;
        }

        public int Insert(MemberTypeInfo mti)
        {
            string sql = "insert into membertypeinfo(mtitle,mdiscount,mIsDelete) values(@title,@discount,0)";

            SQLiteParameter[] ps = {
                new SQLiteParameter("@title",mti.MTitle),
                new SQLiteParameter("@discount",mti.MDiscount)
            };
            //执行sql语句，并返回受影响的行数
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public int Update(MemberTypeInfo mti)
        {
            string sql = "update membertypeinfo set mtitle=@title,mdiscount=@discount where mid=@id";

            SQLiteParameter[] ps = {
                new SQLiteParameter("@title",mti.MTitle),
                new SQLiteParameter("@discount",mti.MDiscount),
                new SQLiteParameter("@id",mti.Mid)
            };

            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public int Delete(int id)
        {
            //逻辑删除，将mIsDelete删除标记改为1表示删除
            string sql = "update membertypeinfo set mIsDelete=1 where mid=@id";

            SQLiteParameter p = new SQLiteParameter("@id", id);
    
            return SqliteHelper.ExecuteNonQuery(sql, p);
        }

    }
}
