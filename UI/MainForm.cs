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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private ListViewItem itemTable;
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //根据登录的用户来决定管理员菜单项对该用户是否可见
            //if (this.Tag.ToString() == "1")
            //{
            //    menuManager.Visible = false;
            //}
            LoadHallInfo();
        }

        private void menuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuManager_Click(object sender, EventArgs e)
        {
            //单例实现一
            ManagerInfoList miList = FormFactory.CreateMIL();
            miList.Show();
            miList.Focus();
        }

        private void menuMember_Click(object sender, EventArgs e)
        {
            //单例实现二
            MemberInfoList mi = new MemberInfoList();
            mi.Show();
            mi.Focus();
        }

        private void menuDish_Click(object sender, EventArgs e)
        {
            DishInfoList diList = DishInfoList.Create();
            diList.Show();
            diList.Focus();
        }

        private void menuTable_Click(object sender, EventArgs e)
        {
            TableInfoList tiList = TableInfoList.Creat();
            tiList.Show();
            tiList.Focus();
        }

        private void LoadHallInfo()
        {
            HallInfoBll hiBll = new HallInfoBll();
            var list = hiBll.GetList();
            foreach (var hallInfo in list)
            {
                TabPage page = new TabPage(hallInfo.HTitle);
                page.Tag = hallInfo.Hid;
                tabHall.TabPages.Add(page);
            }
            tabHall_SelectedIndexChanged(null, null);
        }

        private void tabHall_SelectedIndexChanged(object sender, EventArgs e)
        {
            //选择一个tabPage，然后根据当前选中的tabpage存储的厅包编号，查找里面的餐桌，然后创建ListView，加入所以餐桌，再将ListView加到当前选中的TagPage
            //1.获取选中的tabPage
            var tabPage = tabHall.SelectedTab;
            int hallId = Convert.ToInt32(tabPage.Tag);

            //2.查询餐桌
            TableInfo tiSearch = new TableInfo();
            tiSearch.THallId= hallId;//厅包的条件
            tiSearch.IsFreeSearch = -1; // 空闲状态的要求，-1表示全部
            TableInfoBll tiBll = new TableInfoBll();
            var listTableInfo = tiBll.GetList(tiSearch);

            //3.创建ListView，加入项
            ListView listview = new ListView();
            listview.LargeImageList = imageList1;
            listview.Dock = DockStyle.Fill;
            listview.MultiSelect = false;
            //为ListView绑定双击事件，以获得被双击的项，从而完成点菜
            listview.DoubleClick += Listview_DoubleClick;
            //为ListView绑定单击事件，以获得被选中的餐桌，用于结账
            listview.Click += Listview_Click;
            foreach (var tableinfo in listTableInfo)
            {
                ListViewItem item = new ListViewItem(tableinfo.TTitle, tableinfo.TIsFree ? 0 : 1);
                item.Tag = tableinfo.Tid;
                listview.Items.Add(item);
            }

            //4.将ListView加入当前选中的TabPage
            tabPage.Controls.Add(listview);
        }

        private void Listview_Click(object sender, EventArgs e)
        {
            ListView listview = sender as ListView;
            itemTable = listview.SelectedItems[0];
        }

        private void Listview_DoubleClick(object sender, EventArgs e)
        {
            //得到双击的listview
            ListView listview = sender as ListView;
            //当项被双击后会被选择，得到当前选择的ListViewItem
            ListViewItem item = listview.SelectedItems[0];
            int tableId = Convert.ToInt32(item.Tag);
            //如果当前餐桌是空闲状态则进行开单操作
            if (item.ImageIndex == 0)
            {
                OrderInfoBll oiBll = new OrderInfoBll();
                if (oiBll.KaiDan(tableId))
                {
                    //将当前餐桌的状态改为占用
                    item.ImageIndex = 1;                  
                }
            }
            else
            {
                //如果不是空闲则进行加菜操作 

            }
            //打开点菜窗体
            OrderInfoList orderInfoList = new OrderInfoList();
            orderInfoList.Tag = tableId;//将餐桌编号传递过去，用于进行订单编号的查找
            orderInfoList.Show();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void menuOrder_Click(object sender, EventArgs e)
        {
            //如果没有餐桌选中，则提示
            if (itemTable == null)
            {
                MessageBox.Show("请先选择要结账的餐桌");
                return;
            }
            //判断当前选中的餐桌是否为空闲，如果是则不需要结账
            if (itemTable.ImageIndex == 0)
            {
                MessageBox.Show("当前餐桌并未开单，无需结账");
                return; 
            }
            OrderPay orderPay = OrderPay.Create();
            orderPay.Tag = itemTable.Tag;
            orderPay.SetTableFreeEvent += SetTableFree;
            orderPay.Show();
            orderPay.Focus();         
        }

        private void SetTableFree()
        {
            itemTable.ImageIndex = 0;
        }
    }
}
