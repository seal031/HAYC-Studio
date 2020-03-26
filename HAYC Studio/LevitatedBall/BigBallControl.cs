using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace HAYC_Studio.LevitatedBall
{
    public partial class BallControl : Control
    {
        //private MemoryInfo memoryInfo = new MemoryInfo();

        public BallControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = this.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new SolidBrush(Color.FromArgb(212, 212, 212));
            g.FillEllipse(brush, 0, 0, 70, 70);
            brush = new SolidBrush(Color.FromArgb(169, 169, 169));
            g.FillEllipse(brush, 3, 3, 62, 62);
            brush = new SolidBrush(Color.GreenYellow);//填充的颜色
            g.FillEllipse(brush, 3, 3, 65, 65);

            //brush = new SolidBrush(Color.FromArgb(75, 75, 75));//填充的颜色
            ////g.DrawString(memoryInfo.getUsedMemoryRate(), new Font("微软雅黑", 20), brush, new PointF(18, 35));
            //g.DrawString("%", new Font("微软雅黑", 8), brush, new PointF(53, 39));
            //g.DrawString("已用", new Font("微软雅黑", 8), brush, new PointF(53, 51));
            g.Dispose();
        }

    }
}
