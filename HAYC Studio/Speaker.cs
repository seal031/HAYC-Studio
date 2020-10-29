using AxWMPLib;
using HAYC_ProcessCommunicate_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


public class Speaker
{
    AxWindowsMediaPlayer player;
    PipeCommunicateServer pipeServer;
    Dictionary<string, string> messageSoundDic = new Dictionary<string, string>();
    public CountdownEvent ce;

    public Speaker(AxWindowsMediaPlayer _player, PipeCommunicateServer _pipeServer)
    {
        this.player = _player;
        player.PlayStateChange += Player_PlayStateChange;
        this.pipeServer = _pipeServer;
        messageSoundDic.Add("小安小安", @"voices\OnWake.mp3");
        messageSoundDic.Add("开始休眠", @"voices\OnSleep.mp3");
        messageSoundDic.Add("未能识别命令", @"voices\OnUnkown.mp3");
    }

    private void Player_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
    {
        //Console.WriteLine("player状态是"+e.newState);
        if (e.newState == 8)
        {
            ProcessCommunicateMessage message = new ProcessCommunicateMessage();
            message.ProcessName = "";
            message.MessageType = CommunicateMessageType.START;
            message.Message = "";
            pipeServer.sendMessage(message.toJson());
            Console.WriteLine("发送" + message.toJson());
            ce.Signal();
        }
    }

    public void play(string message)
    {
        if (message == "开始接收语音指令……")
        {

        }
        else if (message == string.Empty)
        {
            player.URL = @"voices\Quiet.m4a";
        }
        else if (messageSoundDic.ContainsKey(message))
        {
            var a=messageSoundDic[message];
            player.URL = messageSoundDic[message];
        }
        else
        {
            player.URL = @"voices\OnCommand.mp3";
        }
    }
}

