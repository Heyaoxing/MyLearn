using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Lambda
{
    public class LBAction
    {
      public  Action NotParamAction = () =>
        {
            Trace.WriteLine("即时窗口显示");
        };

      public Action<int, int> ParamAction = (x, y) =>
        {
            Trace.WriteLine("两个数相加等于:"+(x+y));
        };
    }
}
