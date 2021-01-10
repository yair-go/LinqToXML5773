using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Data;

namespace XmlSeralizerExample
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public enum Gender { male, female }

    public class Student
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public bool IsMarried { get; set; }
        public DateTime PersonDate { get; set; }
        public Gender StudentGender { get; set; }
        public List<Course> Courses { get; set; }

        public Student()
        {
            Courses = new List<Course>();
        }
    }

    class Program
    {
        private static List<Student> getList()
        {
            List<Student> studentList;
            List<Course> courseList;
            courseList = new List<Course>();
            string[] courseNameArray = new string[] { "c#", "java", "מבנה המחשב", "c++", "אינפי", "בדידה", "מבוא לתכנות" };
            for (int i = 0; i < courseNameArray.Length; i++)
                courseList.Add(new Course { Id = i, Name = courseNameArray[i] });

            string[] studentNameArray = new string[] { "אברהם", "יצחק", "יעקוב", "שרה", "רחל" };
            int id = -1;

            studentList = new List<Student>
            {
                new Student {Id = ++id ,
                    IsMarried = true ,
                    PersonDate = DateTime.Parse("7.12.1981"),
                    PersonName = studentNameArray[id],
                    StudentGender =  Gender.male             
                },
                new Student {Id = ++id ,
                    IsMarried = false ,
                    PersonDate = DateTime.Parse("31.12.1985"),
                    PersonName = studentNameArray[id],
                    StudentGender = Gender.male     
                    ,
                  Courses = new List<Course>{courseList[3],courseList[2],courseList[1]}
                },
                new Student {Id = ++id ,
                    IsMarried = true ,
                    PersonDate = DateTime.Parse("13.12.1971"),
                    PersonName = studentNameArray[id],
                    StudentGender = Gender.male  
                    ,
                  Courses = new List<Course>{courseList[6],courseList[2],courseList[4]}
                },
                new Student {Id = ++id ,
                    IsMarried = false ,
                    PersonDate = DateTime.Parse("13.12.1985"),
                    PersonName = studentNameArray[id],
                    StudentGender = Gender.female ,
                  Courses = new List<Course>{courseList[1],courseList[2],courseList[5]}
                },
                new Student {Id = ++id ,
                    IsMarried = true ,
                    PersonDate = DateTime.Parse("13.12.1985"),
                    PersonName = studentNameArray[id],
                    StudentGender = Gender.female   
                    ,
                  Courses = new List<Course>{courseList[2],courseList[3],courseList[5]}
                },
            };

            return studentList;
        }

        public static void saveListToXML(List<Student> list, string path)
        {
            XmlSerializer x = new XmlSerializer(list.GetType());
            FileStream fs = new FileStream(path, FileMode.Create);
            x.Serialize(fs, list);
            fs.Close();
        }

        public static IEnumerable<Student> loadListFromXML(string path)
        {
            IEnumerable<Student> list;
            XmlSerializer x = new XmlSerializer(typeof(List<Student>));
            FileStream fs = new FileStream(path, FileMode.Open);
            list = (IEnumerable<Student>)x.Deserialize(fs);
            return list;

        }


        static void Main(string[] args)
        {
           List<Student> list = getList();        

            string path = "xmlBySerilalizer2.xml";
           
            saveListToXML(list,path);
            IEnumerable<Student> list2 = loadListFromXML(path);

            //Student s = new Student { Id = 43, PersonName = "ddjjd" };
            //XmlSerializer x = new XmlSerializer(s.GetType());
            //FileStream fs = new FileStream("11"+path, FileMode.Create);
            //x.Serialize(fs, s);

            Console.ReadLine();
        }
    }
}
