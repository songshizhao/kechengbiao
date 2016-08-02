using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
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
    public sealed partial class SetPage : Page
    {
        /************************************/
        //获取本地数据存储容器
        ApplicationDataContainer container = ApplicationData.Current.LocalSettings;
        /************************************/

        /************************************/
        public SetPage()
        {
            this.InitializeComponent();
            GetSetting();
        }

        /************************************/
        //加载设置数据
        public void GetSetting()
        {
            if (container.Values["Theme"] != null)
            {
                if (container.Values["Theme"].ToString() == "夜间模式")
                {
                    DarkTheme.IsOn = true;
                    ChangedDark();
                }
                else
                {
                    DarkTheme.IsOn = false;
                    ChangedLight();
                }
            }
            else
            {
                DarkTheme.IsOn = false;
            }
        }
        /************************************/

        /************************************/
        //显示周数
        private void SetWeek_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            //获取datepicker所选选的日期是第几天         
            container.Values["Day"] = SetWeek.Date.DayOfYear.ToString();
        }
        /************************************/


    
        /************************************/
        //清除所有课程信息
        private async void Delete_all_Toggled(object sender, RoutedEventArgs e)
        {
            if (Delete_all.IsOn == true)
            {
                StorageFolder Monfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Monlist");
                await Monfolder.DeleteAsync();
                StorageFolder Tuesfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Tueslist");
                await Tuesfolder.DeleteAsync();
                StorageFolder Wedfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Wedlist");
                await Wedfolder.DeleteAsync();
                StorageFolder Thursfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Thurslist");
                await Thursfolder.DeleteAsync();
                StorageFolder Frifolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Frilist");
                await Frifolder.DeleteAsync();
                StorageFolder Satfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Satlist");
                await Satfolder.DeleteAsync();
                StorageFolder Sunfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Sunlist");
                await Sunfolder.DeleteAsync();
                //显示消息框
                await new MessageDialog("清除完成").ShowAsync();
                //
                Delete_all.IsOn = false;

                //删除应用设置
                //container.Values.Remove("Day");
            }
            else
            {
                return;
            }
        }
        /************************************/

        /*****************************/
        //切换夜间模式
        private void DarkTheme_Toggled(object sender, RoutedEventArgs e)
        {
            if (DarkTheme.IsOn == true)
            {
                //开启夜间模式  
                container.Values["Theme"] = "夜间模式";
                ChangedDark();
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
                {
                    StatusBar statusBar = StatusBar.GetForCurrentView();
                    statusBar.BackgroundColor = Colors.Black;
                    statusBar.ForegroundColor = Colors.White;
                    statusBar.BackgroundOpacity = 1;
                }
            }
            else
            {
                //关闭夜间模式
                container.Values["Theme"] = "日间模式";
                ChangedLight();
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
                {
                    StatusBar statusBar = StatusBar.GetForCurrentView();
                    statusBar.BackgroundColor = Colors.White;
                    statusBar.ForegroundColor = Colors.Black;
                    statusBar.BackgroundOpacity = 1;
                }
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
        /*****************************/

    }
}
