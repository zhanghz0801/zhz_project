namespace UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabHall = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuManager = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMember = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDish = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabHall
            // 
            this.tabHall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabHall.Location = new System.Drawing.Point(0, 72);
            this.tabHall.Margin = new System.Windows.Forms.Padding(4);
            this.tabHall.Name = "tabHall";
            this.tabHall.SelectedIndex = 0;
            this.tabHall.Size = new System.Drawing.Size(800, 378);
            this.tabHall.TabIndex = 3;
            this.tabHall.SelectedIndexChanged += new System.EventHandler(this.tabHall_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuManager,
            this.menuMember,
            this.menuDish,
            this.menuTable,
            this.menuOrder,
            this.menuQuit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(800, 72);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuManager
            // 
            this.menuManager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuManager.Image = global::UI.Properties.Resources.menuManager;
            this.menuManager.Name = "menuManager";
            this.menuManager.Size = new System.Drawing.Size(76, 68);
            this.menuManager.Text = "manager";
            this.menuManager.Click += new System.EventHandler(this.menuManager_Click);
            // 
            // menuMember
            // 
            this.menuMember.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuMember.Image = global::UI.Properties.Resources.menuMember;
            this.menuMember.Name = "menuMember";
            this.menuMember.Size = new System.Drawing.Size(76, 68);
            this.menuMember.Text = "member";
            this.menuMember.Click += new System.EventHandler(this.menuMember_Click);
            // 
            // menuDish
            // 
            this.menuDish.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuDish.Image = global::UI.Properties.Resources.menuDish;
            this.menuDish.Name = "menuDish";
            this.menuDish.Size = new System.Drawing.Size(76, 68);
            this.menuDish.Text = "dish";
            this.menuDish.Click += new System.EventHandler(this.menuDish_Click);
            // 
            // menuTable
            // 
            this.menuTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuTable.Image = global::UI.Properties.Resources.menuTable;
            this.menuTable.Name = "menuTable";
            this.menuTable.Size = new System.Drawing.Size(76, 68);
            this.menuTable.Text = "table";
            this.menuTable.Click += new System.EventHandler(this.menuTable_Click);
            // 
            // menuOrder
            // 
            this.menuOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuOrder.Image = global::UI.Properties.Resources.menuOrder;
            this.menuOrder.Name = "menuOrder";
            this.menuOrder.Size = new System.Drawing.Size(76, 68);
            this.menuOrder.Text = "order";
            this.menuOrder.Click += new System.EventHandler(this.menuOrder_Click);
            // 
            // menuQuit
            // 
            this.menuQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuQuit.Image = global::UI.Properties.Resources.menuQuit;
            this.menuQuit.Name = "menuQuit";
            this.menuQuit.Size = new System.Drawing.Size(76, 68);
            this.menuQuit.Text = "quit";
            this.menuQuit.Click += new System.EventHandler(this.menuQuit_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "desk2.png");
            this.imageList1.Images.SetKeyName(1, "desk1.png");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabHall);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainForm";
            this.Text = "餐饮管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabHall;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuManager;
        private System.Windows.Forms.ToolStripMenuItem menuMember;
        private System.Windows.Forms.ToolStripMenuItem menuDish;
        private System.Windows.Forms.ToolStripMenuItem menuTable;
        private System.Windows.Forms.ToolStripMenuItem menuOrder;
        private System.Windows.Forms.ToolStripMenuItem menuQuit;
        private System.Windows.Forms.ImageList imageList1;
    }
}