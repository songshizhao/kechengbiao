using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using 课程表UWP.Model;

namespace 课程表UWP
{
    public class ListViewDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MaleData { set; get; }
        public DataTemplate FemaleData { set; get; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is Male)
            {
                return MaleData;
            }
            return FemaleData;
        }
    }
}
