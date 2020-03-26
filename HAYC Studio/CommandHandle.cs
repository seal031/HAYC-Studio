using HAYC_Studio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAYC_Studio
{
    /// <summary>
    /// 处理普通语音指令（除唤醒词之外的指令）
    /// </summary>
    public class CommandHandle
    {

    }




    public class BaseCommand
    {
        protected string commandName { get; set; }

        public BaseCommand(string commandText)
        {
            commandName = commandText;
        }

        public virtual void executeCommand() { }
    }

    public class FormCommand : BaseCommand
    {
        public FormCommand(string commandText) : base(commandText)
        {

        }

        public override void executeCommand()
        {
            //var form = FormController.formInfoList.FirstOrDefault(f => f.PageName == "");
            //form.Form.Navigate(@"https://www.baidu.com");
        }
    }
}
