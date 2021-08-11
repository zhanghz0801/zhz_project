using Bll;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class OrderInfoList : Form
    {
    
        public OrderInfoList()
        {
            InitializeComponent();
        }
        private OrderInfoBll oiBll = new OrderInfoBll();
        private int orderId;

        private void OrderInfoList_Load(object sender, EventArgs e)
        {
            //获取传递过来的餐桌编号
            int tableId = Convert.ToInt32(Tag);
            //根据餐桌编号,查询订单编号
            orderId = oiBll.GetOidByTid(tableId);
            //加载所有的菜品信息
            LoadDishTypeInfo();
            LoadDishInfo();
            //加载所有已经菜品
            LoadOrderList();
        }

        private void LoadDishInfo()
        {
            DishInfo di = new DishInfo();
            di.DChar = txtTitle.Text;
            di.DTypeId = Convert.ToInt32(ddlType.SelectedValue);

            DishInfoBll diBll = new DishInfoBll();
            dgvAllDish.AutoGenerateColumns = false;
            dgvAllDish.DataSource = diBll.GetList(di);
        }

        private void LoadDishTypeInfo()
        {
            DishTypeInfoBll dtiBll = new DishTypeInfoBll();
            var list = dtiBll.GetList();

            list.Insert(0, new DishTypeInfo()
            {
                Did = 0,
                DTitle = "全部"
            });

            ddlType.DisplayMember = "DTitle";
            ddlType.ValueMember = "Did";
            ddlType.DataSource = list;
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            LoadDishInfo();
        }

        private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDishInfo();
        }

        private void dgvAllDish_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dishId = Convert.ToInt32(dgvAllDish.Rows[e.RowIndex].Cells[0].Value);
            if (oiBll.DianCai(orderId, dishId))
            {
                LoadOrderList();
            }
        }

        private void LoadOrderList()
        {
            dgvOrderDetail.AutoGenerateColumns = false;
            dgvOrderDetail.DataSource = oiBll.GetOrderDetail(orderId);

            GetOrderMoney();
        }

        private void dgvOrderDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvOrderDetail.Rows[e.RowIndex];
            int oid = Convert.ToInt32(row.Cells[0].Value);
            int count = Convert.ToInt32(row.Cells[e.ColumnIndex].Value);

            if (oiBll.UpdateDishCount(oid, count))
            {
                GetOrderMoney();
            }

        }

        private void GetOrderMoney()
        {
            decimal total = 0;
            var rows = dgvOrderDetail.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                int count = Convert.ToInt32(rows[i].Cells[2].Value);
                decimal price = Convert.ToDecimal(rows[i].Cells[3].Value);
                total += count * price;
            }
            lblMoney.Text = total.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var row = dgvOrderDetail.SelectedRows[0];
            int oid = Convert.ToInt32(row.Cells[0].Value);
            if (oiBll.DeleteDish(oid))
            {
                LoadOrderList();
            }
        }

       

        private void OrderInfoList_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            decimal totalMoney = Convert.ToDecimal(lblMoney.Text);

            if (oiBll.XiaDan(orderId, totalMoney))
            {
                this.Close();
            }
        }
    }
}
