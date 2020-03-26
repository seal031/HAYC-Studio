namespace HAYC_Studio.LevitatedBall
{
    partial class BigForm
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
            this.closeBtn = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.Label();
            this.progressPanel = new System.Windows.Forms.Panel();
            this.lbl_clear = new System.Windows.Forms.Label();
            this.messageListBox = new System.Windows.Forms.ListBox();
            this.systemResources = new System.Windows.Forms.Label();
            this.sb_speech = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.label2 = new System.Windows.Forms.Label();
            this.sb_face = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_speech_state = new System.Windows.Forms.Label();
            this.lbl_face_state = new System.Windows.Forms.Label();
            this.timer_service_state = new System.Windows.Forms.Timer(this.components);
            this.hideMessageFormTimer = new System.Windows.Forms.Timer(this.components);
            this.binBallControl = new HAYC_Studio.LevitatedBall.BallControl();
            this.progressPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.Transparent;
            this.closeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.closeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.closeBtn.ForeColor = System.Drawing.Color.Transparent;
            this.closeBtn.Image = global::HAYC_Studio.Properties.Resources.close_normal;
            this.closeBtn.Location = new System.Drawing.Point(330, 8);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(4);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(24, 22);
            this.closeBtn.TabIndex = 3;
            this.closeBtn.UseVisualStyleBackColor = false;
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.title.Location = new System.Drawing.Point(13, 9);
            this.title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(84, 20);
            this.title.TabIndex = 4;
            this.title.Text = "小安悬浮球";
            this.title.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.title.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            // 
            // progressPanel
            // 
            this.progressPanel.BackColor = System.Drawing.Color.White;
            this.progressPanel.Controls.Add(this.lbl_clear);
            this.progressPanel.Controls.Add(this.messageListBox);
            this.progressPanel.Controls.Add(this.systemResources);
            this.progressPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressPanel.Location = new System.Drawing.Point(0, 152);
            this.progressPanel.Margin = new System.Windows.Forms.Padding(4);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(362, 326);
            this.progressPanel.TabIndex = 8;
            this.progressPanel.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.progressPanel.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            // 
            // lbl_clear
            // 
            this.lbl_clear.AutoSize = true;
            this.lbl_clear.BackColor = System.Drawing.Color.White;
            this.lbl_clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_clear.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_clear.ForeColor = System.Drawing.Color.Blue;
            this.lbl_clear.Location = new System.Drawing.Point(310, 11);
            this.lbl_clear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_clear.Name = "lbl_clear";
            this.lbl_clear.Size = new System.Drawing.Size(39, 19);
            this.lbl_clear.TabIndex = 2;
            this.lbl_clear.Text = "清除";
            this.lbl_clear.Click += new System.EventHandler(this.lbl_clear_Click);
            this.lbl_clear.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.lbl_clear.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            // 
            // messageListBox
            // 
            this.messageListBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.messageListBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.messageListBox.FormattingEnabled = true;
            this.messageListBox.ItemHeight = 20;
            this.messageListBox.Location = new System.Drawing.Point(0, 42);
            this.messageListBox.Name = "messageListBox";
            this.messageListBox.Size = new System.Drawing.Size(362, 284);
            this.messageListBox.TabIndex = 1;
            this.messageListBox.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.messageListBox.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            // 
            // systemResources
            // 
            this.systemResources.AutoSize = true;
            this.systemResources.BackColor = System.Drawing.Color.White;
            this.systemResources.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.systemResources.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.systemResources.Location = new System.Drawing.Point(7, 11);
            this.systemResources.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.systemResources.Name = "systemResources";
            this.systemResources.Size = new System.Drawing.Size(99, 19);
            this.systemResources.TabIndex = 0;
            this.systemResources.Text = "最近识别结果";
            this.systemResources.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.systemResources.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            // 
            // sb_speech
            // 
            // 
            // 
            // 
            this.sb_speech.BackgroundStyle.Class = "";
            this.sb_speech.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sb_speech.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sb_speech.Location = new System.Drawing.Point(268, 54);
            this.sb_speech.Name = "sb_speech";
            this.sb_speech.OffText = "停止";
            this.sb_speech.OnText = "启动";
            this.sb_speech.Size = new System.Drawing.Size(81, 20);
            this.sb_speech.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sb_speech.TabIndex = 11;
            this.sb_speech.Value = true;
            this.sb_speech.ValueChanged += new System.EventHandler(this.sb_speech_ValueChanged);
            this.sb_speech.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseDown);
            this.sb_speech.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.sb_speech.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            this.sb_speech.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseMove);
            this.sb_speech.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(135, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "语音识别";
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseDown);
            this.label2.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.label2.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            this.label2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseMove);
            this.label2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseUp);
            // 
            // sb_face
            // 
            // 
            // 
            // 
            this.sb_face.BackgroundStyle.Class = "";
            this.sb_face.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sb_face.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sb_face.Location = new System.Drawing.Point(268, 94);
            this.sb_face.Name = "sb_face";
            this.sb_face.OffText = "停止";
            this.sb_face.OnText = "启动";
            this.sb_face.Size = new System.Drawing.Size(81, 20);
            this.sb_face.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sb_face.TabIndex = 13;
            this.sb_face.Value = true;
            this.sb_face.ValueChanged += new System.EventHandler(this.sb_face_ValueChanged);
            this.sb_face.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseDown);
            this.sb_face.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.sb_face.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            this.sb_face.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseMove);
            this.sb_face.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(135, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "人脸识别";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseDown);
            this.label1.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.label1.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseUp);
            // 
            // lbl_speech_state
            // 
            this.lbl_speech_state.AutoSize = true;
            this.lbl_speech_state.BackColor = System.Drawing.Color.Transparent;
            this.lbl_speech_state.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_speech_state.Location = new System.Drawing.Point(205, 54);
            this.lbl_speech_state.Name = "lbl_speech_state";
            this.lbl_speech_state.Size = new System.Drawing.Size(54, 19);
            this.lbl_speech_state.TabIndex = 14;
            this.lbl_speech_state.Text = "获取中";
            this.lbl_speech_state.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseDown);
            this.lbl_speech_state.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.lbl_speech_state.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            this.lbl_speech_state.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseMove);
            this.lbl_speech_state.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseUp);
            // 
            // lbl_face_state
            // 
            this.lbl_face_state.AutoSize = true;
            this.lbl_face_state.BackColor = System.Drawing.Color.Transparent;
            this.lbl_face_state.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_face_state.Location = new System.Drawing.Point(205, 94);
            this.lbl_face_state.Name = "lbl_face_state";
            this.lbl_face_state.Size = new System.Drawing.Size(54, 19);
            this.lbl_face_state.TabIndex = 15;
            this.lbl_face_state.Text = "获取中";
            this.lbl_face_state.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseDown);
            this.lbl_face_state.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.lbl_face_state.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            this.lbl_face_state.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseMove);
            this.lbl_face_state.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseUp);
            // 
            // timer_service_state
            // 
            this.timer_service_state.Interval = 1000;
            this.timer_service_state.Tick += new System.EventHandler(this.timer_service_state_Tick);
            // 
            // hideMessageFormTimer
            // 
            this.hideMessageFormTimer.Interval = 700;
            this.hideMessageFormTimer.Tick += new System.EventHandler(this.hideMessageFormTimer_Tick);
            // 
            // binBallControl
            // 
            this.binBallControl.Location = new System.Drawing.Point(22, 45);
            this.binBallControl.Name = "binBallControl";
            this.binBallControl.Size = new System.Drawing.Size(105, 99);
            this.binBallControl.TabIndex = 9;
            this.binBallControl.Text = "ballControl1";
            // 
            // BigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(362, 478);
            this.Controls.Add(this.lbl_face_state);
            this.Controls.Add(this.lbl_speech_state);
            this.Controls.Add(this.sb_face);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sb_speech);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.binBallControl);
            this.Controls.Add(this.progressPanel);
            this.Controls.Add(this.title);
            this.Controls.Add(this.closeBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BigForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "BigForm";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.BigForm_Activated);
            this.Deactivate += new System.EventHandler(this.BigForm_Deactivate);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseDown);
            this.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseUp);
            this.progressPanel.ResumeLayout(false);
            this.progressPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Panel progressPanel;
        private System.Windows.Forms.Label systemResources;
        private System.Windows.Forms.ListBox messageListBox;
        private System.Windows.Forms.Label lbl_clear;
        private BallControl binBallControl;
        private DevComponents.DotNetBar.Controls.SwitchButton sb_speech;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.SwitchButton sb_face;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_speech_state;
        private System.Windows.Forms.Label lbl_face_state;
        private System.Windows.Forms.Timer timer_service_state;
        private System.Windows.Forms.Timer hideMessageFormTimer;
    }
}