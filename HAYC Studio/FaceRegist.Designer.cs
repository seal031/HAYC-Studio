namespace HAYC_Studio
{
    partial class FaceRegist
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pb_image = new System.Windows.Forms.PictureBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.lbl_uploadImage = new DevComponents.DotNetBar.LabelX();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.lbl_openCamera = new DevComponents.DotNetBar.LabelX();
            this.panel_camera = new DevComponents.DotNetBar.PanelEx();
            this.ll_closeCamera = new DevComponents.DotNetBar.LabelX();
            this.lbl_catchImage = new DevComponents.DotNetBar.LabelX();
            this.pb_image_local = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).BeginInit();
            this.panel_camera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_image_local)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_image
            // 
            this.pb_image.ImageLocation = "";
            this.pb_image.Location = new System.Drawing.Point(29, 53);
            this.pb_image.Name = "pb_image";
            this.pb_image.Size = new System.Drawing.Size(430, 358);
            this.pb_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_image.TabIndex = 0;
            this.pb_image.TabStop = false;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(163, 431);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 29);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "确定";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(244, 431);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 29);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 2;
            this.buttonX2.Text = "取消";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // lbl_uploadImage
            // 
            this.lbl_uploadImage.AutoSize = true;
            // 
            // 
            // 
            this.lbl_uploadImage.BackgroundStyle.Class = "";
            this.lbl_uploadImage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbl_uploadImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_uploadImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_uploadImage.Location = new System.Drawing.Point(29, 13);
            this.lbl_uploadImage.Name = "lbl_uploadImage";
            this.lbl_uploadImage.Size = new System.Drawing.Size(131, 24);
            this.lbl_uploadImage.TabIndex = 3;
            this.lbl_uploadImage.Text = "点击上传本地照片";
            this.lbl_uploadImage.Click += new System.EventHandler(this.lbl_uploadImage_Click);
            // 
            // ofd
            // 
            this.ofd.Filter = "*.jpg|*.png";
            // 
            // lbl_openCamera
            // 
            this.lbl_openCamera.AutoSize = true;
            // 
            // 
            // 
            this.lbl_openCamera.BackgroundStyle.Class = "";
            this.lbl_openCamera.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbl_openCamera.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_openCamera.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_openCamera.Location = new System.Drawing.Point(343, 13);
            this.lbl_openCamera.Name = "lbl_openCamera";
            this.lbl_openCamera.Size = new System.Drawing.Size(116, 24);
            this.lbl_openCamera.TabIndex = 4;
            this.lbl_openCamera.Text = "点击打开摄像头";
            this.lbl_openCamera.Click += new System.EventHandler(this.lbl_openCamera_Click);
            // 
            // panel_camera
            // 
            this.panel_camera.CanvasColor = System.Drawing.SystemColors.Control;
            this.panel_camera.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panel_camera.Controls.Add(this.ll_closeCamera);
            this.panel_camera.Controls.Add(this.lbl_catchImage);
            this.panel_camera.Location = new System.Drawing.Point(32, 8);
            this.panel_camera.Name = "panel_camera";
            this.panel_camera.Size = new System.Drawing.Size(427, 35);
            this.panel_camera.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panel_camera.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panel_camera.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panel_camera.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panel_camera.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panel_camera.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panel_camera.Style.GradientAngle = 90;
            this.panel_camera.TabIndex = 5;
            this.panel_camera.Visible = false;
            // 
            // ll_closeCamera
            // 
            this.ll_closeCamera.AutoSize = true;
            // 
            // 
            // 
            this.ll_closeCamera.BackgroundStyle.Class = "";
            this.ll_closeCamera.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ll_closeCamera.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ll_closeCamera.Location = new System.Drawing.Point(335, 7);
            this.ll_closeCamera.Name = "ll_closeCamera";
            this.ll_closeCamera.Size = new System.Drawing.Size(85, 21);
            this.ll_closeCamera.TabIndex = 1;
            this.ll_closeCamera.Text = "关闭摄像头";
            this.ll_closeCamera.Click += new System.EventHandler(this.ll_closeCamera_Click);
            // 
            // lbl_catchImage
            // 
            this.lbl_catchImage.AutoSize = true;
            // 
            // 
            // 
            this.lbl_catchImage.BackgroundStyle.Class = "";
            this.lbl_catchImage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbl_catchImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_catchImage.Location = new System.Drawing.Point(4, 7);
            this.lbl_catchImage.Name = "lbl_catchImage";
            this.lbl_catchImage.Size = new System.Drawing.Size(69, 21);
            this.lbl_catchImage.TabIndex = 0;
            this.lbl_catchImage.Text = "截取图像";
            this.lbl_catchImage.Click += new System.EventHandler(this.lbl_catchImage_Click);
            // 
            // pb_image_local
            // 
            this.pb_image_local.ImageLocation = "";
            this.pb_image_local.Location = new System.Drawing.Point(28, 54);
            this.pb_image_local.Name = "pb_image_local";
            this.pb_image_local.Size = new System.Drawing.Size(430, 358);
            this.pb_image_local.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_image_local.TabIndex = 6;
            this.pb_image_local.TabStop = false;
            // 
            // FaceRegist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 481);
            this.Controls.Add(this.pb_image_local);
            this.Controls.Add(this.pb_image);
            this.Controls.Add(this.panel_camera);
            this.Controls.Add(this.lbl_openCamera);
            this.Controls.Add(this.lbl_uploadImage);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "FaceRegist";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "人脸本地注册";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FaceRegist_FormClosing);
            this.Shown += new System.EventHandler(this.FaceRegist_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).EndInit();
            this.panel_camera.ResumeLayout(false);
            this.panel_camera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_image_local)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_image;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.LabelX lbl_uploadImage;
        private System.Windows.Forms.OpenFileDialog ofd;
        private DevComponents.DotNetBar.LabelX lbl_openCamera;
        private DevComponents.DotNetBar.PanelEx panel_camera;
        private DevComponents.DotNetBar.LabelX lbl_catchImage;
        private DevComponents.DotNetBar.LabelX ll_closeCamera;
        private System.Windows.Forms.PictureBox pb_image_local;
    }
}