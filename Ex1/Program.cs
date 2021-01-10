using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            XElement name = new XElement("name", "oshri");
            XElement id = new XElement("ID", 123);
            XElement student = new XElement("student", id, name);

            student.Save("temp2TAL.xml");

            XElement students = XElement.Load("tempTAL.xml");

         IEnumerable<XElement> list = students.Elements();
            
         foreach (var item in list)
         {
             string studentName = item.Element("name").Value;
             XElement x = new XElement("mail", studentName + "@jct.ac.il");
             item.AddFirst(x);
             Console.WriteLine(item);
             Console.WriteLine();
         }

           // <name/>  
        
        }
    }
}
