using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAYC_Studio.LevitatedBall
{
    public partial class BigForm : Form
    {
        public enum ShowCommands
        {
            HIDE = 1,
        }
        private bool isMouseDown = false;
        public bool isMouseEnter = false;

        private Point mouseOffset;
        private LevitateBall miniForm;

        public BigForm(LevitateBall miniForm)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.miniForm = miniForm;
        }

        #region DetailsPanel和ballControl的鼠标事件
        private void DetailsPanel_MouseLeave(object sender, EventArgs e)
        {
            Point p = MousePosition;

            if (p.X - 10 <= this.Left || p.X + 10 >= this.Right || p.Y - 10 <= this.Top || p.Y + 10 >= this.Top + this.Height)
            {
                isMouseEnter = false;
                hideMessageFormTimer.Enabled = true;
            }
        }

        private void DetailsPanel_MouseEnter(object sender, EventArgs e)
        {
            isMouseEnter = true;
            miniForm.TopLevel = this.TopLevel;

        }

        private void DetailsPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                mouseOffset = new Point(MousePosition.X - this.Location.X, MousePosition.Y - this.Location.Y);
                this.Cursor = Cursors.SizeAll;
            }
        }

        private void DetailsPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            this.Cursor = Cursors.Default;
        }

        private void DetailsPanel_MouseMove(object sender, MouseEventArgs e)
        {
            miniForm.isMouseEnter = false;
            if (isMouseDown == true)
            {
                //this.Location = getDetailFormMoveLocation();
                //miniForm.Location = getMiniBallMoveLocation();
            }
        }
        #endregion


        private void lbl_clear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否清空识别结果？","提示",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                messageListBox.Items.Clear();
            }
        }

        public void addSpeechMessage(string message)
        {
            messageListBox.Items.Insert(0, "【" + DateTime.Now.ToString("MM-dd HH:mm") + "】  " + message);
        }

        private void BigForm_Activated(object sender, EventArgs e)
        {
            timer_service_state.Enabled = true;
        }

        private void sb_speech_ValueChanged(object sender, EventArgs e)
        {
            var value = sb_speech.Value;
            if (value) //true时停止，false启动
            {
                miniForm.windowsServerManager.StopService("HAYC Speech Service");
            }
            else
            {
                miniForm.windowsServerManager.StartService("HAYC Speech Service");
            }
        }

        private void sb_face_ValueChanged(object sender, EventArgs e)
        {

        }

        private void timer_service_state_Tick(object sender, EventArgs e)
        {
            var serviceIsRunning = miniForm.windowsServerManager.ServiceIsRunning("HAYC Speech Service");
            sb_speech.Value = !serviceIsRunning;
            if (serviceIsRunning)
            {
                lbl_speech_state.Text = "运行中";
                lbl_speech_state.ForeColor = Color.Green;
            }
            else
            {
                lbl_speech_state.Text = "已停止";
                lbl_speech_state.ForeColor = Color.Red;
            }
        }

        private void BigForm_Deactivate(object sender, EventArgs e)
        {
            timer_service_state.Enabled = false;
        }

        private void hideMessageFormTimer_Tick(object sender, EventArgs e)
        {
            hideMessageFormTimer.Enabled = false;
            if (!isMouseEnter && !miniForm.isMouseEnter)
            {
                this.Hide();
            }
        }
    }
}
