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
    public partial class MemberInfoList : Form
    {
        //将构造方法变为私有
        public MemberInfoList()
        {
            InitializeComponent();
        }
        //通过指定的方式来创建窗体对象
        private static MemberInfoList mil;
        public static MemberInfoList Create()
        {
            //判断是否不存在
            if (mil == null)
            {
                //新建
                mil = new MemberInfoList();
            }
            //返回对象
            return mil;
        }
        MemberInfoBll miBll = new MemberInfoBll();

        private void MemberInfoList_Load(object sender, EventArgs e)
        {
            LoadList();
            LoadTypeList();
        }
        private void LoadList()
        {
            MemberInfo mi = new MemberInfo();
            mi.MName = txtNameSearch.Text;
            mi.MPhone = txtPhoneSearch.Text;
            dgvList.AutoGenerateColumns = false;//取消自动生成列
            dgvList.DataSource = miBll.GetList(mi);
        }

        private void LoadTypeList()
        {
            //获取会员分类的对象，查询会员分类信息
            MemberTypeInfoBll mtiBll = new MemberTypeInfoBll();
            List<MemberTypeInfo> list = mtiBll.GetList();
            //将会员信息绑定到控件上
            ddlType.DisplayMember = "MTitle";//显示的值
            ddlType.ValueMember = "MId";//获取的值
            ddlType.DataSource = list;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MemberInfo mi = new MemberInfo();
            mi.MName = txtNameAdd.Text;
            mi.MMoney = Convert.ToDecimal(txtMoney.Text);
            mi.MPhone = txtPhoneAdd.Text;
            mi.MTypeId = Convert.ToInt32(ddlType.SelectedValue);
           
            if (btnSave.Text.Equals("添加"))
            {
                //添加逻辑
                if (miBll.Add(mi))
                {
                    btnCancel_Click(null, null);
                    LoadList();
                }
                else
                {
                    MessageBox.Show("添加失败，请稍后再试");
                }
            }
            else
            {
                //修改逻辑
                mi.Mid = Convert.ToInt32(txtId.Text);
                if (miBll.Edit(mi))
                {
                    btnCancel_Click(null, null);
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败，请稍后再试");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtNameAdd.Text = "";
            txtMoney.Text = "";
            txtPhoneAdd.Text = "";
            ddlType.SelectedIndex = 0;
            btnSave.Text = "添加";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //获取双击的单元格所在的行
            var row = dgvList.Rows[e.RowIndex];
            //将行中的数据显示到控件中
            txtId.Text = row.Cells[0].Value.ToString();
            txtNameAdd.Text = row.Cells[1].Value.ToString();
            ddlType.Text = row.Cells[2].Value.ToString();
            txtPhoneAdd.Text = row.Cells[3].Value.ToString();
            txtMoney.Text = row.Cells[4].Value.ToString();
            
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var rows = dgvList.SelectedRows;
            if (rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    int id = int.Parse(rows[0].Cells[0].Value.ToString());
                    if (miBll.Remove(id))
                    {
                        LoadList();
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择");
            }
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtNameAdd.Text = "";
            txtPhoneAdd.Text = "";

            LoadList();
        }

        private void txtNameSearch_Leave(object sender, EventArgs e)
        {
            LoadList();
        }

        private void txtPhoneSearch_Leave(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            MemberTypeInfoList miList = new MemberTypeInfoList();
            miList.UpdateTypeEvent += UpdateType;
            miList.Show();
        }

        private void MemberInfoList_FormClosing(object sender, FormClosingEventArgs e)
        {
            mil = null;
        }

        private void UpdateType()
        {
            LoadTypeList();
            LoadList();
        }
    }
}
