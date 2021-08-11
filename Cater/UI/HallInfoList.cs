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
    public partial class HallInfoList : Form
    {
        public HallInfoList()
        {
            InitializeComponent();
        }

        HallInfoBll hiBll = new HallInfoBll();
        public event Action RefreshHallEvent;
        private void HallInfoList_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = hiBll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            HallInfo hi = new HallInfo();
            hi.HTitle = txtTitle.Text;

            if (btnSave.Text.Equals("添加"))
            {
                if (hiBll.Add(hi))
                {
                    btnCancel_Click(null,null);
                    LoadList();
                    RefreshHallEvent();
                }
                else
                {
                    MessageBox.Show("失败");
                }
            }
            else
            {
                hi.Hid = Convert.ToInt32(txtId.Text);
                if (hiBll.Edit(hi))
                {
                    btnCancel_Click(null, null);
                    LoadList();
                    RefreshHallEvent();
                }
                else
                {
                    MessageBox.Show("失败");
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
            //找到被点击的那一行
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text= row.Cells[1].Value.ToString();
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var row = dgvList.SelectedRows;//获得选中的行
            if (row.Count > 0)
            {
                DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel); ;
                if (result == DialogResult.OK)
                {
                    int id = Convert.ToInt32(row[0].Cells[0].Value);
                    if (hiBll.Remove(id))
                    {
                        LoadList();
                        RefreshHallEvent();
                    }
                    else
                    {
                        MessageBox.Show("出现错误");
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择行");
            }
        }
    }
}
