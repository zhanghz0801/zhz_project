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
    public partial class TableInfoList : Form
    {
        private TableInfoList()
        {
            InitializeComponent();
        }
        private static TableInfoList tiList;
        public static TableInfoList Creat()
        {
            if (tiList == null || tiList.IsDisposed)
            {
                tiList = new TableInfoList();
            }

            return tiList;
        }

        TableInfoBll tiBll = new TableInfoBll();

        private void LoadList()
        {
            TableInfo ti = new TableInfo();
            ti.THallId = Convert.ToInt32(ddlHallSearch.SelectedValue);
            ti.IsFreeSearch = Convert.ToInt32(ddlFreeSearch.SelectedValue);

            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = tiBll.GetList(ti);
        }
        private void LoadSearch()
        {
            HallInfoBll hiBll = new HallInfoBll();
            var list = hiBll.GetList();
            list.Insert(0, new HallInfo()
            {
                Hid = 0,
                HTitle = "全部"
            });
            ddlHallSearch.DisplayMember = "HTitle";
            ddlHallSearch.ValueMember = "Hid";
            ddlHallSearch.DataSource = list;

            ddlHallAdd.DisplayMember = "HTitle";
            ddlHallAdd.ValueMember = "Hid";
            ddlHallAdd.DataSource = hiBll.GetList();

            List<TableState> listState = new List<TableState>();
            listState.Add(new TableState(-1, "全部"));
            listState.Add(new TableState(0, "非空闲"));
            listState.Add(new TableState(1, "空闲"));

            ddlFreeSearch.DisplayMember = "Title";
            ddlFreeSearch.ValueMember = "State";
            ddlFreeSearch.DataSource = listState;
        }

        private void TableInfoList_Load(object sender, EventArgs e)
        {
            LoadList();
            LoadSearch();
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (Convert.ToBoolean(e.Value))
                {
                    e.Value = "是";
                }
                else
                {
                    e.Value = "否";
                }
            }
        }

        private void ddlHallSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlFreeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            ddlFreeSearch.SelectedIndex = 0;
            ddlHallSearch.SelectedIndex = 0;
            LoadList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            ddlHallAdd.SelectedIndex = 0;
            rbFree.Checked = true;
            btnSave.Text = "添加";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TableInfo ti = new TableInfo();
            ti.TTitle = txtTitle.Text;
            ti.THallId = Convert.ToInt32(ddlHallAdd.SelectedValue);
            ti.TIsFree = rbFree.Checked;
            if (btnSave.Text.Equals("添加"))
            {
                if (tiBll.Add(ti))
                {
                    btnCancel_Click(null, null);
                    LoadList();
                }
                else
                {
                    MessageBox.Show("失败");
                }
            }
            else
            {
                ti.Tid = Convert.ToInt32(txtId.Text);
                if (tiBll.Edit(ti))
                {
                    btnCancel_Click(null, null);
                    LoadList();
                }
                else
                {
                    MessageBox.Show("失败");
                }

            }
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //找到被点击的那一行
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            ddlHallAdd.Text = row.Cells[2].Value.ToString();
            if (Convert.ToBoolean(row.Cells[3].Value))
            {
                rbFree.Checked = true;
            }
            else
            {
                rbFree.Checked = false;
            }
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
                    if (tiBll.Remove(id))
                    {
                        LoadList();
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

        private void btnAddHall_Click(object sender, EventArgs e)
        {
            HallInfoList hiList = new HallInfoList();
            hiList.RefreshHallEvent += RefreshHallInfo;
            hiList.Show();
        }
        private void RefreshHallInfo()
        {
            LoadList();
            LoadSearch();
        }
    }
}
