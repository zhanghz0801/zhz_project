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
    public partial class ManagerInfoList : Form
    {
        public ManagerInfoList()
        {
            InitializeComponent();
        }

        ManagerInfoBll miBll = new ManagerInfoBll();

        private void ManagerInfoList_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            gvList.AutoGenerateColumns = false;//取消自动生成列
            gvList.DataSource = miBll.GetList();
        }

        private void gvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                switch (e.Value.ToString())
                {
                    case "1":
                        e.Value = "经理";
                        break;
                    case "0":
                        e.Value = "店员";
                        break;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //接收控件的值，用于构造对象
            ManagerInfo mi = new ManagerInfo()
            {
                MName = txtName.Text,
                MPwd = txtPwd.Text,
                MType=rb1.Checked?1:0 
            };
            //判断当前是进行添加操作还修改操作
            if (btnSave.Text == "添加")
            {
                if (miBll.Add(mi))
                {
                    btnCancel_Click(null, null);
                    LoadList();
                }
                else
                {
                    MessageBox.Show("添加失败！");
                };
            }
            else
            {
                mi.Mid = Convert.ToInt32(txtId.Text);
                if (miBll.Edit(mi))
                {
                    btnCancel_Click(null, null);
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败，请稍后再试！");
                }
            }

           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtName.Text = "";
            txtPwd.Text = "";
            rb2.Checked = true;
            btnSave.Text = "添加"; 
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //获取选中的行
            var rows = gvList.SelectedRows;
            if (rows.Count>0)  //说明有选中的行
            {
                DialogResult result= MessageBox.Show("确定删除吗？","提示",MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    //拿第一行第一个单元格的值
                    int id = Convert.ToInt32(rows[0].Cells[0].Value);
                    if (miBll.Remove(id))
                    {
                        LoadList();
                    }
                    else
                    {
                        MessageBox.Show("删除失败！");
                    }
                }
            } 
            else
            {
                MessageBox.Show("还没选中想要删除的行！");
            }

        }

        private void gvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //当双击单元格时，将内容显示到控件中，以备修改 
            var row = gvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            txtPwd.Text = "******";
            if (row.Cells[2].Value.ToString() == "1")
            {
                rb1.Checked = true;
            }
            else
            {
                rb2.Checked = true;
            }

            btnSave.Text = "修改";
        }
    }
}
