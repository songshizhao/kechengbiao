using System.IO;
using System.Xml;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using System.Diagnostics;

namespace 课程表UWP.data
{
    public sealed class ListViewTemplateSelector : DataTemplateSelector
    {
        public DataTemplate _DataTemplate01;
        public DataTemplate _DataTemplate02;

        public DataTemplate DataTemplate01
        {
            get
            {
                return _DataTemplate01;
            }

            set
            {
                if (value != _DataTemplate01)
                {
                    _DataTemplate01 = value;
                }
            }
        }

        public DataTemplate DataTemplate02
        {
            get
            {
                return _DataTemplate02;
            }

            set
            {
                if (value != _DataTemplate02)
                {
                    _DataTemplate02 = value;
                }
            }
        }
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {


            ListView list = ItemsControl.ItemsControlFromItemContainer(container) as ListView;

            int index = list.IndexFromContainer(container);

            //Debug.WriteLine(item.GetType().ToString());

            Course D = (Course)item;

            if (D.class_finished_property=="1")
            {
                return DataTemplate02;
            }
            else
            {
                return DataTemplate01;
            }


        }



        















        }
    }

