namespace StudioLicenseCreater
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_mac = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_cpu = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_license = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(96, 23);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(515, 25);
            this.txt_ip.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "MAC地址";
            // 
            // txt_mac
            // 
            this.txt_mac.Location = new System.Drawing.Point(96, 77);
            this.txt_mac.Name = "txt_mac";
            this.txt_mac.Size = new System.Drawing.Size(515, 25);
            this.txt_mac.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "CPU编号";
            // 
            // txt_cpu
            // 
            this.txt_cpu.Location = new System.Drawing.Point(96, 126);
            this.txt_cpu.Name = "txt_cpu";
            this.txt_cpu.Size = new System.Drawing.Size(515, 25);
            this.txt_cpu.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(494, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "生成 License";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_license
            // 
            this.txt_license.Location = new System.Drawing.Point(96, 219);
            this.txt_license.Name = "txt_license";
            this.txt_license.ReadOnly = true;
            this.txt_license.Size = new System.Drawing.Size(515, 25);
            this.txt_license.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "License";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 276);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_license);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_cpu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_mac);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_ip);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HAYC Studio License Creater";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_mac;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_cpu;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_license;
        private System.Windows.Forms.Label label4;
    }
}

