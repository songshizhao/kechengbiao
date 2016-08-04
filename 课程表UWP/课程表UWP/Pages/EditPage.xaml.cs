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
        string day;//dayofweek
        string classindex;//课节

        public EditPage()
        {
            this.InitializeComponent();
           
            //acourse.PropertyChanged += Acourse_PropertyChanged;
        }



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
                            acourse.class_score_property = each_InnerElement.GetAttribute("score");
                            acourse.class_type_property = Convert.ToInt32(each_InnerElement.GetAttribute("type"));
                            acourse.class_room_property = each_InnerElement.GetAttribute("room");
                            acourse.class_teacher_property = each_InnerElement.GetAttribute("teacher");
                            acourse.class_weeklimit_property =Convert.ToInt32( each_InnerElement.GetAttribute("weeklimit"));
                            //将课程属性绑定到整个grid元素

                            acourse.class_startTime = each_InnerElement.GetAttribute("startTime");
                            acourse.class_endTime = each_InnerElement.GetAttribute("endTime");
        
              
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
                            each_InnerElement.SetAttribute("type", acourse.class_type_property.ToString());
                            each_InnerElement.SetAttribute("room", acourse.class_room_property);
                            each_InnerElement.SetAttribute("score", acourse.class_score_property);
                            each_InnerElement.SetAttribute("teacher", acourse.class_teacher_property);
                            each_InnerElement.SetAttribute("weeklimit", acourse.class_weeklimit_property.ToString());
                            each_InnerElement.SetAttribute("finished", "0");

                            string st = start_time.Time.ToString().Remove(start_time.Time.ToString().Length - 3, 3);
                            string et = end_time.Time.ToString().Remove(end_time.Time.ToString().Length - 3, 3);

                            each_InnerElement.SetAttribute("startTime", st);
                            each_InnerElement.SetAttribute("endTime", et);

                            //Debug.WriteLine(each_InnerElement.GetAttribute("name") + "!!!!!");
                        }

                    }
                }
                else
                {
                    foreach (var InnerElement in each_element)
                    {
                        XmlElement each_InnerElement = (XmlElement)InnerElement;
                        if (each_InnerElement.GetAttribute("index") == classindex)
                        {
                           
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

            Frame.GoBack();
        }
        //取消
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
       
    }
}
