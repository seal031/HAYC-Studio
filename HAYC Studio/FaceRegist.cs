using HAYC_ProcessCommunicate_Library;
using OpenNIWrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAYC_Studio
{
    public partial class FaceRegist : DevComponents.DotNetBar.Office2007Form
    {
        string fileName;
        public FaceRegist()
        {
            InitializeComponent();
        }

        private void FaceRegist_Shown(object sender, EventArgs e)
        {
            SetDouble(this);
            SetDouble(pb_image);
        }

        private void FaceRegist_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (pb_image.Image == null && pb_image.ImageLocation == string.Empty && pb_image_local.Image == null && pb_image_local.ImageLocation == string.Empty)
            {
                MessageBox.Show("您还没有上传人脸照片");
            }
            else
            {
                ProcessCommunicateMessage message = new ProcessCommunicateMessage();
                message.ProcessName = "";
                message.MessageType = CommunicateMessageType.FEAT;
                message.Message = fileName;
                FacePipeCommunicateServerWorker.sendMessage(message.toJson());
                MessageBox.Show("处理完成");
                this.Close();
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbl_uploadImage_Click(object sender, EventArgs e)
        {
            ofd.Filter = "图片文件|*.jpg|PNG图片|*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileName = ofd.FileName;
                pb_image_local.Image = Image.FromFile(ofd.FileName);
                //pb_image_local.Refresh();
            }
        }

        private void lbl_openCamera_Click(object sender, EventArgs e)
        {
            initCamera();
            if (OpenCamera())
            {
                panel_camera.Visible = true;
                pb_image_local.Visible = false;
            }
        }

        private void lbl_catchImage_Click(object sender, EventArgs e)
        {
            if (CloseCamera())
            {
                fileName = pickAndSaveFaceImage();
                panel_camera.Visible = false;
                pb_image.ImageLocation = fileName;
            }
        }

        private void ll_closeCamera_Click(object sender, EventArgs e)
        {
            if (CloseCamera())
            {
                panel_camera.Visible = false;
                pb_image_local.Visible = true;
                pb_image.Image = null;
            }
        }

        #region 摄像头图像控制
        private Bitmap bitmap = new Bitmap(1, 1);
        private string tempJpgPath = Application.StartupPath + "\\faceRegist";
        private Device currentDevice;
        private VideoMode videoMode;
        private VideoStream currentSensor;
        private void initCamera()
        {
            HandleError(OpenNI.Initialize());
            var cameraCount = OpenNI.EnumerateDevices().Length;
            if (cameraCount > 0)
            {
                currentDevice = OpenNI.EnumerateDevices()[0].OpenDevice();
                currentSensor = currentDevice.CreateVideoStream(Device.SensorType.Color);
                videoMode = currentSensor.SensorInfo.GetSupportedVideoModes().First(m => m.DataPixelFormat == VideoMode.PixelFormat.Rgb888 && m.Resolution.Height == 480 && m.Resolution.Width == 640 && m.Fps == 30);
            }
            else
            {
                MessageBox.Show("未能在本机上找到视频传感器设备。请确认视频传感器设备已连接到本机，并已正确安装驱动程序", "未能找到设备", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        private void HandleError(OpenNI.Status status)
        {
            if (status == OpenNI.Status.Ok)
            {
                return;
            }
            MessageBox.Show(string.Format(@"Error: {0} - {1}", status, OpenNI.LastError), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        /// <summary>
        /// 开启摄像头
        /// </summary>
        public bool OpenCamera()
        {
            try
            {
                if (this.currentSensor != null && this.currentSensor.IsValid)
                {
                    this.currentSensor.Stop();
                    this.currentSensor.OnNewFrame -= this.CurrentSensorOnNewFrame;
                    //this.currentSensor.VideoMode = videoMode;

                    if (this.currentSensor.Start() == OpenNI.Status.Ok)
                    {
                        this.currentSensor.OnNewFrame += this.CurrentSensorOnNewFrame;
                        return true;
                    }
                    else
                    {
                        LogHelper.WriteLog(@"Failed to start stream.");
                        MessageBox.Show(@"Failed to start stream.");
                        return false;
                    }
                }
                else
                {
                    LogHelper.WriteLog("摄像头尚未初始化");
                    MessageBox.Show("摄像头尚未初始化");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("打开摄像头失败:" + ex.Message);
                MessageBox.Show("打开摄像头失败，请重试");
                return false;
            }
        }
        public bool CloseCamera()
        {
            try
            {
                if (this.currentSensor != null && this.currentSensor.IsValid)
                {
                    this.currentSensor.Stop();
                    this.currentSensor.OnNewFrame -= this.CurrentSensorOnNewFrame;
                    //this.currentSensor.VideoMode = videoMode;
                    OpenNI.Shutdown();
                    return true;
                }
                else
                {
                    LogHelper.WriteLog("摄像头尚未初始化");
                    MessageBox.Show("摄像头尚未初始化");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("关闭摄像头失败:" + ex.Message);
                MessageBox.Show("关闭摄像头失败，请重试");
                return false;
            }
        }
        private void CurrentSensorOnNewFrame(VideoStream videoStream)
        {
            if (videoStream.IsValid && videoStream.IsFrameAvailable())
            {
                using (VideoFrameRef frame = videoStream.ReadFrame())
                {
                    if (frame.IsValid)
                    {
                        VideoFrameRef.CopyBitmapOptions options = VideoFrameRef.CopyBitmapOptions.Force24BitRgb | VideoFrameRef.CopyBitmapOptions.DepthFillShadow;
                        lock (this.bitmap)
                        {
                            try
                            {
                                frame.UpdateBitmap(this.bitmap, options);
                            }
                            catch (Exception ex)
                            {
                                this.bitmap = frame.ToBitmap(options);
                            }
                            finally
                            {

                            }
                        }
                        //if (!this.pb_camera.Visible)
                        {
                            this.Invalidate();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 截图及保存
        /// </summary>
        /// <returns></returns>
        public string pickAndSaveFaceImage()
        {
            string imagePath = string.Empty;
            try
            {
                imagePath = tempJpgPath + "\\" + DateTime.Now.ToString("HHmmss") + ".jpg";
                lock (this.bitmap)
                {
                    this.bitmap.Save(imagePath, ImageFormat.Jpeg);
                }
            }
            catch (Exception)
            {
                imagePath = string.Empty;
            }
            return imagePath;
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.pb_image.Visible)
            {
                this.pb_image.Visible = false;
            }

            if (this.bitmap == null)
            {
                return;
            }

            lock (this.bitmap)
            {
                System.Drawing.Size canvasSize = this.pb_image.Size;
                System.Drawing.Point canvasPosition = pb_image.Location;

                double ratioX = canvasSize.Width / (double)this.bitmap.Width;
                double ratioY = canvasSize.Height / (double)this.bitmap.Height;
                double ratio = Math.Min(ratioX, ratioY);

                int drawWidth = Convert.ToInt32(this.bitmap.Width * ratio);
                int drawHeight = Convert.ToInt32(this.bitmap.Height * ratio);

                int drawX = canvasPosition.X + Convert.ToInt32((canvasSize.Width - drawWidth) / 2);
                int drawY = canvasPosition.Y + Convert.ToInt32((canvasSize.Height - drawHeight) / 2);

                e.Graphics.DrawImage(this.bitmap, drawX, drawY, drawWidth, drawHeight);
                //pb_image.BringToFront();
            }
        }
        /// <summary>
        /// 双缓存解决窗体闪屏问题
        /// </summary>
        /// <param name="cc"></param>
        public static void SetDouble(Control cc)
        {
            cc.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(cc, true, null);
        }
    }
}
