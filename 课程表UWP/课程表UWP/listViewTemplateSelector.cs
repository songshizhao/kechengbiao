using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace 课程表UWP
{
    class ListViewTemplateSelector:DataTemplateSelector
    {
        private DataTemplate _DataTemplate1;
        private DataTemplate _DataTemplate2;

        public DataTemplate DataTemplate1
        {
            get
            {
                return _DataTemplate1;
            }

            set
            {
                if (value != _DataTemplate1)
                {
                    _DataTemplate1 = value;                  
                }
            }
        }

        public DataTemplate DataTemplate2
        {
            get
            {
                return _DataTemplate2;
            }

            set
            {
                if (value != _DataTemplate2)
                {
                    _DataTemplate2 = value;
                }
            }
        }
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item.ToString() == "-")
            {
                return DataTemplate1;
            }
            else
            {
                return DataTemplate2;
            }           
        }



        






    }
}
