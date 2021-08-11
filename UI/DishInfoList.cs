using Bll;
using Common;
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
    public partial class DishInfoList : Form
    {
        private DishInfoList()
        {
            InitializeComponent();
        }

        private static DishInfoList diList;

        public static DishInfoList Create()
        {
            if (diList == null||diList.IsDisposed)
            {
                diList = new DishInfoList();
            }
            return diList;
        }

        DishInfoBll diBll = new DishInfoBll();

        private void DishInfoList_Load(object sender, EventArgs e)
        {
            LoadList();
            LoadTypeList();
        }

        private void LoadList()
        {
            DishInfo di = new DishInfo();

            if (!string.IsNullOrEmpty(txtTitleSearch.Text))
            {
                //不为空时
                di.DTitle = txtTitleSearch.Text;
            }
            di.DTypeId = Convert.ToInt32(ddlTypeSearch.SelectedIndex);
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = diBll.GetList(di);
        }
        private void LoadTypeList()
        {
            //获取会员分类的对象，查询会员分类信息
            DishTypeInfoBll dtiBll = new DishTypeInfoBll();
            //绑定添加与修改的分类
            ddlTypeAdd.DisplayMember = "DTitle";//显示的值
            ddlTypeAdd.ValueMember = "DId";//获取的值
            ddlTypeAdd.DataSource = dtiBll.GetList();
            //绑定查询的分类
            var list = dtiBll.GetList();
            list.Insert(0, new DishTypeInfo()
            {
                Did = 0,
                DTitle = "全部"
             });
            ddlTypeSearch.DisplayMember = "DTitle";//显示的值
            ddlTypeSearch.ValueMember = "DId";//获取的值
            ddlTypeSearch.DataSource = list;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DishInfo di = new DishInfo();
            di.DTitle = txtTitleSave.Text;
            di.DTypeId = Convert.ToInt32(ddlTypeAdd.SelectedValue);
            di.DPrice = Convert.ToDecimal(txtPrice.Text);
            di.DChar = txtChar.Text;

            if (btnSave.Text.Equals("添加"))
            {
                //添加逻辑
                if (diBll.Add(di))
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
                di.Did = Convert.ToInt32(txtId.Text);
                if (diBll.Edit(di))
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

        private void txtTitleSave_Leave(object sender, EventArgs e)
        {
            txtChar.Text = PinyinHelper.GetPinyin(txtTitleSave.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtTitleSave.Text = "";
            txtPrice.Text = "";
            txtChar.Text = "";
            ddlTypeAdd.SelectedIndex = 0;
            btnSave.Text = "添加";
        }

       

        private void dgvList_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //获取双击的单元格所在的行
            var row = dgvList.Rows[e.RowIndex];
            //将行中的数据显示到控件中
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitleSave.Text = row.Cells[1].Value.ToString();
            ddlTypeAdd.Text = row.Cells[2].Value.ToString();
            txtPrice.Text = row.Cells[3].Value.ToString();
            txtChar.Text = row.Cells[4].Value.ToString();

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
                    if (diBll.Remove(id))
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

        private void txtTitleSearch_Leave(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtTitleSearch.Text = "";
            ddlTypeSearch.SelectedIndex = 0;
            LoadList();
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            DishTypeInfoList dtiList = new DishTypeInfoList();
            dtiList.UpdateTypeEvent += UpdateType;
            dtiList.Show();
        }

        private void UpdateType()
        {
            LoadList();
            LoadTypeList();
        }
    }
        
 }
