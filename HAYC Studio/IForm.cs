using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAYC_Studio
{
    public interface IForm
    {
        void DoJavaScriptFuntion(string funcName, params object[] paramList);
    }
}
