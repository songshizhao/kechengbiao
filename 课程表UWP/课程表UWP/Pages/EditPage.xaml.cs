using System;
using System.IO;
using System.Xml;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using 课程表UWP.data;


namespace 课程表UWP.Pages
{

    public sealed partial class EditPage : Page
    {
        //新建课程对象
        Course acourse = new Course();


        public EditPage()
        {
            this.InitializeComponent();
           
            //acourse.PropertyChanged += Acourse_PropertyChanged;
        }

        string day;//dayofweek
        string classindex;//课节

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //导航传来参数
            string[] index = (string[])e.Parameter;
            day = index[0];
            classindex = index[1];

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile  XmlFileInLocal;
           
            //读xml文件数据
            XmlFileInLocal = await folder.GetFileAsync("class");
            Stream XmlReader = await XmlFileInLocal.OpenStreamForReadAsync();
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(XmlReader);
            XmlReader.Dispose();

            foreach (var element in XmlDoc.DocumentElement)
            {
                XmlElement each_element = (XmlElement)element;
                if (each_element.Name==day)
                {
                    foreach (var InnerElement in each_element)
                    {
                        XmlElement each_InnerElement = (XmlElement)InnerElement;
                        if (each_InnerElement.GetAttribute("index")==classindex)
                        {
                            acourse.class_name_property = each_InnerElement.GetAttribute("name");
                            acourse.class_duration_property = each_InnerElement.GetAttribute("duration");
                            acourse.class_tag_property = each_InnerElement.GetAttribute("tag");
                            acourse.class_room_property = each_InnerElement.GetAttribute("room");
                            acourse.class_teacher_property = each_InnerElement.GetAttribute("teacher");
                            acourse.class_weeklimit_property = each_InnerElement.GetAttribute("weeklimit");
                            //将课程属性绑定到整个grid元素

                            acourse.class_startTime = each_InnerElement.GetAttribute("startTime");
                            acourse.class_endTime = each_InnerElement.GetAttribute("endTime");
                            //acourse.class_endTime = "08:30";
              
                            var start_hour = acourse.class_startTime.Substring(0,2);
                            var start_minute = acourse.class_startTime.Substring(3,2);

                            var end_hour = acourse.class_endTime.Substring(0, 2);
                            var end_minute = acourse.class_endTime.Substring(3, 2);

                            try
                            {
                                acourse.startTime = new TimeSpan(Convert.ToInt32(start_hour), Convert.ToInt32(start_minute), 0);
                                acourse.endTime = new TimeSpan(Convert.ToInt32(end_hour), Convert.ToInt32(end_minute), 0);
                            }
                            catch (Exception)
                            {
                            }
                            
                            MainGrid.DataContext = acourse;

                        }

                    }
                }

            }
            


        }



        ////保存
        private async void save_Click(object sender, RoutedEventArgs e)
        {

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile XmlFileInLocal;

            //读现有xml到内存
            XmlFileInLocal = await folder.GetFileAsync("class");
            Stream XmlReader = await XmlFileInLocal.OpenStreamForReadAsync();
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(XmlReader);
            XmlReader.Dispose();
            //修改内存中xml
            foreach (var element in XmlDoc.DocumentElement)
            {
                XmlElement each_element = (XmlElement)element;
                if (each_element.Name == day)
                {
                    foreach (var InnerElement in each_element)
                    {
                        XmlElement each_InnerElement = (XmlElement)InnerElement;
                        if (each_InnerElement.GetAttribute("index") == classindex)
                        {
                            //Debug.WriteLine(acourse.class_name_property+"#######################");
                            each_InnerElement.SetAttribute("name", acourse.class_name_property);
                            each_InnerElement.SetAttribute("duration", acourse.class_duration_property);
                            each_InnerElement.SetAttribute("tag", acourse.class_tag_property);
                            each_InnerElement.SetAttribute("room", acourse.class_room_property);
                            each_InnerElement.SetAttribute("teacher", acourse.class_teacher_property);
                            each_InnerElement.SetAttribute("weeklimit", acourse.class_weeklimit_property);
                            each_InnerElement.SetAttribute("finished", "0");

                            string st = start_time.Time.ToString().Remove(start_time.Time.ToString().Length - 3, 3);
                            string et = end_time.Time.ToString().Remove(end_time.Time.ToString().Length - 3, 3);

                            each_InnerElement.SetAttribute("startTime", st);
                            each_InnerElement.SetAttribute("endTime", et);

                            //Debug.WriteLine(each_InnerElement.GetAttribute("name") + "!!!!!");
                        }

                    }
                }

            }

            ////保存xml到local
            StorageFile xml = await folder.CreateFileAsync("class", CreationCollisionOption.ReplaceExisting);
            Stream fileWriter = await xml.OpenStreamForWriteAsync();
            XmlDoc.Save(fileWriter);
            fileWriter.Dispose();



            ////判断是否有信息输入
            //if (nameTxt.Text == "" && weekTxt.Text == "" && roomTxt.Text == "" && teacherTxt.Text == "")
            //{
            //    await new MessageDialog("请至少输入一项信息!").ShowAsync();
            //    return;
            //}
            //else
            //{
            //    //有信息输入
            //    //创建一个xml对象
            //    XmlDocument _doc = new XmlDocument();
            //    //使用课程名称创建一个根节点
            //    XmlElement _item = _doc.CreateElement(nameTxt.Text);
            //    _item.SetAttribute("coursename", nameTxt.Text);
            //    _item.SetAttribute("startTime", start_time.Time.ToString());
            //    _item.SetAttribute("endTime", end_time.Time.ToString());
            //    _item.SetAttribute("week", weekTxt.Text);
            //    _item.SetAttribute("room", roomTxt.Text);
            //    _item.SetAttribute("teacher", teacherTxt.Text);
            //    _item.SetAttribute("sdmode", sOrd);

            //    /*************************/

            //    /*************************/
            //    _doc.AppendChild(_item);
            //    //保存信息到相应的文件夹
            //    switch (str1)
            //    {
            //        case 1:
            //            StorageFolder Monfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Monlist");
            //            //创建一个应用文件
            //            StorageFile mon_file = await Monfolder.CreateFileAsync(nameTxt.Text + "zhf" + ".xml", CreationCollisionOption.ReplaceExisting);
            //            //把xml信息保存到文件中
            //            await _doc.SaveToFileAsync(mon_file);
            //            break;
            //        case 2:
            //            StorageFolder Tuesfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Tueslist");
            //            //创建一个应用文件
            //            StorageFile tues_file = await Tuesfolder.CreateFileAsync(nameTxt.Text + "zhf" + ".xml", CreationCollisionOption.ReplaceExisting);
            //            //把xml信息保存到文件中
            //            await _doc.SaveToFileAsync(tues_file);
            //            break;
            //        case 3:
            //            StorageFolder Wedfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Wedlist");
            //            //创建一个应用文件
            //            StorageFile wed_file = await Wedfolder.CreateFileAsync(nameTxt.Text + "zhf" + ".xml", CreationCollisionOption.ReplaceExisting);
            //            //把xml信息保存到文件中
            //            await _doc.SaveToFileAsync(wed_file);
            //            break;
            //        case 4:
            //            StorageFolder Thursfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Thurslist");
            //            //创建一个应用文件
            //            StorageFile thurs_file = await Thursfolder.CreateFileAsync(nameTxt.Text + "zhf" + ".xml", CreationCollisionOption.ReplaceExisting);
            //            //把xml信息保存到文件中
            //            await _doc.SaveToFileAsync(thurs_file);
            //            break;
            //        case 5:
            //            StorageFolder Frifolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Frilist");
            //            //创建一个应用文件
            //            StorageFile fri_file = await Frifolder.CreateFileAsync(nameTxt.Text + "zhf" + ".xml", CreationCollisionOption.ReplaceExisting);
            //            //把xml信息保存到文件中
            //            await _doc.SaveToFileAsync(fri_file);
            //            break;
            //        case 6:
            //            StorageFolder Satfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Satlist");
            //            //创建一个应用文件
            //            StorageFile sat_file = await Satfolder.CreateFileAsync(nameTxt.Text + "zhf" + ".xml", CreationCollisionOption.ReplaceExisting);
            //            //把xml信息保存到文件中
            //            await _doc.SaveToFileAsync(sat_file);
            //            break;
            //        case 0:
            //            StorageFolder Sunfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Sunlist");
            //            //创建一个应用文件
            //            StorageFile sun_file = await Sunfolder.CreateFileAsync(nameTxt.Text + "zhf" + ".xml", CreationCollisionOption.ReplaceExisting);
            //            //把xml信息保存到文件中
            //            await _doc.SaveToFileAsync(sun_file);
            //            break;
            //        default: break;
            //    }
            //    //显示消息框
            //    // await new MessageDialog("保存成功!").ShowAsync();
            //}
            ////添加完成（保存）返回主页
            Frame.GoBack();
        }
        //取消
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        //单双周设置
        private void SorD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int index = SorD.SelectedIndex;
            //switch (index)
            //{
            //    case 0:
            //        sOrd = "无";
            //        break;
            //    case 1:
            //        sOrd = "单周";
            //        break;
            //    case 2:
            //        sOrd = "双周";
            //        break;
            //}
        }
        /************************************/

        /************************************/
        //加载夜间模式设置
        //private void GetSetting()
        //{
        //    if (container.Values["Theme"] != null)
        //    {
        //        if (container.Values["Theme"].ToString() == "夜间模式")
        //        {
        //            ChangedDark();
        //        }
        //        else
        //        {
        //            ChangedLight();
        //        }
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
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
