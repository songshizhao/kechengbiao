using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml;
//using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using 课程表UWP.data;
using 课程表UWP.Pages;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace 课程表UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        ObservableCollection<Course> Monday = new ObservableCollection<Course>();
        ObservableCollection<Course> Tuesday = new ObservableCollection<Course>();
        ObservableCollection<Course> Wednesday = new ObservableCollection<Course>();
        ObservableCollection<Course> Thursday = new ObservableCollection<Course>();
        ObservableCollection<Course> Friday = new ObservableCollection<Course>();
        ObservableCollection<Course> Saturday = new ObservableCollection<Course>();
        ObservableCollection<Course> Sunday = new ObservableCollection<Course>();

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;

        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile XmlFileInApp, XmlFileInLocal;
            //如果本地文件夹内不存在xml记录，复制安装包内xml过去
            if (await folder.TryGetItemAsync("class") == null)
            {
                //获取安装包内xml
                XmlFileInApp = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///data\class.xml"));
                Stream XmlReader = await XmlFileInApp.OpenStreamForReadAsync();
                //读取xml到内存
                XmlDocument xml = new XmlDocument();
                xml.Load(XmlReader);
                //在本地新建xml文件
                XmlFileInLocal = await folder.CreateFileAsync("class", CreationCollisionOption.ReplaceExisting);
                //保存xml文件到local
                Stream XmlWriter = await XmlFileInLocal.OpenStreamForWriteAsync();
                xml.Save(XmlWriter);
                XmlWriter.Dispose();

            }
            //读xml文件
            XmlFileInLocal = await folder.GetFileAsync("class");
            Stream XmlReader1 = await XmlFileInLocal.OpenStreamForReadAsync();
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(XmlReader1);

            


            foreach (var element in XmlDoc.DocumentElement)
            {

                XmlElement each_element = (XmlElement)element;
                //Debug.WriteLine(each_element.Name);



                foreach (var InnerElement in each_element)
                {
                    XmlElement each_InnerElement = (XmlElement)InnerElement;
                    //Debug.WriteLine(each_InnerElement.Name);//Debug.WriteLine(each_InnerElement.GetAttribute("duration"));
                    Course acourse = new Course();
                    acourse.class_name_property = each_InnerElement.GetAttribute("name");
                    acourse.class_index_property[0] = each_element.Name;
                    acourse.class_index_property[1] = each_InnerElement.GetAttribute("index");                    
                    acourse.class_duration_property = each_InnerElement.GetAttribute("duration");
                    acourse.class_startTime = each_InnerElement.GetAttribute("startTime");
                    acourse.class_endTime = each_InnerElement.GetAttribute("endTime");                
                    acourse.class_tag_property = each_InnerElement.GetAttribute("tag");
                    acourse.class_room_property = each_InnerElement.GetAttribute("room");
                    acourse.class_teacher_property = each_InnerElement.GetAttribute("teacher");
                    acourse.class_weeklimit_property = each_InnerElement.GetAttribute("weeklimit");

                    //Debug.WriteLine(acourse.class_index_property);
                    switch (each_element.Name)
                    {
                        case "Monday":
                            Monday.Add(acourse);
                            break;
                        case "Tuesday":
                            Tuesday.Add(acourse);
                            break;
                        case "Wednesday":
                            Wednesday.Add(acourse);
                            break;
                        case "Thursday":
                            Thursday.Add(acourse);
                            break;
                        case "Friday":
                            Friday.Add(acourse);
                            break;
                        case "Saturday":
                            Saturday.Add(acourse);
                            break;
                        case "Sunday":
                            Sunday.Add(acourse);
                            break;
                    }


                }

                day1.DataContext = Monday;
                day2.DataContext = Tuesday;
                day3.DataContext = Wednesday;
                day4.DataContext = Thursday;
                day5.DataContext = Friday;
                day6.DataContext = Saturday;
                day7.DataContext = Sunday;
              
              
            }
        }

        private void edit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var edit_button = (Button)sender;
            Frame.Navigate(typeof(EditPage), edit_button.Tag);

        }

        private void delete_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Button deletebutton = (Button)sender;            
            //ContentPresenter myContentPresenter = (ContentPresenter)(ListView.ItemsControlFromItemContainer();
            //myContentPresenter.ContentTemplate = DataTemplate.;

        }

        private void add_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Button btn =(Button) sender;
            string flag =(string) btn.Tag;
            switch (flag)
            {
                case "设置":
                    Frame.Navigate(typeof(SetPage));
                    break;
                case "关于":
                    Frame.Navigate(typeof(AboutPage));
                    break;

            }
        }
    }


}








//}

//}
//{
//    /*****************************/
//    //变量声明
//    int dayOfweek;    //Located at Weekday();
//    int countOfweek;
//    int sdModeCount; //用于单双周背景显示
//    int count,count1;
//    //获取本地数据存储容器
//    ApplicationDataContainer container = ApplicationData.Current.LocalSettings;
//    /*****************************/

//    public MainPage()
//    {
//        this.InitializeComponent();
//        //获得DateTime对象,pivot跳转
//        Weekday();
//        //加载pivot
//        SetPivotFontSize();
//        //加载页面
//        Loaded += MainPage_Loaded;
//        //加载设置
//        GetSetting();
//        //加载周数
//        LoadingWeek();
//    }

//    /*****************************/
//    //加载周数
//    private void LoadingWeek()
//    {
//        //显示周数
//        if (container.Values.ContainsKey("Day") == false)
//        {
//            SetPage setpage = new SetPage();
//            container.Values["Day"] = setpage.SetWeek.Date.DayOfYear.ToString();
//        }
//        ShowWeek();
//        ShowWeekTxt.Text = "第" + countOfweek + "周";
//    }
//    //显示周数
//    private void  ShowWeek()
//    {
//        //从本地存储中获取被选定的日期
//        string selectedday = container.Values["Day"].ToString();
////   pivot.Title = selectedday;
//        int Toint_day = Convert.ToInt32(selectedday);
//        //获取设备当前时间
//        DateTime datetime = DateTime.Now;
//        string currentDay = datetime.Date.DayOfYear.ToString();
////  pivot.Title = currentDay;
//        int Toint_currentDay = Convert.ToInt32(currentDay);
//        //比较
//        int a =  (Toint_currentDay - Toint_day) / 7;
//        switch (a)
//        {
//            case 0: countOfweek = 1;break;
//            case 1: countOfweek = 2;break;
//            case 2: countOfweek = 3; break;
//            case 3: countOfweek = 4; break;
//            case 4: countOfweek = 5; break;
//            case 5: countOfweek = 6; break;
//            case 6: countOfweek = 7; break;
//            case 7: countOfweek = 8; break;
//            case 8: countOfweek = 9; break;
//            case 9: countOfweek = 10; break;
//            case 10: countOfweek = 11; break;
//            case 11: countOfweek = 12; break;
//            case 12: countOfweek = 13; break;
//            case 13: countOfweek = 14; break;
//            case 14: countOfweek = 15; break;
//            case 15: countOfweek = 16; break;
//            case 16: countOfweek = 17; break;
//            case 17: countOfweek = 18; break;
//            case 18: countOfweek = 19; break;
//            case 19: countOfweek = 20; break;
//            case 20: countOfweek = 21; break;
//        }
//    }
//    /*****************************/

//    /*****************************/
//    //设置pivot字体大小
//    private void SetPivotFontSize()
//    {
//        pivot.Title = new TextBlock { Text = "课程表", FontSize = 35 };
//        p0.Header = new TextBlock { Text = "星期日", FontSize = 20 };
//        p1.Header = new TextBlock { Text = "星期一", FontSize = 20 };
//        p2.Header = new TextBlock { Text = "星期二", FontSize = 20 };
//        p3.Header = new TextBlock { Text = "星期三", FontSize = 20 };
//        p4.Header = new TextBlock { Text = "星期四", FontSize = 20 };
//        p5.Header = new TextBlock { Text = "星期五", FontSize = 20 };
//        p6.Header = new TextBlock { Text = "星期六", FontSize = 20 };
//    }
//    //打开应用页面pivot跳转到当天所对应的星期
//    private void Weekday()
//    {
//        DateTime datetime = DateTime.Now;
//        //获取星期枚举           
//        string day = datetime.DayOfWeek.ToString();
//        switch (day)
//        {
//            case "Sunday":
//                dayOfweek = 0;                   
//                break;
//            case "Monday":
//                dayOfweek = 1;                   
//                break;
//            case "Tuesday":
//                dayOfweek = 2;                   
//                break;
//            case "Wednesday":
//                dayOfweek = 3;                    
//                break;
//            case "Thursday":
//                dayOfweek = 4;                    
//                break;
//            case "Friday":
//                dayOfweek = 5;                  
//                break;
//            case "Saturday":
//                dayOfweek = 6;                   
//                break;
//        }            
//        pivot.SelectedIndex = dayOfweek;       
//    }
//    /*****************************/

//    /*****************************/
//    //加载信息
//    async void MainPage_Loaded(object sender, RoutedEventArgs e)
//    {
//        //创建名称为“Monlist”--"Sunlist"文件夹并获取
//        StorageFolder Monfolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Monlist", CreationCollisionOption.OpenIfExists);
//        StorageFolder Tuesfolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Tueslist", CreationCollisionOption.OpenIfExists);
//        StorageFolder Wedfolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Wedlist", CreationCollisionOption.OpenIfExists);
//        StorageFolder Thursfolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Thurslist", CreationCollisionOption.OpenIfExists);
//        StorageFolder Frifolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Frilist", CreationCollisionOption.OpenIfExists);
//        StorageFolder Satfolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Satlist", CreationCollisionOption.OpenIfExists);
//        StorageFolder Sunfolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Sunlist", CreationCollisionOption.OpenIfExists);
//        /**************************************/
//        Load_week(Sunfolder, SunFiles);
//        Load_week(Monfolder, MonFiles);
//        Load_week(Tuesfolder, TuesFiles);
//        Load_week(Wedfolder, WedFiles);
//        Load_week(Thursfolder, ThursFiles);
//        Load_week(Frifolder, FriFiles);
//        Load_week(Satfolder, SatFiles);           
//    }
//    //Loading信息事件
//    private async void Load_week(StorageFolder folder, ListBox list)
//    {
//        var files = await folder.GetFilesAsync();
//        {
//            //获取Monlisi文件夹里的文件
//            foreach (StorageFile file in files)
//            {
//                //加载应用文件
//                XmlDocument doc = await XmlDocument.LoadFromFileAsync(file);
//                /**************************************/
//                //动态grid
//                Grid grid = new Grid();
//                //搭建Grid
//                //第一列
//                ColumnDefinition col = new ColumnDefinition();
//                GridLength c1 = new GridLength(200);
//                col.Width = c1;
//                grid.ColumnDefinitions.Add(col);
//                //第二列
//                ColumnDefinition col2 = new ColumnDefinition();
//                GridLength c2 = new GridLength(200);
//                col2.Width = c2;
//                grid.ColumnDefinitions.Add(col2);
//                //第一行
//                RowDefinition row1 = new RowDefinition();
//                GridLength r1 = new GridLength(50);
//                row1.Height = r1;
//                grid.RowDefinitions.Add(row1);
//                //第二行
//                RowDefinition row2 = new RowDefinition();
//                GridLength r2 = new GridLength(50);
//                row1.Height = r2;
//                grid.RowDefinitions.Add(row2);
//                //第三行
//                RowDefinition row3 = new RowDefinition();
//                GridLength r3 = new GridLength(50);
//                row1.Height = r3;
//                grid.RowDefinitions.Add(row3);
//                /**************************************/
//                //nameTxt课程名称
//                TextBlock nameTxt = new TextBlock();
//                nameTxt.Text = doc.DocumentElement.Attributes.GetNamedItem("coursename").NodeValue.ToString();
//                nameTxt.FontSize = 24;
//                Grid.SetRow(nameTxt, 0);
//                Grid.SetColumn(nameTxt, 0);
//                //start_time——end_time时间
//                TextBlock timeTxt = new TextBlock();
//                timeTxt.Text = doc.DocumentElement.Attributes.GetNamedItem("startTime").NodeValue.ToString() + "--"
//                    + doc.DocumentElement.Attributes.GetNamedItem("endTime").NodeValue.ToString();
//                Grid.SetRow(timeTxt, 1);
//                Grid.SetColumn(timeTxt, 0);
//                //weekTxt课程周数
//                TextBlock weekTxt = new TextBlock();
//                weekTxt.Text = doc.DocumentElement.Attributes.GetNamedItem("week").NodeValue.ToString()+"-"
//                    + doc.DocumentElement.Attributes.GetNamedItem("sdmode").NodeValue.ToString();
//                Grid.SetRow(weekTxt, 1);
//                Grid.SetColumn(weekTxt, 1);
//                //roomTxt教室
//                TextBlock roomTxt = new TextBlock();
//                roomTxt.Text = doc.DocumentElement.Attributes.GetNamedItem("room").NodeValue.ToString();
//                Grid.SetRow(roomTxt, 2);
//                Grid.SetColumn(roomTxt, 0);
//                //teacherTxt教师
//                TextBlock teacherTxt = new TextBlock();
//                teacherTxt.Text = doc.DocumentElement.Attributes.GetNamedItem("teacher").NodeValue.ToString();
//                Grid.SetRow(teacherTxt, 2);
//                Grid.SetColumn(teacherTxt, 1);
//                //向Grid添加控件
//                grid.Children.Add(nameTxt);
//                grid.Children.Add(timeTxt);
//                grid.Children.Add(weekTxt);
//                grid.Children.Add(roomTxt);
//                grid.Children.Add(teacherTxt);
//                grid.Name = file.DisplayName;
//                grid.Background = new SolidColorBrush(Colors.DeepSkyBlue );
//                /**************************************/
//                //获取课程结束时间
//                string end = doc.DocumentElement.Attributes.GetNamedItem("endTime").NodeValue.ToString();
//                LearnedShow(end,grid);
//                /**************************************/
//                //单双周显示不同颜色
//                string sdMode = doc.DocumentElement.Attributes.GetNamedItem("sdmode").NodeValue.ToString();
//                sdModeShow(sdMode,grid);
//                /**************************************/
//                //向Listbox添加条目
//                list.Items.Add(grid);
//            }
//        }
//    }
//    /*****************************/

//    /*****************************/
//    //单双周显示不同颜色
//    private void sdModeShow(string str,Grid grid )
//    {
//        switch (str)
//        {
//            case "无":
//                sdModeCount = 0;
//                break;
//            case "单周":
//                sdModeCount = 1;
//                break;
//            case "双周":
//                sdModeCount = 2;
//                break;
//        }
//        //单周   不是单周显示hotpimk
//        if (countOfweek % 2 != 0)
//        {
//            if (sdModeCount  == 2)
//            {
//                grid.Background = new SolidColorBrush(Colors.HotPink);
//            }
//        }
//        else
//        {
//            if (sdModeCount == 1)
//            {
//                grid.Background = new SolidColorBrush(Colors.HotPink);
//            }
//        }
//    }
//    //上过的课程显示灰色
//    private void LearnedShow(string str, Grid grid)
//    {
//        //上过的课程显示灰色
//          DateTime datetime = DateTime.Now;
//        //获取当前时间--小时
//          string hour = datetime.Hour.ToString();
//          int intHour = Convert.ToInt32(hour);
//        string minute = datetime.Minute.ToString();
//        int intMinute = Convert.ToInt32(minute);
//        //把String装换为DateTime
//          DateTime endtemp = Convert.ToDateTime(str);
//          string endhour = endtemp.Hour.ToString();
//          int intEndhour = Convert.ToInt32(endhour);
//        string endMinute = endtemp.Minute.ToString();
//        int intEndminute = Convert.ToInt32(endMinute);
//        //比较当前时间与结束时间                    
//        if (intHour > intEndhour || intHour == intEndhour)
//        {
//            if (intHour > intEndhour)
//            {
//                grid.Background = new SolidColorBrush(Colors.Gray);
//            }
//            else
//            {
//                if (intMinute > intEndminute)
//                {
//                    grid.Background = new SolidColorBrush(Colors.Gray);
//                }
//            }
//        }
//    }
//    /*****************************/

//    /*****************************/
//    //加载设置
//    private void GetSetting()
//    {
//        //夜间模式
//        if (container.Values["Theme"] != null)
//        {
//            if (container.Values["Theme"].ToString() == "夜间模式")
//            {
//                ChangedDark();
//                //状态栏
//                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
//                {
//                    StatusBar statusBar = StatusBar.GetForCurrentView();
//                    statusBar.BackgroundColor = Colors.Black;
//                    statusBar.ForegroundColor = Colors.White;
//                    statusBar.BackgroundOpacity = 1;
//                }
//            }
//            else
//            {
//                ChangedLight();
//                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
//                {
//                    StatusBar statusBar = StatusBar.GetForCurrentView();
//                    statusBar.BackgroundColor = Colors.White;
//                    statusBar.ForegroundColor = Colors.Black;
//                    statusBar.BackgroundOpacity = 1;
//                }
//            }
//        }
//        else
//        {
//            return;
//        }
//    }
//    /*****************************/

//    /*****************************/
//    //右键菜单
//    private void Tapped_Flyout(object sender, TappedRoutedEventArgs e)
//    {
//        FrameworkElement element = sender as FrameworkElement;
//        if (element != null)
//        {
//            FlyoutBase.ShowAttachedFlyout(element);
//        }        
//    }
//    //右键删除课程
//    private async void DeleteCourse(object sender, RoutedEventArgs e)
//    {
//        int index = pivot.SelectedIndex;
//        switch (index)
//        {
//            case 1:
//                Grid mon_grid = (Grid)MonFiles.SelectedItem;
//                MonFiles.Items.Remove(MonFiles.SelectedItem);
//                StorageFolder Monfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Monlist");
//                StorageFile MonFile = await Monfolder.GetFileAsync(mon_grid.Name + ".xml");
//                await MonFile.DeleteAsync();
//                break;
//            case 2:
//                Grid tues_grid = (Grid)TuesFiles.SelectedItem;
//                TuesFiles.Items.Remove(TuesFiles.SelectedItem);
//                StorageFolder Tuesfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Tueslist");
//                StorageFile TuesFile = await Tuesfolder.GetFileAsync(tues_grid.Name + ".xml");
//                await TuesFile.DeleteAsync();
//                break;
//            case 3:
//                Grid wed_grid = (Grid)WedFiles.SelectedItem;
//                WedFiles.Items.Remove(WedFiles.SelectedItem);
//                StorageFolder Wedfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Wedlist");
//                StorageFile WedFile = await Wedfolder.GetFileAsync(wed_grid.Name + ".xml");
//                await WedFile.DeleteAsync();
//                break;
//            case 4:
//                Grid thurs_grid = (Grid)ThursFiles.SelectedItem;
//                ThursFiles.Items.Remove(ThursFiles.SelectedItem);
//                StorageFolder Thursfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Thurslist");
//                StorageFile ThursFile = await Thursfolder.GetFileAsync(thurs_grid.Name + ".xml");
//                await ThursFile.DeleteAsync();
//                break;
//            case 5:
//                Grid fri_grid = (Grid)FriFiles.SelectedItem;
//                FriFiles.Items.Remove(FriFiles.SelectedItem);
//                StorageFolder Frifolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Frilist");
//                StorageFile FriFile = await Frifolder.GetFileAsync(fri_grid.Name + ".xml");
//                await FriFile.DeleteAsync();
//                break;
//            case 6:
//                Grid sat_grid = (Grid)SatFiles.SelectedItem;
//                SatFiles.Items.Remove(SatFiles.SelectedItem);
//                StorageFolder Satfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Satlist");
//                StorageFile SatFile = await Satfolder.GetFileAsync(sat_grid.Name + ".xml");
//                await SatFile.DeleteAsync();
//                break;
//            case 0:
//                Grid sun_grid = (Grid)SunFiles.SelectedItem;
//                SunFiles.Items.Remove(SunFiles.SelectedItem);
//                StorageFolder Sunfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Sunlist");
//                StorageFile SunFile = await Sunfolder.GetFileAsync(sun_grid.Name + ".xml");
//                await SunFile.DeleteAsync();
//                break;
//        }
//    }
//    //右键编辑课程
//    private void EditCourse(object sender, RoutedEventArgs e)
//    {
//        int index = pivot.SelectedIndex;
//        switch (index)
//        {
//            case 1:
//                Grid mon_grid = (Grid)MonFiles.SelectedItem;
//                string str1 = "1"+","+ mon_grid.Name;
//                Frame.Navigate(typeof(EditPage), str1);
//                break;
//            case 2:
//                Grid tues_grid = (Grid)TuesFiles.SelectedItem;
//                string str2 = "2" + "," + tues_grid.Name;
//                Frame.Navigate(typeof(EditPage), str2);
//                break;
//            case 3:
//                Grid wed_grid = (Grid)WedFiles.SelectedItem;
//                string str3 = "3" + "," +wed_grid.Name;
//                Frame.Navigate(typeof(EditPage), str3);
//                break;
//            case 4:
//                Grid thurs_grid = (Grid)ThursFiles.SelectedItem;
//                string str4 = "4" + "," + thurs_grid.Name;
//                Frame.Navigate(typeof(EditPage), str4);
//                break;
//            case 5:
//                Grid fri_grid = (Grid)FriFiles.SelectedItem;
//                string str5 = "5" + "," + fri_grid.Name;
//                Frame.Navigate(typeof(EditPage),str5);
//                break;
//            case 6:
//                Grid sat_grid = (Grid)SatFiles.SelectedItem;
//                string str6 = "6" + "," + sat_grid.Name;
//                Frame.Navigate(typeof(EditPage), str6);
//                break;
//            case 0:
//                Grid sun_grid = (Grid)SunFiles.SelectedItem;
//                string str0 = "0" + "," + sun_grid.Name;
//                Frame.Navigate(typeof(EditPage), str0);
//                break;
//        }
//    }
//    /*****************************/

//    /*****************************/
//    //跳转到关于页面
//    private void AppBarButton_Click(object sender, RoutedEventArgs e)
//    {
//        Frame.Navigate(typeof(AboutPage));
//    }
//    //跳转到设置页面
//    private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
//    {
//        Frame.Navigate(typeof(SetPage));
//    }
//    //跳转到添加页面并传递参数pivot索引值
//    private void Add_Click(object sender, RoutedEventArgs e)
//    {
//        Frame.Navigate(typeof(AddItems), pivot.SelectedIndex);
//    }
//    /*****************************/

//    /*****************************/
//    //夜间模式
//    private void ChangedDark()
//    {
//        RequestedTheme = ElementTheme.Dark;
//    }
//    //日间模式
//    private void ChangedLight()
//    {
//        RequestedTheme = ElementTheme.Light;
//    }
//    /*****************************/
//}