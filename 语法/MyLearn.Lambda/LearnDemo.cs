using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Lambda
{
    public class LearnDemo
    {
        public Func<int, string> func1 = x => "这个方法1:" + x;
        public Func<int, string> func2 = x => "这个方法2:" + x;

        public void Process()
        {
            Expression<Func<MyModel, int>> expr = model => model.MyProperty;
            var member = expr.Body as MemberExpression;
            Console.WriteLine(member);
        }
    }

    public class MyModel
    {
        public string name { set; get; }
        public int MyProperty { set; get; }
    }
}
