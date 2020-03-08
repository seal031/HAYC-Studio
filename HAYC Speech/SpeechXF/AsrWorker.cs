
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAYC_Speech_Service.Voice
{
    public static class AsrWoker
    {
        public static byte[] data;

        public static string ASR_RES_PATH = "msc/res/asr/common.jet"; //离线语法识别资源路径
        public static string GRM_BUILD_PATH = "msc/res/asr/GrmBuilld"; //构建离线语法识别网络生成数据保存路径
        public static string GRM_FILE = "call.bnf"; //构建离线识别语法网络所用的语法文件
        public static string LEX_NAME = "contact"; //更新离线识别语法的contact槽（语法文件为此示例中使用的call.bnf）
        
        public static int build_grammar(IntPtr udata)
        {
            FileStream fs = new FileStream(GRM_FILE, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string s = sr.ReadToEnd();
            sr.Close();
            fs.Close();

            byte[] data = Encoding.Default.GetBytes(s);
            uint grm_cnt_len = (uint)data.Length;

            IntPtr grm_content = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, grm_content, data.Length);

            string grm_build_params = string.Format("engine_type = local,asr_res_path = {0}, sample_rate = {1},grm_build_path = {2},", ASR_RES_PATH, DefineConstantsAsr_demo.SAMPLE_RATE_16K, GRM_BUILD_PATH);
            int ret = qisr.QISRBuildGrammar(Marshal.StringToHGlobalAnsi("bnf"), grm_content, grm_cnt_len, Marshal.StringToHGlobalAnsi(grm_build_params), build_grm_cb, udata);

            Marshal.FreeHGlobal(grm_content);
            return ret;
        }
        public static int update_lexicon(IntPtr udata)
        {
            string lex_content = "丁伟\n黄辣椒";
            uint lex_cnt_len = (uint)Encoding.Default.GetByteCount(lex_content);
            string update_lex_params = string.Format("engine_type = local, text_encoding = GB2312,asr_res_path = {0},sample_rate = {1},grm_build_path = {2},grammar_list = {3}, ", ASR_RES_PATH, DefineConstantsAsr_demo.SAMPLE_RATE_16K, GRM_BUILD_PATH, grammarId);

            int ret = qisr.QISRUpdateLexicon(LEX_NAME, lex_content, lex_cnt_len, update_lex_params, update_lex_cb, udata);
            return ret;
        }

        //private static IAudioPlayer audioPlayer;
        private static byte[] allData = new byte[] { };
        public static string run_asr(byte[] data)
        {
            //audioPlayer.Play(data);
            string asr_params = "";
            string rec_rslt = null;
            string session_id = null;
            //string asr_audiof = null;
            IntPtr pcm_data = (IntPtr)0;
            int pcm_count = 0;
            int pcm_size = 0;
            int last_audio = 0;
            int aud_stat = (int)AS.MSP_AUDIO_SAMPLE_CONTINUE;
            int ep_status = (int)EP.MSP_EP_LOOKING_FOR_SPEECH;
            int rec_status = (int)RS.MSP_REC_STATUS_INCOMPLETE;
            int rss_status = (int)RS.MSP_REC_STATUS_INCOMPLETE;
            int errcode = -1;

            //byte[] data = utils.ReadFile(asr_audiof);

            pcm_size = data.Length;
            pcm_data = Marshal.AllocHGlobal(pcm_size);
            Marshal.Copy(data, 0, pcm_data, pcm_size);

            //离线语法识别参数设置
            asr_params = string.Format("engine_type = local,asr_res_path = {0}, sample_rate = {1}, grm_build_path = {2}, local_grammar = {3}, result_type = xml, result_encoding = GB2312, asr_denoise=1", ASR_RES_PATH, DefineConstantsAsr_demo.SAMPLE_RATE_16K, GRM_BUILD_PATH, grammarId);
            IntPtr p = Marshal.StringToHGlobalAnsi(asr_params);
            IntPtr r = qisr.QISRSessionBegin(null, p, ref errcode);
            session_id = Marshal.PtrToStringAnsi(r);
            Marshal.FreeHGlobal(p);
            if (null == session_id) return string.Empty;

            Console.Write("开始识别...\n");

            p = pcm_data;
            while (true)
            {
                uint len = 6400;
                if (pcm_size < 12800)
                {
                    len = (uint)pcm_size;
                    last_audio = 1;
                }
                aud_stat = (int)AS.MSP_AUDIO_SAMPLE_CONTINUE;
                if (0 == pcm_count)
                {
                    aud_stat = (int)AS.MSP_AUDIO_SAMPLE_FIRST;
                }
                if (len <= 0)
                {
                    break;
                }
                Console.Write(">");
                //errcode = qisr.QISRAudioWrite(session_id, (IntPtr)&pcm_data[pcm_count], len, aud_stat, ref ep_status, ref rec_status);
                errcode = qisr.QISRAudioWrite(session_id, p, len, aud_stat, ref ep_status, ref rec_status);
                utils.Inc(ref p, (int)len);
                if (ERROR.MSP_SUCCESS != (ERROR)errcode)
                {
                    break;
                }
                pcm_count += (int)len;
                pcm_size -= (int)len;
                //检测到音频结束
                if (EP.MSP_EP_AFTER_SPEECH == (EP)ep_status)
                {
                    Console.Write("\n检测到音频结束：\n");
                    break;
                }
                Thread.Sleep(150); //模拟人说话时间间隙
            }
            //主动点击音频结束
            qisr.QISRAudioWrite(session_id, (IntPtr)0, 0, (int)AS.MSP_AUDIO_SAMPLE_LAST, ref ep_status, ref rec_status);

            Marshal.FreeHGlobal(pcm_data);

            //获取识别结果
            while (RS.MSP_REC_STATUS_COMPLETE != (RS)rss_status && ERROR.MSP_SUCCESS == (ERROR)errcode)
            {
                p = qisr.QISRGetResult(session_id, ref rss_status, 0, ref errcode);
                rec_rslt = Marshal.PtrToStringAnsi(p);
                Thread.Sleep(150);
            }
            Console.Write("\n识别结束：\n");
            if (null != rec_rslt)
            {
                Console.Write("{0}\n", rec_rslt);
            }
            else
            {
                //    Console.Write("没有识别结果！\n");
            }
            qisr.QISRSessionEnd(session_id, null);
            return rec_rslt;
        }

        private static int errorCode = -1;
        private static string grammarId = "";

        public static int build_grm_cb(int ecode, string info, IntPtr pdata)
        {
            errorCode = ecode;
            grammarId = info; // Marshal.PtrToStringAuto(info);

            if (ecode == 0 && null != info)
            {
                Console.Write("构建语法成功！ 语法ID:{0}\n", info);
            }
            else
            {
                Console.Write("构建语法失败！{0:D}\n", ecode);
            }

            return 0;
        }

        public static int update_lex_cb(int ecode, string info, IntPtr pdata)
        {
            errorCode = ecode;

            if (ERROR.MSP_SUCCESS == (ERROR)ecode)
            {
                Console.Write("更新词典成功！\n");
            }
            else
            {
                Console.Write("更新词典失败！{0:D}\n", ecode);
            }

            return 0;
        }
        public const string session_begin_params = "sub = iat, domain = iat, language = zh_cn, accent = mandarin, sample_rate = 16000, result_type = plain, result_encoding =gb2312, asr_denoise=1";
        public static int init()
        {
            //audioPlayer = PlayerFactory.CreateAudioPlayer(0, 16000, 1, 16, 5);
            //56089e95 ha
            //564d2cdc
            string login_config = "appid = 5e4f7a2b"; //登录参数写自己创建的语音听写的id
            int ret = 0;
            try
            {
                ret = msp_cmn.MSPLogin("", "", login_config); //第一个参数为用户名，第二个参数为密码，传NULL即可，第三个参数是登录参数
                if (ERROR.MSP_SUCCESS != (ERROR)ret)
                {
                    Console.Write("登录失败：{0:D}\n", ret);
                    return 0;
                }
                //audio_iat("audio_source/test.wav", session_begin_params);
                //Console.ReadKey();
                Console.Write("构建离线识别语法网络...\n");
                ret = build_grammar((IntPtr)0); //第一次使用某语法进行识别，需要先构建语法网络，获取语法ID，之后使用此语法进行识别，无需再次构建
                if (ERROR.MSP_SUCCESS != (ERROR)ret)
                {
                    Console.Write("构建语法调用失败！\n");
                    return 0;
                }

                //while (errorCode != 0)
                //{
                //    Thread.Sleep(300);
                //}

                Console.Write("离线识别语法网络构建完成，开始识别...\n");
                return 0;
            }
            finally
            { 
            }
        }

        private static bool audio_iat(string audio_path, string session_begin_params)
        {
            if (audio_path == null || audio_path == "") return false;
            IntPtr session_id;
            StringBuilder result = new StringBuilder();//存储最终识别的结果
            var aud_stat = AudioStatus.MSP_AUDIO_SAMPLE_CONTINUE;//音频状态
            var ep_stat = EpStatus.MSP_EP_LOOKING_FOR_SPEECH;//端点状态
            var rec_stat = RecogStatus.MSP_REC_STATUS_SUCCESS;//识别状态
            int errcode = (int)Errors.MSP_SUCCESS;
            byte[] audio_content;  //用来存储音频文件的二进制数据
            int totalLength = 0;//用来记录总的识别后的结果的长度，判断是否超过缓存最大值
            try
            {
                audio_content = File.ReadAllBytes(audio_path);
                //SoundPlayer player = new SoundPlayer(audio_path);
                //player.Play();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                audio_content = null;
            }
            //if (audio_content == null)
            //{
            //    Console.WriteLine("没有读取到任何内容");
            //    return false;
            //}
            Console.WriteLine("开始进行语音听写.......");
            /*
            * QISRSessionBegin（）；
            * 功能：开始一次语音识别
            * 参数一：定义关键词识别||语法识别||连续语音识别（null）
            * 参数2：设置识别的参数：语言、领域、语言区域。。。。
            * 参数3：带回语音识别的结果，成功||错误代码
            * 返回值intPtr类型,后面会用到这个返回值
            */
            session_id = mscDLL.QISRSessionBegin(null, session_begin_params, ref errcode);
            if (errcode != (int)Errors.MSP_SUCCESS)
            {
                Console.WriteLine("开始一次语音识别失败！");
                return false;
            }
            /*
              QISRAudioWrite（）；
              功能：写入本次识别的音频
              参数1：之前已经得到的sessionID
              参数2：音频数据缓冲区起始地址
              参数3：音频数据长度,单位字节。
               参数4：用来告知MSC音频发送是否完成     MSP_AUDIO_SAMPLE_FIRST = 1	第一块音频
                                                       MSP_AUDIO_SAMPLE_CONTINUE = 2	还有后继音频
                                                        MSP_AUDIO_SAMPLE_LAST = 4	最后一块音频
              参数5：端点检测（End-point detected）器所处的状态
                                                     MSP_EP_LOOKING_FOR_SPEECH = 0	还没有检测到音频的前端点。
                                                      MSP_EP_IN_SPEECH = 1	已经检测到了音频前端点，正在进行正常的音频处理。
                                                      MSP_EP_AFTER_SPEECH = 3	检测到音频的后端点，后继的音频会被MSC忽略。
                                                       MSP_EP_TIMEOUT = 4	超时。
                                                      MSP_EP_ERROR = 5	出现错误。
                                                      MSP_EP_MAX_SPEECH = 6	音频过大。
              参数6：识别器返回的状态，提醒用户及时开始\停止获取识别结果
                                            MSP_REC_STATUS_SUCCESS = 0	识别成功，此时用户可以调用QISRGetResult来获取（部分）结果。
                                             MSP_REC_STATUS_NO_MATCH = 1	识别结束，没有识别结果。
                                           MSP_REC_STATUS_INCOMPLETE = 2	正在识别中。
                                           MSP_REC_STATUS_COMPLETE = 5	识别结束。
              返回值：函数调用成功则其值为MSP_SUCCESS，否则返回错误代码。
                本接口需不断调用，直到音频全部写入为止。上传音频时，需更新audioStatus的值。具体来说:
                当写入首块音频时,将audioStatus置为MSP_AUDIO_SAMPLE_FIRST
                当写入最后一块音频时,将audioStatus置为MSP_AUDIO_SAMPLE_LAST
                其余情况下,将audioStatus置为MSP_AUDIO_SAMPLE_CONTINUE
                同时，需定时检查两个变量：epStatus和rsltStatus。具体来说:
                当epStatus显示已检测到后端点时，MSC已不再接收音频，应及时停止音频写入
                当rsltStatus显示有识别结果返回时，即可从MSC缓存中获取结果*/
            int res = mscDLL.QISRAudioWrite(session_id, audio_content, (uint)audio_content.Length, aud_stat, ref ep_stat, ref rec_stat);
            if (res != (int)Errors.MSP_SUCCESS)
            {
                Console.WriteLine("写入识别的音频失败！" + res);
                return false;
            }
            res = mscDLL.QISRAudioWrite(session_id, null, 0, AudioStatus.MSP_AUDIO_SAMPLE_LAST, ref ep_stat, ref rec_stat);
            if (res != (int)Errors.MSP_SUCCESS)
            {
                Console.WriteLine("写入音频失败！" + res);
                return false;
            }
            while (RecogStatus.MSP_REC_STATUS_COMPLETE != rec_stat)
            {//如果没有完成就一直继续获取结果
             /*
              QISRGetResult（）；
              功能：获取识别结果
              参数1：session，之前已获得
              参数2：识别结果的状态
              参数3：waitTime[in]	此参数做保留用
              参数4：错误编码||成功
              返回值：函数执行成功且有识别结果时，返回结果字符串指针；其他情况(失败或无结果)返回NULL。
              */
                IntPtr now_result = mscDLL.QISRGetResult(session_id, ref rec_stat, 0, ref errcode);
                if (errcode != (int)Errors.MSP_SUCCESS)
                {
                    Console.WriteLine("获取结果失败：" + errcode);
                    return false;
                }
                if (now_result != null)
                {
                    int length = now_result.ToString().Length;
                    totalLength += length;
                    if (totalLength > 4096)
                    {
                        Console.WriteLine("缓存空间不够" + totalLength);
                        return false;
                    }
                    result.Append(Marshal.PtrToStringAnsi(now_result));
                }
                Thread.Sleep(150);//防止频繁占用cpu
            }
            Console.WriteLine("语音听写结束");
            Console.WriteLine("结果：\n");
            Console.WriteLine(result);
            return true;

        }
    }

    internal sealed class DefineConstantsAsr_demo
    {
        public const int SAMPLE_RATE_16K = 16000;
        public const int SAMPLE_RATE_8K = 8000;
        public const int MAX_GRAMMARID_LEN = 32;
        public const int MAX_PARAMS_LEN = 1024;
    }
}
