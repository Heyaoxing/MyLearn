using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethodPattern
{
    class Program
    {
        static void Main(string[] args)
        {
          
            Protector yangjian=new YangJian();
            yangjian.Guard();


            Protector nazha = new NaZha();
            nazha.Guard();

            Console.ReadKey();
        }


        public class Protector
        {
            /// <summary>
            /// 守卫佛门
            /// </summary>
            public void Guard()
            {
                Console.WriteLine(Ability());
            }
            /// <summary>
            /// 能力
            /// </summary>
            /// <returns></returns>
            protected virtual string Ability()
            {
                return "";
            }
        }

        /// <summary>
        /// 郑伦
        /// </summary>
        public class ZhengLun : Protector
        {
            protected override string Ability()
            {
                return "哼";
            }
        }
        /// <summary>
        /// 陈奇
        /// </summary>
        public class ChenQi : Protector
        {
            protected override string Ability()
            {
                return "哈";
            }
        }
        /// <summary>
        /// 杨戬
        /// </summary>
        public class YangJian : Protector
        {
            protected override string Ability()
            {
                return "天眼";
            }
        }
        /// <summary>
        /// 哪咤
        /// </summary>
        public class NaZha : Protector
        {
            protected override string Ability()
            {
                return "喷火";
            }
        }
    }
}
