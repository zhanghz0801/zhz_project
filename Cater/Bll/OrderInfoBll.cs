using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class OrderInfoBll
    {
        OrderInfoDal oiDal = new OrderInfoDal();

        public bool KaiDan(int tableId)
        {
            return oiDal.KaiDan(tableId) > 0;
        }

        public int GetOidByTid(int tid)
        {
            return oiDal.GetOidByTid(tid);
        }

        public decimal GetMoneyByTid(int tid)
        {
            return oiDal.GetMoneyByTid(tid); 
        }

        public bool DianCai(int orderId, int dishId)
        {
            return oiDal.DianCai(orderId, dishId) > 0;
        }

        public List<OrderDetailInfo> GetOrderDetail(int orderId)
        {
            return oiDal.GetOrderDetail(orderId);
        }

        public bool UpdateDishCount(int oid, int count)
        {
            return oiDal.UpdateDishCount(oid,count)>0;
        }

        public bool DeleteDish(int oid)
        {
            return oiDal.DeleteDish(oid) > 0;
        }

        public bool XiaDan(int orderId, decimal totalMoney)
        {
            return oiDal.XiaDan(orderId, totalMoney) > 0;
        }

        public bool JieZhang(int tableId, int memberId, decimal discount, decimal payMoney)
        {
            return oiDal.JieZhang(tableId, memberId, discount, payMoney) > 0;
        }
    }
}
