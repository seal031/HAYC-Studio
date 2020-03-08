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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btn_login = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HAYC_Studio.Properties.Resources.user;
            this.pictureBox1.Location = new System.Drawing.Point(185, 50);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(136, 136);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // txt_username
            // 
            this.txt_username.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_username.Location = new System.Drawing.Point(135, 207);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(208, 29);
            this.txt_username.TabIndex = 16;
            // 
            // txt_password
            // 
            this.txt_password.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_password.Location = new System.Drawing.Point(135, 252);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(208, 29);
            this.txt_password.TabIndex = 17;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::HAYC_Studio.Properties.Resources.userAndPwd;
            this.pictureBox2.Location = new System.Drawing.Point(117, 192);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(301, 107);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // btn_login
            // 
            this.btn_login.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_login.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.btn_login.Image = global::HAYC_Studio.Properties.Resources.loginSmall;
            this.btn_login.Location = new System.Drawing.Point(343, 244);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(48, 45);
            this.btn_login.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_login.TabIndex = 18;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(502, 359);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.pictureBox2);
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统登录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.PictureBox pictureBox2;
        private DevComponents.DotNetBar.ButtonX btn_login;
    }
}