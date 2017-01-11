using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Lambda
{
    public class LambdaTree
    {
        #region  无限循环执行
        //// 创建 loop表达式体来包含我们想要执行的代码
        //LoopExpression loop = Expression.Loop(
        //    Expression.Call(
        //        null,
        //        typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
        //        Expression.Constant("Hello"))
        //        );




        //public  void Process()
        //{

        //    // 创建一个代码块表达式包含我们上面创建的loop表达式
        //    BlockExpression block = Expression.Block(loop);

        //    // 将我们上面的代码块表达式
        //    Expression<Action> lambdaExpression = Expression.Lambda<Action>(block);

        //    lambdaExpression.Compile().Invoke();
        //}

        #endregion

        #region 条件判断 退出循环
        //LabelTarget labelBreak = Expression.Label();
        //ParameterExpression loopIndex = Expression.Parameter(typeof(int), "index");
        //public void Process()
        //{
        //    BlockExpression block = Expression.Block(
        //                            new[] { loopIndex },
        //                                        // 初始化loopIndex =1
        //                            Expression.Assign(loopIndex, Expression.Constant(1)),
        //                            Expression.Loop(
        //                                Expression.IfThenElse(
        //                                        // if 的判断逻辑
        //                                    Expression.LessThanOrEqual(loopIndex, Expression.Constant(10)
        //                                    ),
        //                                        // 判断逻辑通过的代码
        //                                    Expression.Block(
        //                                        Expression.Call(
        //                                            null,
        //                                            typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
        //                                            Expression.Constant("Hello")),
        //                                        Expression.PostIncrementAssign(loopIndex)),
        //                                        // 判断不通过的代码
        //                                    Expression.Break(labelBreak)
        //                                    ), labelBreak));
        //    // 将我们上面的代码块表达式
        //    Expression<Action> lambdaExpression = Expression.Lambda<Action>(block);
        //    lambdaExpression.Compile().Invoke();
        //}
        #endregion

        #region 有返回值的表达式树
        // 直接返回常量值 
       // ConstantExpression ce1 = Expression.Constant("你好");
        //public void Process()
        //{
        //    // 直接用我们上面创建的常量表达式来创建表达式树
        //    Expression<Func<string>> expr1 = Expression.Lambda<Func<string>>(ce1);
        //    Console.WriteLine(expr1.Compile().Invoke());

        //    // --------------在方法体内创建变量，经过操作之后再返回------------------
        //    // 1.创建方法体表达式 2.在方法体内声明变量并附值 3. 返回该变量
        //    ParameterExpression param2 = Expression.Parameter(typeof(int));
        //    BlockExpression block2 = Expression.Block(
        //        new[] { param2 },
        //        Expression.AddAssign(param2, Expression.Constant(20)),
        //        param2
        //        );
        //    Expression<Func<int>> expr2 = Expression.Lambda<Func<int>>(block2);
        //    Console.WriteLine(expr2.Compile().Invoke());

        //    // -------------利用GotoExpression返回值-----------------------------------
        //    LabelTarget returnLabelTarget = Expression.Label(typeof (Int32));


        //    LabelExpression returnLabel = Expression.Label(returnLabelTarget, Expression.Constant(80,typeof(int)));

        //    ParameterExpression inParam3 = Expression.Parameter(typeof (int));

        //    BlockExpression block3 = Expression.Block(
        //        Expression.AddAssign(inParam3,Expression.Constant(10)),
        //        Expression.Return(returnLabelTarget, inParam3), returnLabel
        //        );

        //    Expression<Func<int, int>> expr3 = Expression.Lambda<Func<int, int>>(block3, inParam3);

        //    Console.WriteLine(expr3.Compile().Invoke(520));
        //}
        #endregion


        #region 简单的switch case 语句

        ParameterExpression genderParam = Expression.Parameter(typeof (int));

        public void Process()
        {
          SwitchExpression swithExpression = Expression.Switch(
            genderParam,
            Expression.Constant("不详"),
            Expression.SwitchCase(Expression.Constant("男"),Expression.Constant(1)),
            Expression.SwitchCase(Expression.Constant("女"),Expression.Constant(0))

            );


          Expression<Func<int, string>> expr4 = Expression.Lambda<Func<int, string>>(swithExpression, genderParam);

          Console.WriteLine(expr4.Compile().Invoke(1));
          Console.WriteLine(expr4.Compile().Invoke(0));
          Console.WriteLine(expr4.Compile().Invoke(10));
        }
       
        #endregion

    }
}
