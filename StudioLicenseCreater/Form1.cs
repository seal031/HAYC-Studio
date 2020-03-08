using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudioLicenseCreater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Utils.isIp(txt_ip.Text) == false)
            {
                MessageBox.Show("IP地址格式不正确");
                return;
            }
            if (Utils.isMac(txt_mac.Text) == false)
            {
                MessageBox.Show("MAC地址格式不正确");
                return;
            }

            txt_license.Text = Utils.MD5Encrypt(txt_ip.Text.Trim() + txt_mac.Text.Trim() + txt_cpu.Text.Trim(), 32);
        }
    }
}
