using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace HAYC_Studio.LevitatedBall
{
    public partial class MainBallControl : Control
    {
        public MainBallControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);         
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Brush brush = new SolidBrush(Color.FromArgb(78, 78, 78));
            Rectangle rect = new Rectangle(0, 0, 95, 44);
            GraphicsPath path = CreateRoundedRectanglePath(rect, 22);
            g.FillPath(brush, path); 

            brush = new SolidBrush(Color.WhiteSmoke);
            rect = new Rectangle(0, 0, 95,44);
            path = CreateRoundedRectanglePath(rect, 22);
            g.FillPath(brush, path);            

            brush = new SolidBrush(Color.FromArgb(51, 54, 156));
            g.FillEllipse(brush, 2, 2, 40, 40);

            brush = new SolidBrush(Color.DodgerBlue);//填充的颜色         
            g.FillEllipse(brush, 4, 4, 36, 36);

            g.Dispose();
        }

        public static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            int diameter = cornerRadius*2;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            // 左上角
            path.AddArc(arcRect, 180, 90);

            // 右上角
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);

            // 右下角
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);

            // 左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();//闭合曲线
            return path;
        }
    }
}
