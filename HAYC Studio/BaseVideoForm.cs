using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAYC_Studio
{
    public interface IVideoForm
    {
        void showVideo(params object[] paramList);
    }

    public abstract class BaseVideoForm : Form, IVideoForm
    {
        public abstract void showVideo(params object[] paramList);
        public abstract void RedrawBorder(object tag);

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseVideoForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "BaseVideoForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }
    }
}
