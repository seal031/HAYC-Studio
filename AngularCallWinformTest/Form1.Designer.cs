using System.Windows.Forms;

namespace AngularCallWinformTest
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.wb3 = new AngularCallWinformTest.IEBrowser();
            this.wb2 = new AngularCallWinformTest.IEBrowser();
            this.wb1 = new AngularCallWinformTest.IEBrowser();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1177, 48);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(957, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "访问";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(924, 25);
            this.textBox1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.wb3);
            this.panel2.Controls.Add(this.wb2);
            this.panel2.Controls.Add(this.wb1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1177, 683);
            this.panel2.TabIndex = 3;
            // 
            // wb3
            // 
            this.wb3.Location = new System.Drawing.Point(781, 3);
            this.wb3.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb3.Name = "wb3";
            this.wb3.Size = new System.Drawing.Size(383, 683);
            this.wb3.TabIndex = 2;
            // 
            // wb2
            // 
            this.wb2.Location = new System.Drawing.Point(389, 0);
            this.wb2.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb2.Name = "wb2";
            this.wb2.Size = new System.Drawing.Size(383, 683);
            this.wb2.TabIndex = 1;
            // 
            // wb1
            // 
            this.wb1.Location = new System.Drawing.Point(0, 0);
            this.wb1.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb1.Name = "wb1";
            this.wb1.Size = new System.Drawing.Size(383, 683);
            this.wb1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1057, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "清理";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 731);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel2;
        private Button button2;
        private IEBrowser wb3;
        private IEBrowser wb2;
        private IEBrowser wb1;
    }
}

