using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 课程表UWP.Model;

namespace 课程表UWP.ViewModel
{
    public class ViewModel : NotifyProperty
    {
        public ViewModel()
        {
            HumanWorld.Add(new Male()
            {
                Name = "csdn",
                Stature = "100"
            });

            HumanWorld.Add(new Female()
            {
                Name = "女",
                Year = "16"
            });
        }

        public ObservableCollection<Human> HumanWorld { set; get; }
        =new ObservableCollection<Human>();
    }
}
