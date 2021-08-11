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
    public partial class DishTypeInfoList : Form
    {
        public DishTypeInfoList()
        {
            InitializeComponent();
        }
        private DishTypeInfoBll dtiBll = new DishTypeInfoBll();

        public event Action UpdateTypeEvent;

        private void DishTypeInfoList_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = dtiBll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DishTypeInfo  dti = new DishTypeInfo();
            dti.DTitle = txtTitle.Text;

            if (btnSave.Text.Equals("添加"))
            {
                //添加逻辑
                if (dtiBll.Add(dti))
                {
                    btnCancel_Click(null, null);
                    LoadList();
                    UpdateTypeEvent();
                }
                else
                {
                    MessageBox.Show("添加失败，请稍后再试");
                }
            }
            else
            {
                //修改逻辑
                dti.Did = Convert.ToInt32(txtId.Text);
                if (dtiBll.Edit(dti))
                {
                    btnCancel_Click(null, null);
                    LoadList();
                    UpdateTypeEvent();
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
            txtTitle.Text = "";
            btnSave.Text = "添加";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //获取双击的单元格所在的行
            var row = dgvList.Rows[e.RowIndex];
            //将行中的数据显示到控件中
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();

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
                    if (dtiBll.Remove(id))
                    {
                        LoadList();
                        UpdateTypeEvent();
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择");
            }
        }
    }
}
