using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Dal
{
    public class OrderInfoDal
    {
        //开单
        public int KaiDan(int tableId)
        {
            string sql = "insert into orderinfo(odate,ispay,tableId) values(datetime('now','localtime'),0,@tid);" +
                "update tableinfo set tIsFree=0 where tid=@tid";

            SQLiteParameter p = new SQLiteParameter("@tid", tableId);

            return SqliteHelper.ExecuteNonQuery(sql, p);
        }

        public int GetOidByTid(int tid)
        {
            string sql = "select oid from orderinfo where tableid=@tid and ispay=0";

            SQLiteParameter p = new SQLiteParameter("@tid", tid);

            return Convert.ToInt32(SqliteHelper.ExecuteScalar(sql, p));

        }

        public decimal GetMoneyByTid(int tid)
        {
            string sql = "select omoney from orderinfo where tableId=@tid and ispay=0";
            SQLiteParameter p = new SQLiteParameter("@tid", tid);
            return Convert.ToDecimal(SqliteHelper.ExecuteScalar(sql, p)); 
        }

        public int DianCai(int orderId, int dishId)
        {
            string sql = "select count(*) from orderDetailInfo where orderid=@oid and dishid=@did";
             SQLiteParameter[] ps =
             {
                new SQLiteParameter("@oid", orderId),
                new SQLiteParameter("@did", dishId),
             };
            int count = Convert.ToInt32(SqliteHelper.ExecuteScalar(sql, ps));

            if (count == 0)
            {
                //当前订单中没有指定菜品，则进行添加
                sql = "insert into orderDetailinfo(orderid,dishid,count) values(@oid,@did,1)";

            }
            else
            {
                //当前订单中7已经存在此菜品，进行数量更新
                sql = "update orderDetailInfo set count=count+1 where orderid=@oid and dishid=@did";

            }
           
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public List<OrderDetailInfo> GetOrderDetail(int orderId)
        {
            string sql = "select odi.*,di.DTitle,di.DPrice" +
                " from orderDetailInfo odi" +
                " inner join dishInfo di" +
                " on odi.dishid=di.did" +
                " where odi.orderid=@oid";


            SQLiteParameter p = new SQLiteParameter("@oid", orderId);

            DataTable dt = SqliteHelper.GetList(sql, p);

            List<OrderDetailInfo> list = new List<OrderDetailInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new OrderDetailInfo()
                {
                    Oid = Convert.ToInt32(row["oid"]),
                    OrderId = orderId,
                    DishId = Convert.ToInt32(row["dishid"]),
                    Count = Convert.ToInt32(row["count"]),
                    DishTitle = row["dtitle"].ToString(),
                    DishPrice = Convert.ToDecimal(row["dprice"]),  

                });

            }

            return list;

        }

        public int UpdateDishCount(int oid,int count)
        {
            string sql = "update orderDetailInfo set count=@count where oid=@oid";

            SQLiteParameter[] ps =
             {
                new SQLiteParameter("@count", count),
                new SQLiteParameter("@oid", oid),
             };

            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public int DeleteDish(int oid)
        {
            string sql = "delete from orderDetailInfo where oid=@oid";

            SQLiteParameter p = new SQLiteParameter("@oid", oid);

            return SqliteHelper.ExecuteNonQuery(sql, p);
        }

        public int XiaDan(int orderId, decimal totalMoney)
        {
            string sql = "update orderinfo set omoney=@totalMoney where oid=@oid";
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@oid",orderId),
                new SQLiteParameter("@totalMoney",totalMoney)
            };

            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public int JieZhang(int tableId,int memberId,decimal discount,decimal payMoney)
        {

            using (SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["cater"].ConnectionString))
            {
                conn.Open();
                //开启事务
                SQLiteTransaction tran = conn.BeginTransaction();
                int counter = 0;
                try
                {
                    //创建command对象,并与事务相关联
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Transaction = tran;
                    //1、更改订单状态：ispay=1,
                    string sql = "update orderinfo set ispay=1";
                    //1.1、如果是会员，则记录下来
                    if (memberId > 0)
                    {
                        sql += ",memberId=" + memberId + ",discount=" + discount;
                    }
                    sql += " where tableId=" + tableId + " and ispay=0";
                    cmd.CommandText = sql;
                    counter += cmd.ExecuteNonQuery();

                    //2、将餐桌状态更改为1空闲
                    sql = "update tableInfo set tIsFree=1 where tid=" + tableId;
                    cmd.CommandText = sql;
                    counter += cmd.ExecuteNonQuery();

                    //3、如果使用余额结账，则更新会员余额
                    if (payMoney > 0)
                    {
                        sql = "update memberinfo set mMoney=mMoney-" + payMoney + " where mid=" + memberId;
                        cmd.CommandText = sql;
                        counter += cmd.ExecuteNonQuery();
                    }

                    //操作成功，则提交确定之前所有的操作
                    tran.Commit();
                }
                catch
                {
                    counter = 0;
                    //一旦出错，则回滚，放弃之前所有的操作
                    tran.Rollback();
                }
                return counter;
            }
        }
    }
}

