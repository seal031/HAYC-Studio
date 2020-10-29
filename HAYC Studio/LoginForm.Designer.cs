namespace HAYC_Studio
{
    partial class LoginForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btn_login = new DevComponents.DotNetBar.ButtonX();
            this.pb_camera = new System.Windows.Forms.PictureBox();
            this.lbl_faceDetect = new DevComponents.DotNetBar.LabelX();
            this.timer_drawBox = new System.Windows.Forms.Timer(this.components);
            this.pb_cameraWithBox = new System.Windows.Forms.PictureBox();
            this.lbl_faceDetectResult = new System.Windows.Forms.Label();
            this.timer_saveImage = new System.Windows.Forms.Timer(this.components);
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_camera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_cameraWithBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HAYC_Studio.Properties.Resources.user;
            this.pictureBox1.Location = new System.Drawing.Point(1060, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(136, 136);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // txt_username
            // 
            this.txt_username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_username.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_username.ForeColor = System.Drawing.Color.White;
            this.txt_username.Location = new System.Drawing.Point(505, 336);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(218, 29);
            this.txt_username.TabIndex = 16;
            this.txt_username.TextChanged += new System.EventHandler(this.txt_username_TextChanged);
            this.txt_username.Enter += new System.EventHandler(this.txt_username_Enter);
            this.txt_username.MouseEnter += new System.EventHandler(this.txt_username_MouseEnter);
            this.txt_username.MouseLeave += new System.EventHandler(this.txt_username_MouseLeave);
            // 
            // txt_password
            // 
            this.txt_password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_password.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_password.ForeColor = System.Drawing.Color.White;
            this.txt_password.Location = new System.Drawing.Point(504, 379);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(218, 29);
            this.txt_password.TabIndex = 17;
            this.txt_password.TextChanged += new System.EventHandler(this.txt_username_TextChanged);
            this.txt_password.Enter += new System.EventHandler(this.txt_username_Enter);
            this.txt_password.MouseEnter += new System.EventHandler(this.txt_username_MouseEnter);
            this.txt_password.MouseLeave += new System.EventHandler(this.txt_username_MouseLeave);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::HAYC_Studio.Properties.Resources.userAndPwd;
            this.pictureBox2.Location = new System.Drawing.Point(918, 156);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(301, 107);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // btn_login
            // 
            this.btn_login.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_login.BackColor = System.Drawing.Color.Transparent;
            this.btn_login.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.btn_login.Location = new System.Drawing.Point(467, 435);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(259, 35);
            this.btn_login.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_login.TabIndex = 18;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // pb_camera
            // 
            this.pb_camera.Location = new System.Drawing.Point(0, 0);
            this.pb_camera.Margin = new System.Windows.Forms.Padding(4);
            this.pb_camera.Name = "pb_camera";
            this.pb_camera.Size = new System.Drawing.Size(300, 300);
            this.pb_camera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_camera.TabIndex = 21;
            this.pb_camera.TabStop = false;
            this.pb_camera.Visible = false;
            // 
            // lbl_faceDetect
            // 
            this.lbl_faceDetect.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbl_faceDetect.BackgroundStyle.Class = "";
            this.lbl_faceDetect.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbl_faceDetect.Location = new System.Drawing.Point(546, 162);
            this.lbl_faceDetect.Name = "lbl_faceDetect";
            this.lbl_faceDetect.Size = new System.Drawing.Size(103, 107);
            this.lbl_faceDetect.TabIndex = 22;
            this.lbl_faceDetect.Click += new System.EventHandler(this.lbl_faceDetect_Click);
            // 
            // timer_drawBox
            // 
            this.timer_drawBox.Enabled = true;
            this.timer_drawBox.Interval = 10;
            this.timer_drawBox.Tick += new System.EventHandler(this.timer_drawBox_Tick);
            // 
            // pb_cameraWithBox
            // 
            this.pb_cameraWithBox.Location = new System.Drawing.Point(312, 129);
            this.pb_cameraWithBox.Margin = new System.Windows.Forms.Padding(4);
            this.pb_cameraWithBox.Name = "pb_cameraWithBox";
            this.pb_cameraWithBox.Size = new System.Drawing.Size(300, 300);
            this.pb_cameraWithBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_cameraWithBox.TabIndex = 23;
            this.pb_cameraWithBox.TabStop = false;
            this.pb_cameraWithBox.Visible = false;
            // 
            // lbl_faceDetectResult
            // 
            this.lbl_faceDetectResult.AutoSize = true;
            this.lbl_faceDetectResult.BackColor = System.Drawing.Color.Transparent;
            this.lbl_faceDetectResult.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_faceDetectResult.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_faceDetectResult.Location = new System.Drawing.Point(562, 101);
            this.lbl_faceDetectResult.Name = "lbl_faceDetectResult";
            this.lbl_faceDetectResult.Size = new System.Drawing.Size(91, 24);
            this.lbl_faceDetectResult.TabIndex = 24;
            this.lbl_faceDetectResult.Text = "验证中......";
            this.lbl_faceDetectResult.Visible = false;
            // 
            // timer_saveImage
            // 
            this.timer_saveImage.Interval = 2000;
            this.timer_saveImage.Tick += new System.EventHandler(this.timer_saveImage_Tick);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.AutoSize = true;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(1037, 463);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(104, 35);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 25;
            this.buttonX1.Text = "警灯测试--红";
            this.buttonX1.Visible = false;
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.AutoSize = true;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(1037, 504);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(104, 35);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 26;
            this.buttonX2.Text = "警灯测试--黄";
            this.buttonX2.Visible = false;
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.AutoSize = true;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new System.Drawing.Point(1037, 545);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(104, 35);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 27;
            this.buttonX3.Text = "警灯测试--绿";
            this.buttonX3.Visible = false;
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btn_login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::HAYC_Studio.Properties.Resources.LoginBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1223, 632);
            this.Controls.Add(this.buttonX3);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.lbl_faceDetectResult);
            this.Controls.Add(this.pb_cameraWithBox);
            this.Controls.Add(this.lbl_faceDetect);
            this.Controls.Add(this.pb_camera);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统登录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginForm_FormClosed);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.Shown += new System.EventHandler(this.LoginForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_camera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_cameraWithBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.PictureBox pictureBox2;
        private DevComponents.DotNetBar.ButtonX btn_login;
        private AxWMPLib.AxWindowsMediaPlayer mediaPlayer;
        private System.Windows.Forms.PictureBox pb_camera;
        private DevComponents.DotNetBar.LabelX lbl_faceDetect;
        private System.Windows.Forms.Timer timer_drawBox;
        private System.Windows.Forms.PictureBox pb_cameraWithBox;
        private System.Windows.Forms.Label lbl_faceDetectResult;
        private System.Windows.Forms.Timer timer_saveImage;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX buttonX3;
    }
}