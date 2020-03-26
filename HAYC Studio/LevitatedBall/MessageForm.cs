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
    public partial class MessageForm : Form
    {
        public enum ShowCommands
        {
            HIDE = 1,
        }

        private bool isMouseDown = false;
        public bool isMouseEnter = false;

        private Point mouseOffset;
        private LevitateBall miniForm;

        public MessageForm(LevitateBall miniForm)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.miniForm = miniForm;
        }

        public void setMessage(string message)
        {
            this.lbl_message.Text = message;
            this.Refresh();
        }
    }
}
