namespace HAYC_Studio.LevitatedBall
{
    partial class LevitateBall
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevitateBall));
            this.mainContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showhide = new System.Windows.Forms.ToolStripMenuItem();
            this.showStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.showStyle1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showStyle2 = new System.Windows.Forms.ToolStripMenuItem();
            this.transparecy = new System.Windows.Forms.ToolStripMenuItem();
            this.opacity100 = new System.Windows.Forms.ToolStripMenuItem();
            this.opacity95 = new System.Windows.Forms.ToolStripMenuItem();
            this.opacity85 = new System.Windows.Forms.ToolStripMenuItem();
            this.opacity80 = new System.Windows.Forms.ToolStripMenuItem();
            this.opacity75 = new System.Windows.Forms.ToolStripMenuItem();
            this.opacity50 = new System.Windows.Forms.ToolStripMenuItem();
            this.opacity25 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.setup = new System.Windows.Forms.ToolStripMenuItem();
            this.quit = new System.Windows.Forms.ToolStripMenuItem();
            this.showDetailFormTimer = new System.Windows.Forms.Timer(this.components);
            this.hideDetailFormTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.mainBallControl1 = new HAYC_Studio.LevitatedBall.MainBallControl();
            this.mainContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContextMenu
            // 
            this.mainContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showhide,
            this.showStyle,
            this.transparecy,
            this.toolStripSeparator1,
            this.setup,
            this.quit});
            this.mainContextMenu.Name = "mainContextMenu";
            this.mainContextMenu.Size = new System.Drawing.Size(139, 130);
            // 
            // showhide
            // 
            this.showhide.Name = "showhide";
            this.showhide.ShowShortcutKeys = false;
            this.showhide.Size = new System.Drawing.Size(138, 24);
            this.showhide.Text = "隐藏";
            this.showhide.Click += new System.EventHandler(this.showhide_Click);
            // 
            // showStyle
            // 
            this.showStyle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showStyle1,
            this.showStyle2});
            this.showStyle.Name = "showStyle";
            this.showStyle.Size = new System.Drawing.Size(138, 24);
            this.showStyle.Text = "显示方式";
            // 
            // showStyle1
            // 
            this.showStyle1.Name = "showStyle1";
            this.showStyle1.Size = new System.Drawing.Size(204, 26);
            this.showStyle1.Text = "不在前端显示";
            this.showStyle1.Click += new System.EventHandler(this.showStyle1_Click);
            // 
            // showStyle2
            // 
            this.showStyle2.Name = "showStyle2";
            this.showStyle2.Size = new System.Drawing.Size(204, 26);
            this.showStyle2.Text = "在其他窗口前显示";
            this.showStyle2.Click += new System.EventHandler(this.showStyle2_Click);
            // 
            // transparecy
            // 
            this.transparecy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opacity100,
            this.opacity95,
            this.opacity85,
            this.opacity80,
            this.opacity75,
            this.opacity50,
            this.opacity25});
            this.transparecy.Name = "transparecy";
            this.transparecy.ShowShortcutKeys = false;
            this.transparecy.Size = new System.Drawing.Size(138, 24);
            this.transparecy.Text = "透明度";
            // 
            // opacity100
            // 
            this.opacity100.AutoSize = false;
            this.opacity100.Name = "opacity100";
            this.opacity100.ShowShortcutKeys = false;
            this.opacity100.Size = new System.Drawing.Size(152, 22);
            this.opacity100.Text = "不透明";
            this.opacity100.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.opacity100.Click += new System.EventHandler(this.opacity100_Click);
            // 
            // opacity95
            // 
            this.opacity95.Name = "opacity95";
            this.opacity95.ShowShortcutKeys = false;
            this.opacity95.Size = new System.Drawing.Size(120, 26);
            this.opacity95.Text = "95";
            this.opacity95.Click += new System.EventHandler(this.opacity95_Click);
            // 
            // opacity85
            // 
            this.opacity85.Name = "opacity85";
            this.opacity85.ShowShortcutKeys = false;
            this.opacity85.Size = new System.Drawing.Size(120, 26);
            this.opacity85.Text = "85";
            this.opacity85.Click += new System.EventHandler(this.opacity85_Click);
            // 
            // opacity80
            // 
            this.opacity80.Name = "opacity80";
            this.opacity80.ShowShortcutKeys = false;
            this.opacity80.Size = new System.Drawing.Size(120, 26);
            this.opacity80.Text = "80";
            this.opacity80.Click += new System.EventHandler(this.opacity80_Click);
            // 
            // opacity75
            // 
            this.opacity75.Name = "opacity75";
            this.opacity75.ShowShortcutKeys = false;
            this.opacity75.Size = new System.Drawing.Size(120, 26);
            this.opacity75.Text = "75";
            this.opacity75.Click += new System.EventHandler(this.opacity75_Click);
            // 
            // opacity50
            // 
            this.opacity50.Name = "opacity50";
            this.opacity50.ShowShortcutKeys = false;
            this.opacity50.Size = new System.Drawing.Size(120, 26);
            this.opacity50.Text = "50";
            this.opacity50.Click += new System.EventHandler(this.opacity50_Click);
            // 
            // opacity25
            // 
            this.opacity25.Name = "opacity25";
            this.opacity25.ShowShortcutKeys = false;
            this.opacity25.Size = new System.Drawing.Size(120, 26);
            this.opacity25.Text = "25";
            this.opacity25.Click += new System.EventHandler(this.opacity25_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
            // 
            // setup
            // 
            this.setup.Name = "setup";
            this.setup.Size = new System.Drawing.Size(138, 24);
            this.setup.Text = "系统设置";
            this.setup.Click += new System.EventHandler(this.setup_Click);
            // 
            // quit
            // 
            this.quit.Name = "quit";
            this.quit.ShowShortcutKeys = false;
            this.quit.Size = new System.Drawing.Size(138, 24);
            this.quit.Text = "退出系统";
            this.quit.Click += new System.EventHandler(this.quit_Click);
            // 
            // showDetailFormTimer
            // 
            this.showDetailFormTimer.Interval = 700;
            this.showDetailFormTimer.Tick += new System.EventHandler(this.showDetailFormTimer_Tick);
            // 
            // hideDetailFormTimer
            // 
            this.hideDetailFormTimer.Interval = 700;
            this.hideDetailFormTimer.Tick += new System.EventHandler(this.hideDetailFormTimer_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.mainContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "小安悬浮球";
            this.notifyIcon.Visible = true;
            // 
            // mainBallControl1
            // 
            this.mainBallControl1.BackColor = System.Drawing.Color.White;
            this.mainBallControl1.Location = new System.Drawing.Point(0, 0);
            this.mainBallControl1.Name = "mainBallControl1";
            this.mainBallControl1.Size = new System.Drawing.Size(170, 69);
            this.mainBallControl1.TabIndex = 0;
            this.mainBallControl1.Text = "mainBallControl1";
            this.mainBallControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseDown);
            this.mainBallControl1.MouseEnter += new System.EventHandler(this.miniBigFormSpace_MouseEnter);
            this.mainBallControl1.MouseLeave += new System.EventHandler(this.miniBigFormSpace_MouseLeave);
            this.mainBallControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseMove);
            this.mainBallControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseUp);
            // 
            // LevitateBall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(156, 57);
            this.ContextMenuStrip = this.mainContextMenu;
            this.Controls.Add(this.mainBallControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LevitateBall";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LevitateBall";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.White;
            this.mainContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MainBallControl mainBallControl1;
        private System.Windows.Forms.ContextMenuStrip mainContextMenu;
        private System.Windows.Forms.ToolStripMenuItem showhide;
        private System.Windows.Forms.ToolStripMenuItem showStyle;
        private System.Windows.Forms.ToolStripMenuItem showStyle1;
        private System.Windows.Forms.ToolStripMenuItem showStyle2;
        private System.Windows.Forms.ToolStripMenuItem transparecy;
        private System.Windows.Forms.ToolStripMenuItem opacity100;
        private System.Windows.Forms.ToolStripMenuItem opacity95;
        private System.Windows.Forms.ToolStripMenuItem opacity85;
        private System.Windows.Forms.ToolStripMenuItem opacity80;
        private System.Windows.Forms.ToolStripMenuItem opacity75;
        private System.Windows.Forms.ToolStripMenuItem opacity50;
        private System.Windows.Forms.ToolStripMenuItem opacity25;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quit;
        private System.Windows.Forms.Timer showDetailFormTimer;
        private System.Windows.Forms.Timer hideDetailFormTimer;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem setup;
    }
}