using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 课程表UWP.Model
{
   public class Human
    {
        public string Name { set; get; }
    }

    public class Male:Human
    {
        public string Stature { set; get; }
    }

    public class Female : Human
    {
        public string Year { set; get; }
    }
}
