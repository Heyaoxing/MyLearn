using System;
using System.Collections.Generic;
using System.Text;

namespace EventDemo
{
    /// <summary>
    /// 钓鱼者
    /// </summary>
    public class FishingMan
    {
        public string Name;
        public int Count = 0;
        public FishingMan(string name)
        {
            this.Name = name;
        }
    }
}
