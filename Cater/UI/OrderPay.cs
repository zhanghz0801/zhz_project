using Bll;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class OrderPay : Form
    {
        private OrderPay()
        {
            InitializeComponent();
        }
        private static OrderPay orderPay;

        public  static OrderPay Create()
        {
            if (orderPay == null || orderPay.IsDisposed)
            {
                orderPay = new OrderPay();
            }
            return orderPay;
        }

        private OrderInfoBll oiBll = new OrderInfoBll();

        private void OrderPay_Load(object sender, EventArgs e)
        {
            gbMember.Enabled = false;
            this.Text="为【"+this.Tag+ "】餐桌结账付款";

            lblPayMoney.Text = oiBll.GetMoneyByTid(Convert.ToInt32(this.Tag)).ToString();
            lblPayMoneyDiscount.Text = lblPayMoney.Text;
         }

        private void cbkMember_CheckedChanged(object sender, EventArgs e)
        {
            //Enabled和Checked都是返回bool类型
            gbMember.Enabled = cbkMember.Checked;
            if (!cbkMember.Checked)
            {
                txtId.Text = "";
                txtPhone.Text = "";
                lblMoney.Text = "0";
                lblTypeTitle.Text = "普通会员";
                lblDiscount.Text = "1";
                lblPayMoneyDiscount.Text = lblPayMoney.Text;
                cbkMoney.Checked = false;
            }      
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            GetMemberInfo();
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            GetMemberInfo();
        }

        private void GetMemberInfo()
        {
            MemberInfo mi = new MemberInfo();
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                mi.Mid = Convert.ToInt32(txtId.Text);
            }
            mi.MPhone = txtPhone.Text;

            MemberInfoBll miBll = new MemberInfoBll();
            var list= miBll.GetList(mi);
            if (list.Count ==1)
            {
                mi = list[0];//list里就一个，所有直接拿list[0]就行
                txtId.Text = mi.Mid.ToString();
                txtPhone.Text = mi.MPhone;
                lblMoney.Text = mi.MMoney.ToString();
                lblTypeTitle.Text = mi.TypeTitle;
                lblDiscount.Text = mi.TypeDiscount.ToString();
                //根据折扣，更新应付折扣金额的值
                decimal payMoneyDiscount = mi.TypeDiscount * Convert.ToDecimal(lblPayMoney.Text);
                lblPayMoneyDiscount.Text = payMoneyDiscount.ToString();
            }
            else
            {
                MessageBox.Show("会员信息有误，请核对！");
            }
        }

        public event Action SetTableFreeEvent;
        private void btnOrderPay_Click(object sender, EventArgs e)
        {
            //已知数据:餐桌编号tabldId
            //需要的数据:会员编号，折扣，折扣金额
            //获取餐桌编号
            int tableId = Convert.ToInt32(this.Tag);
            //如果使用会员，则获取会员编号及折扣，否则编号为0
            //会员编号为0时，表示不使用会员信息
            int memberId = 0;
            decimal discount = 0;
            if(!string.IsNullOrEmpty(txtId.Text))
            {
                memberId = int.Parse(txtId.Text);
                discount = Convert.ToDecimal(lblDiscount.Text);
            }
            //结账金额，如果不使用余额结账，则payMoney为0
            decimal payMoney = 0;
            if (cbkMoney.Checked)
            {
                //如果使用余额结账，则判断。
                decimal totalMoney = decimal.Parse(lblMoney.Text);
                decimal payDiscount = decimal.Parse(lblPayMoneyDiscount.Text);
                if (totalMoney > payDiscount)
                {
                    //余额大于就付，则传递应付
                    payMoney = payDiscount;
                }
                else
                {
                    //如果余额较小，则传递余额，使会员余额不会小于0
                    payMoney = totalMoney;
                }
            }
            if(oiBll.JieZhang(tableId, memberId, discount, payMoney))
            {
                //4、更改结账的餐桌的状态图片
                SetTableFreeEvent();
                this.Close();
            }
            else
            {
                MessageBox.Show("error");
            }
        }
    }
}
