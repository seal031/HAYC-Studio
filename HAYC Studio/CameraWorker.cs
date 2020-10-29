using OpenCvSharp;
using OpenNIWrapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAYC_Studio
{
    public class CameraWorker
    {
        private Bitmap bitmap = new Bitmap(1, 1);
        private Form form;
        private PictureBox pb_image;
        private Device currentDevice;
        private VideoMode videoMode;
        private VideoStream currentSensor;
        CascadeClassifier face_cascade = new CascadeClassifier(@"haarcascade_frontalface_alt.xml");

        public CameraWorker(Form form,PictureBox pb,Bitmap bitmap)
        {
            this.form = form;
            this.pb_image = pb;
            this.bitmap = bitmap;
            HandleError(OpenNI.Initialize());
            currentDevice = OpenNI.EnumerateDevices()[0].OpenDevice();
            currentSensor = currentDevice.CreateVideoStream(Device.SensorType.Color);
            videoMode = currentSensor.SensorInfo.GetSupportedVideoModes().First(m => m.DataPixelFormat == VideoMode.PixelFormat.Rgb888 && m.Resolution.Height == 480 && m.Resolution.Width == 640 && m.Fps == 30);
        }
        private void HandleError(OpenNI.Status status)
        {
            if (status == OpenNI.Status.Ok)
            {
                return;
            }
            MessageBox.Show(string.Format(@"Error: {0} - {1}", status, OpenNI.LastError),@"Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        /// <summary>
        /// 开启摄像头
        /// </summary>
        public void OpenCamera()
        {
            if (this.currentSensor != null && this.currentSensor.IsValid)
            {
                this.currentSensor.Stop();
                this.currentSensor.OnNewFrame -= this.CurrentSensorOnNewFrame;
                this.currentSensor.VideoMode = videoMode;

                if (this.currentSensor.Start() == OpenNI.Status.Ok)
                {
                    this.currentSensor.OnNewFrame += this.CurrentSensorOnNewFrame;
                }
                else
                {
                    MessageBox.Show(@"Failed to start stream.");
                }
            }
        }

        public void ShutDown()
        {
            OpenNI.Shutdown();
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
                        if (!this.pb_image.Visible)
                        {
                            form.Invalidate();
                        }
                    }
                }
            }
        }

        public Mat DetectFace(Mat src)
        {
            Mat result;
            using (var gray = new Mat())
            {
                result = src.Clone();
                try
                {
                    Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
                    Rect[] faces = face_cascade.DetectMultiScale(gray, 1.08, 2, HaarDetectionType.ScaleImage, new OpenCvSharp.Size(30, 30));
                    foreach (Rect face in faces)
                    {
                        var center = new OpenCvSharp.Point
                        {
                            X = (int)(face.X + face.Width * 0.5),
                            Y = (int)(face.Y + face.Height * 0.5)
                        };
                        var axes = new OpenCvSharp.Size
                        {
                            Width = (int)(face.Width * 0.5),
                            Height = (int)(face.Height * 0.5)
                        };
                        Cv2.Ellipse(result, center, axes, 0, 0, 360, new Scalar(0, 255, 0), 4);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return result;
        }
    }
}
