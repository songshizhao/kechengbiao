using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace 课程表UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        /************************************/
        //获取本地数据存储容器
        ApplicationDataContainer container = ApplicationData.Current.LocalSettings;
        /************************************/

        public AboutPage()
        {
            this.InitializeComponent();
            GetSetting();
        }

        /************************************/
        //加载设置
        private void GetSetting()
        {
            if (container.Values["Theme"] != null)
            {
                if (container.Values["Theme"].ToString() == "夜间模式")
                {
                    ChangedDark();
                }
                else
                {
                    ChangedLight();
                }
            }
            else
            {
                return;
            }
        }
        //夜间模式
        private void ChangedDark()
        {
            RequestedTheme = ElementTheme.Dark;
        }
        //日间模式
        private void ChangedLight()
        {
            RequestedTheme = ElementTheme.Light;
        }
        /************************************/
    }
}
