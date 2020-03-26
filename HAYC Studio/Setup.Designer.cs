namespace HAYC_Studio
{
    partial class Setup
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
            this.components = new System.ComponentModel.Container();
            this.stc = new DevComponents.DotNetBar.SuperTabControl();
            this.stcp_speech = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.lbl_speech_state = new System.Windows.Forms.Label();
            this.sb_speechState = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.ii_UnKownTimes = new DevComponents.Editors.IntegerInput();
            this.ii_MicVolumnPickerSleepSecond = new DevComponents.Editors.IntegerInput();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ii_EndPickAdditional = new DevComponents.Editors.IntegerInput();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_speech_cancel = new DevComponents.DotNetBar.ButtonX();
            this.btn_speech_ok = new DevComponents.DotNetBar.ButtonX();
            this.ii_BufferMilliseconds = new DevComponents.Editors.IntegerInput();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ii_sensitivity = new DevComponents.Editors.IntegerInput();
            this.sb_speechAutoStart = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.kc_micSensitivity = new DevComponents.Instrumentation.KnobControl();
            this.sti_speech = new DevComponents.DotNetBar.SuperTabItem();
            this.stcp_common = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.sti_common = new DevComponents.DotNetBar.SuperTabItem();
            this.stcp_face = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.switchButton3 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.switchButton4 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.sti_face = new DevComponents.DotNetBar.SuperTabItem();
            this.timer_speech_state = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.stc)).BeginInit();
            this.stc.SuspendLayout();
            this.stcp_speech.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ii_UnKownTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ii_MicVolumnPickerSleepSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ii_EndPickAdditional)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ii_BufferMilliseconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ii_sensitivity)).BeginInit();
            this.stcp_common.SuspendLayout();
            this.stcp_face.SuspendLayout();
            this.SuspendLayout();
            // 
            // stc
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.stc.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.stc.ControlBox.MenuBox.Name = "";
            this.stc.ControlBox.Name = "";
            this.stc.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.stc.ControlBox.MenuBox,
            this.stc.ControlBox.CloseBox});
            this.stc.Controls.Add(this.stcp_common);
            this.stc.Controls.Add(this.stcp_face);
            this.stc.Controls.Add(this.stcp_speech);
            this.stc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stc.Location = new System.Drawing.Point(0, 0);
            this.stc.Name = "stc";
            this.stc.ReorderTabsEnabled = true;
            this.stc.SelectedTabFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.stc.SelectedTabIndex = 1;
            this.stc.Size = new System.Drawing.Size(882, 653);
            this.stc.TabFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stc.TabIndex = 0;
            this.stc.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.sti_common,
            this.sti_speech,
            this.sti_face});
            this.stc.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.OneNote2007;
            this.stc.Text = "superTabControl1";
            this.stc.TextAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
            this.stc.SelectedTabChanged += new System.EventHandler<DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs>(this.stc_SelectedTabChanged);
            // 
            // stcp_speech
            // 
            this.stcp_speech.Controls.Add(this.lbl_speech_state);
            this.stcp_speech.Controls.Add(this.sb_speechState);
            this.stcp_speech.Controls.Add(this.ii_UnKownTimes);
            this.stcp_speech.Controls.Add(this.ii_MicVolumnPickerSleepSecond);
            this.stcp_speech.Controls.Add(this.label9);
            this.stcp_speech.Controls.Add(this.label8);
            this.stcp_speech.Controls.Add(this.ii_EndPickAdditional);
            this.stcp_speech.Controls.Add(this.label7);
            this.stcp_speech.Controls.Add(this.btn_speech_cancel);
            this.stcp_speech.Controls.Add(this.btn_speech_ok);
            this.stcp_speech.Controls.Add(this.ii_BufferMilliseconds);
            this.stcp_speech.Controls.Add(this.label6);
            this.stcp_speech.Controls.Add(this.label5);
            this.stcp_speech.Controls.Add(this.label4);
            this.stcp_speech.Controls.Add(this.ii_sensitivity);
            this.stcp_speech.Controls.Add(this.sb_speechAutoStart);
            this.stcp_speech.Controls.Add(this.label3);
            this.stcp_speech.Controls.Add(this.label2);
            this.stcp_speech.Controls.Add(this.label1);
            this.stcp_speech.Controls.Add(this.kc_micSensitivity);
            this.stcp_speech.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stcp_speech.Location = new System.Drawing.Point(0, 32);
            this.stcp_speech.Name = "stcp_speech";
            this.stcp_speech.Size = new System.Drawing.Size(882, 621);
            this.stcp_speech.TabIndex = 0;
            this.stcp_speech.TabItem = this.sti_speech;
            // 
            // lbl_speech_state
            // 
            this.lbl_speech_state.AutoSize = true;
            this.lbl_speech_state.BackColor = System.Drawing.Color.Transparent;
            this.lbl_speech_state.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_speech_state.Location = new System.Drawing.Point(232, 26);
            this.lbl_speech_state.Name = "lbl_speech_state";
            this.lbl_speech_state.Size = new System.Drawing.Size(13, 19);
            this.lbl_speech_state.TabIndex = 19;
            this.lbl_speech_state.Text = " ";
            // 
            // sb_speechState
            // 
            // 
            // 
            // 
            this.sb_speechState.BackgroundStyle.Class = "";
            this.sb_speechState.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sb_speechState.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sb_speechState.Location = new System.Drawing.Point(295, 26);
            this.sb_speechState.Name = "sb_speechState";
            this.sb_speechState.OffText = "停止";
            this.sb_speechState.OnText = "启动";
            this.sb_speechState.Size = new System.Drawing.Size(81, 20);
            this.sb_speechState.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sb_speechState.TabIndex = 18;
            this.sb_speechState.Value = true;
            this.sb_speechState.ValueChanged += new System.EventHandler(this.sb_speechState_ValueChanged);
            // 
            // ii_UnKownTimes
            // 
            // 
            // 
            // 
            this.ii_UnKownTimes.BackgroundStyle.Class = "DateTimeInputBackground";
            this.ii_UnKownTimes.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ii_UnKownTimes.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.ii_UnKownTimes.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ii_UnKownTimes.Location = new System.Drawing.Point(526, 521);
            this.ii_UnKownTimes.MaxValue = 10;
            this.ii_UnKownTimes.MinValue = 1;
            this.ii_UnKownTimes.Name = "ii_UnKownTimes";
            this.ii_UnKownTimes.ShowUpDown = true;
            this.ii_UnKownTimes.Size = new System.Drawing.Size(63, 27);
            this.ii_UnKownTimes.TabIndex = 17;
            this.ii_UnKownTimes.Value = 3;
            // 
            // ii_MicVolumnPickerSleepSecond
            // 
            // 
            // 
            // 
            this.ii_MicVolumnPickerSleepSecond.BackgroundStyle.Class = "DateTimeInputBackground";
            this.ii_MicVolumnPickerSleepSecond.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ii_MicVolumnPickerSleepSecond.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.ii_MicVolumnPickerSleepSecond.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ii_MicVolumnPickerSleepSecond.Increment = 5;
            this.ii_MicVolumnPickerSleepSecond.Location = new System.Drawing.Point(427, 487);
            this.ii_MicVolumnPickerSleepSecond.MaxValue = 600;
            this.ii_MicVolumnPickerSleepSecond.MinValue = 30;
            this.ii_MicVolumnPickerSleepSecond.Name = "ii_MicVolumnPickerSleepSecond";
            this.ii_MicVolumnPickerSleepSecond.ShowUpDown = true;
            this.ii_MicVolumnPickerSleepSecond.Size = new System.Drawing.Size(75, 27);
            this.ii_MicVolumnPickerSleepSecond.TabIndex = 16;
            this.ii_MicVolumnPickerSleepSecond.Value = 300;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(223, 526);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(268, 20);
            this.label9.TabIndex = 15;
            this.label9.Text = "连续识别几次失败后自动休眠(默认值3)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(223, 487);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(181, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "自动休眠时间(默认300秒)";
            // 
            // ii_EndPickAdditional
            // 
            // 
            // 
            // 
            this.ii_EndPickAdditional.BackgroundStyle.Class = "DateTimeInputBackground";
            this.ii_EndPickAdditional.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ii_EndPickAdditional.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.ii_EndPickAdditional.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ii_EndPickAdditional.Location = new System.Drawing.Point(427, 448);
            this.ii_EndPickAdditional.MaxValue = 7;
            this.ii_EndPickAdditional.MinValue = 3;
            this.ii_EndPickAdditional.Name = "ii_EndPickAdditional";
            this.ii_EndPickAdditional.ShowUpDown = true;
            this.ii_EndPickAdditional.Size = new System.Drawing.Size(75, 27);
            this.ii_EndPickAdditional.TabIndex = 14;
            this.ii_EndPickAdditional.Value = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(223, 450);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(163, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "声音采集后延(默认值5)";
            // 
            // btn_speech_cancel
            // 
            this.btn_speech_cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_speech_cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_speech_cancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_speech_cancel.Location = new System.Drawing.Point(754, 573);
            this.btn_speech_cancel.Name = "btn_speech_cancel";
            this.btn_speech_cancel.Size = new System.Drawing.Size(90, 22);
            this.btn_speech_cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_speech_cancel.TabIndex = 12;
            this.btn_speech_cancel.Text = "取 消";
            this.btn_speech_cancel.Click += new System.EventHandler(this.btn_speech_cancel_Click);
            // 
            // btn_speech_ok
            // 
            this.btn_speech_ok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_speech_ok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_speech_ok.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_speech_ok.Location = new System.Drawing.Point(637, 573);
            this.btn_speech_ok.Name = "btn_speech_ok";
            this.btn_speech_ok.Size = new System.Drawing.Size(90, 22);
            this.btn_speech_ok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_speech_ok.TabIndex = 11;
            this.btn_speech_ok.Text = "确 定";
            this.btn_speech_ok.Click += new System.EventHandler(this.btn_speech_ok_Click);
            // 
            // ii_BufferMilliseconds
            // 
            // 
            // 
            // 
            this.ii_BufferMilliseconds.BackgroundStyle.Class = "DateTimeInputBackground";
            this.ii_BufferMilliseconds.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ii_BufferMilliseconds.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.ii_BufferMilliseconds.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ii_BufferMilliseconds.Increment = 5;
            this.ii_BufferMilliseconds.Location = new System.Drawing.Point(427, 407);
            this.ii_BufferMilliseconds.MaxValue = 200;
            this.ii_BufferMilliseconds.MinValue = 20;
            this.ii_BufferMilliseconds.Name = "ii_BufferMilliseconds";
            this.ii_BufferMilliseconds.ShowUpDown = true;
            this.ii_BufferMilliseconds.Size = new System.Drawing.Size(75, 27);
            this.ii_BufferMilliseconds.TabIndex = 10;
            this.ii_BufferMilliseconds.Value = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(223, 409);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(187, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "声音采集频率(默认25毫秒)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(33, 409);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "高级设置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(529, 325);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "灵敏度值";
            // 
            // ii_sensitivity
            // 
            // 
            // 
            // 
            this.ii_sensitivity.BackgroundStyle.Class = "DateTimeInputBackground";
            this.ii_sensitivity.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ii_sensitivity.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.ii_sensitivity.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ii_sensitivity.Location = new System.Drawing.Point(604, 320);
            this.ii_sensitivity.MaxValue = 100;
            this.ii_sensitivity.MinValue = 0;
            this.ii_sensitivity.Name = "ii_sensitivity";
            this.ii_sensitivity.Size = new System.Drawing.Size(59, 27);
            this.ii_sensitivity.TabIndex = 6;
            // 
            // sb_speechAutoStart
            // 
            // 
            // 
            // 
            this.sb_speechAutoStart.BackgroundStyle.Class = "";
            this.sb_speechAutoStart.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sb_speechAutoStart.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sb_speechAutoStart.Location = new System.Drawing.Point(227, 79);
            this.sb_speechAutoStart.Name = "sb_speechAutoStart";
            this.sb_speechAutoStart.OffText = "手动启动";
            this.sb_speechAutoStart.OnText = "自动启动";
            this.sb_speechAutoStart.Size = new System.Drawing.Size(149, 22);
            this.sb_speechAutoStart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sb_speechAutoStart.TabIndex = 5;
            this.sb_speechAutoStart.Value = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(33, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "登录自动启动服务";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(33, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "服务运行状态";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(33, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "麦克风灵敏度";
            // 
            // kc_micSensitivity
            // 
            this.kc_micSensitivity.BackColor = System.Drawing.Color.Transparent;
            this.kc_micSensitivity.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.kc_micSensitivity.Location = new System.Drawing.Point(227, 137);
            this.kc_micSensitivity.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.kc_micSensitivity.Name = "kc_micSensitivity";
            this.kc_micSensitivity.Size = new System.Drawing.Size(295, 238);
            this.kc_micSensitivity.TabIndex = 0;
            this.kc_micSensitivity.Text = "knobControl1";
            this.kc_micSensitivity.ValueChanged += new System.EventHandler<DevComponents.Instrumentation.ValueChangedEventArgs>(this.kc_micSensitivity_ValueChanged);
            // 
            // sti_speech
            // 
            this.sti_speech.AttachedControl = this.stcp_speech;
            this.sti_speech.GlobalItem = false;
            this.sti_speech.Name = "sti_speech";
            this.sti_speech.Text = "语音识别";
            // 
            // stcp_common
            // 
            this.stcp_common.Controls.Add(this.label12);
            this.stcp_common.Controls.Add(this.label13);
            this.stcp_common.Controls.Add(this.label14);
            this.stcp_common.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stcp_common.Location = new System.Drawing.Point(0, 32);
            this.stcp_common.Name = "stcp_common";
            this.stcp_common.Size = new System.Drawing.Size(882, 621);
            this.stcp_common.TabIndex = 1;
            this.stcp_common.TabItem = this.sti_common;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(33, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(129, 19);
            this.label12.TabIndex = 6;
            this.label12.Text = "登录自动启动服务";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(33, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(99, 19);
            this.label13.TabIndex = 5;
            this.label13.Text = "服务运行状态";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(33, 137);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(99, 19);
            this.label14.TabIndex = 4;
            this.label14.Text = "麦克风灵敏度";
            // 
            // sti_common
            // 
            this.sti_common.AttachedControl = this.stcp_common;
            this.sti_common.GlobalItem = false;
            this.sti_common.Name = "sti_common";
            this.sti_common.Text = "通用";
            // 
            // stcp_face
            // 
            this.stcp_face.Controls.Add(this.switchButton3);
            this.stcp_face.Controls.Add(this.switchButton4);
            this.stcp_face.Controls.Add(this.label10);
            this.stcp_face.Controls.Add(this.label11);
            this.stcp_face.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stcp_face.Location = new System.Drawing.Point(0, 32);
            this.stcp_face.Name = "stcp_face";
            this.stcp_face.Size = new System.Drawing.Size(882, 621);
            this.stcp_face.TabIndex = 0;
            this.stcp_face.TabItem = this.sti_face;
            // 
            // switchButton3
            // 
            // 
            // 
            // 
            this.switchButton3.BackgroundStyle.Class = "";
            this.switchButton3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.switchButton3.Location = new System.Drawing.Point(227, 79);
            this.switchButton3.Name = "switchButton3";
            this.switchButton3.OffText = "手动启动";
            this.switchButton3.OnText = "自动启动";
            this.switchButton3.Size = new System.Drawing.Size(149, 22);
            this.switchButton3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton3.TabIndex = 9;
            this.switchButton3.Value = true;
            // 
            // switchButton4
            // 
            // 
            // 
            // 
            this.switchButton4.BackgroundStyle.Class = "";
            this.switchButton4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.switchButton4.Location = new System.Drawing.Point(227, 26);
            this.switchButton4.Name = "switchButton4";
            this.switchButton4.OffText = "服务停止";
            this.switchButton4.OnText = "服务运行";
            this.switchButton4.Size = new System.Drawing.Size(149, 22);
            this.switchButton4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton4.TabIndex = 8;
            this.switchButton4.Value = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(33, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 19);
            this.label10.TabIndex = 7;
            this.label10.Text = "登录自动启动服务";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(33, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 19);
            this.label11.TabIndex = 6;
            this.label11.Text = "服务运行状态";
            // 
            // sti_face
            // 
            this.sti_face.AttachedControl = this.stcp_face;
            this.sti_face.GlobalItem = false;
            this.sti_face.Name = "sti_face";
            this.sti_face.Text = "人脸识别";
            // 
            // timer_speech_state
            // 
            this.timer_speech_state.Interval = 1000;
            this.timer_speech_state.Tick += new System.EventHandler(this.timer_speech_state_Tick);
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 653);
            this.Controls.Add(this.stc);
            this.MaximizeBox = false;
            this.Name = "Setup";
            this.ShowIcon = false;
            this.Text = "系统设置";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Setup_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.stc)).EndInit();
            this.stc.ResumeLayout(false);
            this.stcp_speech.ResumeLayout(false);
            this.stcp_speech.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ii_UnKownTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ii_MicVolumnPickerSleepSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ii_EndPickAdditional)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ii_BufferMilliseconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ii_sensitivity)).EndInit();
            this.stcp_common.ResumeLayout(false);
            this.stcp_common.PerformLayout();
            this.stcp_face.ResumeLayout(false);
            this.stcp_face.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperTabControl stc;
        private DevComponents.DotNetBar.SuperTabControlPanel stcp_common;
        private DevComponents.DotNetBar.SuperTabItem sti_common;
        private DevComponents.DotNetBar.SuperTabControlPanel stcp_speech;
        private DevComponents.DotNetBar.SuperTabItem sti_speech;
        private DevComponents.DotNetBar.SuperTabControlPanel stcp_face;
        private DevComponents.DotNetBar.SuperTabItem sti_face;
        private DevComponents.Instrumentation.KnobControl kc_micSensitivity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.SwitchButton sb_speechAutoStart;
        private System.Windows.Forms.Label label4;
        private DevComponents.Editors.IntegerInput ii_sensitivity;
        private System.Windows.Forms.Label label5;
        private DevComponents.Editors.IntegerInput ii_BufferMilliseconds;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.ButtonX btn_speech_cancel;
        private DevComponents.DotNetBar.ButtonX btn_speech_ok;
        private DevComponents.Editors.IntegerInput ii_EndPickAdditional;
        private System.Windows.Forms.Label label7;
        private DevComponents.Editors.IntegerInput ii_UnKownTimes;
        private DevComponents.Editors.IntegerInput ii_MicVolumnPickerSleepSecond;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton3;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbl_speech_state;
        private DevComponents.DotNetBar.Controls.SwitchButton sb_speechState;
        private System.Windows.Forms.Timer timer_speech_state;
    }
}