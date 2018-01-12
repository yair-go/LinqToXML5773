using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;
namespace LinqToXML5772
{
    class XmlSample
    {
        XElement studentRoot;
        
        //string studentPath = @"C:\JCT\Courses\sem A\C#miniProject\Example Code\8\StudentXml.xml";
        string studentPath = @"StudentXml.xml";

        public XmlSample()
        {
          
            if (!File.Exists(studentPath))
                CreateFiles();
            else
                LoadData();
        }

        private void CreateFiles()
        {
            studentRoot = new XElement("students");
            studentRoot.Save(studentPath);
        }

        private void LoadData()
        {
            try
            {
                studentRoot = XElement.Load(studentPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        public void SaveStudentList(List<Student> studentList)
        {
            studentRoot = new XElement("students");

            foreach (Student item in studentList)
            {
                //XElement id = new XElement("id", item.Id);
                //XElement firstName = new XElement("firstName", item.FirstName);
                //XElement lastName = new XElement("lastName", item.LastName);

                //XElement name = new XElement("name", firstName, lastName);
                //XElement student = new XElement("student", id, name);

                //studentRoot.Add(student);

                AddStudent(item);
            }
           
            studentRoot.Save(studentPath);
        }
        public void SaveStudentListLinq(List<Student> studentList)
        {
            var v =   from p in studentList
                                    select new XElement("student",
                                        new XElement("id", p.Id),
                                       new XElement("name",
                                            new XElement("firstName", p.FirstName),
                                            new XElement("lastName", p.LastName)
                                            )
                                        );

            studentRoot = new XElement("students",v);
                                  
                                  
            studentRoot.Save(studentPath);
        }

        public List<Student> GetStudentList()
        {
            LoadData();
            List<Student> students;
            try
            {
                students = (from p in studentRoot.Elements()
                            select new Student()
                            {
                                Id = Convert.ToInt32(p.Element("id").Value),
                                FirstName = p.Element("name").Element("firstName").Value,
                                LastName = p.Element("name").Element("lastName").Value
                            }).ToList();
            }
            catch
            {
                students = null;
            }
            return students;
        }

        public Student GetStudent(int id)
        {
            LoadData();
            Student student;
            try
            {
                student = (from p in studentRoot.Elements()
                           where Convert.ToInt32(p.Element("id").Value) == id
                           select new Student()
                           {
                               Id = Convert.ToInt32(p.Element("id").Value),
                               FirstName = p.Element("name").Element("firstName").Value,
                               LastName = p.Element("name").Element("lastName").Value
                           }).FirstOrDefault();
            }
            catch
            {
                student = null;
            }
            return student;
        }

        public string GetStudentName(int id)
        {
            LoadData();
            string studentName;
            try
            {
                studentName = (from p in studentRoot.Elements()
                               where Convert.ToInt32(p.Element("id").Value) == id
                               select p.Element("name").Element("firstName").Value
                               + " " +
                                  p.Element("name").Element("lastName").Value
                                  ).FirstOrDefault();
            }
            catch
            {
                studentName = null;
            }
            return studentName;
        }

        public void AddStudent(Student student)
        {
            XElement id = new XElement("id", student.Id);
            XElement firstName = new XElement("firstName", student.FirstName);
            XElement lastName = new XElement("lastName", student.LastName);
            XElement name = new XElement("name", firstName, lastName);
            XElement s = new XElement("student", id, name);
            studentRoot.Add(s);
            studentRoot.Save(studentPath);
        }

        public bool RemoveStudent(int id)
        {
            XElement studentElement;
            try
            {
                studentElement = (from p in studentRoot.Elements()
                                  where Convert.ToInt32(p.Element("id").Value) == id
                                  select p).FirstOrDefault();
                studentElement.Remove();
                studentRoot.Save(studentPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void UpdateStudent(Student student)
        {
            XElement studentElement = (from p in studentRoot.Elements()
                                       where Convert.ToInt32(p.Element("id").Value) == student.Id
                                       select p).FirstOrDefault();
         //   XElement studentElement2 = studentRoot.Elements().Where(p => Convert.ToInt32(p.Element("id").Value) == student.Id).FirstOrDefault();
            studentElement.Element("name").Element("firstName").Value = student.FirstName;
            studentElement.Element("name").Element("lastName").Value = student.LastName;

            studentRoot.Save(studentPath);
        }
    }
}
