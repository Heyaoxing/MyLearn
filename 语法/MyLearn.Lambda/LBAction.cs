using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyLearn.Lambda
{
    public class LBAction
    {

        public static int OutParam = 10;

        /// <summary>
        /// 无参无返回值委托
        /// </summary>
        public Action NotParamAction = () =>
          {
              Trace.WriteLine("即时窗口显示");
          };

        /// <summary>
        /// 有参无返回值委托,最大支持16个参数.
        /// </summary>
        public Action<int, int> ParamAction = (x, y) =>
          {
              Trace.WriteLine("相加等于:" + (x + y));
          };

        /// <summary>
        /// 有参返回值委托,最大支持16个参数.
        /// </summary>
        public Func<int, int> ParamFunc = x => x + 1;

        /// <summary>
        /// 接收两个不同类型的有参返回值委托
        /// </summary>
        public Func<int, DateTime, string> ChangeDataFunc = (x, data) =>
        {
            var newData = data.AddDays(x);
            Trace.WriteLine(data);
            return newData.ToString();
        };

        /// <summary>
        ///TODO: 未破解-异步泛型委托
        /// </summary>
        public Func<int, int, Task<string>> AsynFunc = async(x, y) =>
        {
            var total = x*y + y;
            Trace.WriteLine("等待2秒...");
            Thread.Sleep(1000*2);
            Trace.WriteLine("等待完成");
            return total.ToString();
        };


        /// <summary>
        /// 接收外部参数泛型委托
        /// </summary>
        public Func<int, int> OutParamFunc01 = x => x * OutParam;
        /// <summary>
        /// 接收外部参数泛型委托
        /// </summary>
        public Func<int, int> OutParamFunc02 = x => x + OutParam;

        /// <summary>
        /// 当作参数调用,这种方式可以将同参数不同逻辑的泛型委托当作参数注入!
        /// </summary>
        /// <param name="outParamFunc"></param>
        public void ModifyStuff(Func<int, int> outParamFunc)
        {
            Trace.WriteLine(outParamFunc(3));
        }
    }
}
