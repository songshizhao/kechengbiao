using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
//双向绑定类
//https://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-2
//
using System.Threading.Tasks;

namespace 课程表UWP.data
{
    
    class Course : INotifyPropertyChanged
    {
        private string class_name;
        private string[] class_index;
        private string class_duration;
        private string class_tag;
        private string class_room;
        private string class_teacher;
        private string class_weeklimit;
        private string class_finished;


        public Course()
        {
            class_index = new string[2];
        }

        public string class_startTime { get; set; }
        public string class_endTime { get; set; }

        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }


        /// <summary>
        /// 课程名
        /// </summary>
        public string class_name_property
        {
            get
            {
                return class_name;
            }

            set
            {
                if (value != class_name)
                {
                    class_name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 课程时间,数组（星期，课节），例如（monday，1）是星期一的第一节课
        /// </summary>
        public string[] class_index_property
        {
            get
            {
                return class_index;
            }

            set
            {
                if (value != class_index)
                {
                    class_index = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 持续时间，例如：18=18周
        /// </summary>
        public string class_duration_property
        {
            get
            {
                return class_duration;
            }

            set
            {
                if (value != class_duration)
                {
                    class_duration = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 课程类型：主修？选修？旁听？专业课？
        /// </summary>
        public string class_tag_property
        {
            get
            {
                return class_tag;
            }

            set
            {
                if (value != class_tag)
                {
                    class_tag = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 教室
        /// </summary>
        public string class_room_property
        {
            get
            {
                return class_room;
            }

            set
            {
                if (value != class_room)
                {
                    class_room = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        ///教师
        /// </summary>
        public string class_teacher_property
        {
            get
            {
                return class_teacher;
            }

            set
            {
                if (value != class_teacher)
                {
                    class_teacher = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        ///单双周限制
        /// </summary>
        public string class_weeklimit_property
        {
            get
            {
                return class_weeklimit;
            }

            set
            {
                if (value != class_weeklimit)
                {
                    class_weeklimit = value;
                    NotifyPropertyChanged();
                }
            }
        }



        /// <summary>
        /// 课程名
        /// </summary>
        public string class_finished_property
        {
            get
            {
                return class_finished;
            }

            set
            {
                if (value != class_finished)
                {
                    class_finished = value;
                    NotifyPropertyChanged();
                }
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
