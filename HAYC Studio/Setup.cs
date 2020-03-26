using DevComponents.DotNetBar;
using HAYC_Studio.LevitatedBall;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAYC_Studio
{
    public partial class Setup : Form
    {
        ConfigInfoSpeech configInfoSpeech;
        ConfigInfoMain configInfoMain;
        ConfigInfoFace configInfoFace;
        private LevitateBall miniForm;

        public Setup(LevitateBall miniForm)
        {
            InitializeComponent();
            this.miniForm = miniForm;
        }

        private void stc_SelectedTabChanged(object sender, DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs e)
        {
            switch (e.NewValue.Text)
            {
                case "通用":
                    loadCommonConfig();
                    break;
                case "语音识别":
                    loadSpeechConfig();
                    break;
                case "人脸识别":
                    loadFaceConfig();
                    break;
                default:
                    break;
            }
        }

        private void loadSpeechConfig()
        {
            try
            {
                configInfoSpeech = ConfigInfoSpeech.LoadConfig();
                sb_speechAutoStart.Value = configInfoSpeech.speechAutoStart == 0 ? false : true;
                kc_micSensitivity.Value = configInfoSpeech.micSensitivity;
                ii_sensitivity.Value = configInfoSpeech.micSensitivity;
                ii_BufferMilliseconds.Value = configInfoSpeech.BufferMilliseconds;
                ii_EndPickAdditional.Value = configInfoSpeech.EndPickAdditional;
                ii_MicVolumnPickerSleepSecond.Value = configInfoSpeech.MicVolumnPickerSleepSecond;
                ii_UnKownTimes.Value = configInfoSpeech.UnKownTimes;

                if (miniForm != null)
                {
                    timer_speech_state.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadCommonConfig()
        {
            try
            {
                timer_speech_state.Enabled = false;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadFaceConfig()
        {
            try
            {
                timer_speech_state.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_speech_ok_Click(object sender, EventArgs e)
        {
            try
            {
                configInfoSpeech.BufferMilliseconds = ii_BufferMilliseconds.Value;
                configInfoSpeech.EndPickAdditional = ii_EndPickAdditional.Value;
                configInfoSpeech.micSensitivity = ii_sensitivity.Value;
                configInfoSpeech.MicVolumnPickerSleepSecond = ii_MicVolumnPickerSleepSecond.Value;
                configInfoSpeech.speechAutoStart = sb_speechAutoStart.Value ? 1 : 0;
                configInfoSpeech.UnKownTimes = ii_UnKownTimes.Value;
                ConfigInfoSpeech.SaveConfig(configInfoSpeech);
                MessageBox.Show("保存成功");
                PipeCommunicateServerWorker.sendSetting(configInfoSpeech);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_speech_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kc_micSensitivity_ValueChanged(object sender, DevComponents.Instrumentation.ValueChangedEventArgs e)
        {
            ii_sensitivity.Value = (int)(e.NewValue);
        }

        private void sb_speechState_ValueChanged(object sender, EventArgs e)
        {
            var value = sb_speechState.Value;
            if (value) //true时停止，false启动
            {
                miniForm.windowsServerManager.StopService("HAYC Speech Service");
            }
            else
            {
                miniForm.windowsServerManager.StartService("HAYC Speech Service");
            }
        }

        private void timer_speech_state_Tick(object sender, EventArgs e)
        {
            var speechServiceState = miniForm.windowsServerManager.ServiceIsRunning("HAYC Speech Service");
            sb_speechState.Value = !speechServiceState;
            if (speechServiceState)
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

        private void Setup_Shown(object sender, EventArgs e)
        {
            stc.SelectedTab = (SuperTabItem)(stc.Tabs[0]);
        }
    }
}
