using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern
{
    /// <summary>
    /// 大哥
    /// </summary>
    public class BigBrother
    {
        public void RedEnvelopes(string name)
        {
            Console.WriteLine(name + "恭喜发财");
        }
    }

    /// <summary>
    /// 弟弟
    /// </summary>
    public class LittleBrother
    {
        public void RedEnvelopes(string name)
        {
            Console.WriteLine(name + "恭喜发财");
        }
    }
}
