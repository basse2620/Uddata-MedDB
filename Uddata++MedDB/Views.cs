using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uddata__MedDB
{
    class Views
    {
        public void TeacherListSelect(List<string[]> teacherListSelect)
        {
            foreach (string[] teacher in teacherListSelect)
            {
                Console.WriteLine("Laerer id: " + teacher[0] +
                    " Learer navn: " + teacher[1]
                    );
            }
            Console.ReadKey();
        }
        public void StudentListSelect(List<Student> studentList)
        {
            foreach (var student in studentList)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(student))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(student);

                    Console.WriteLine($"{name}: {value}");
                }
            }
        }
        public void SubjectListSelect(List<string[]> subjectList)
        {
            foreach (string[] subject in subjectList)
            {
                Console.WriteLine("Fagets id: " + subject[0] +
                    " Fagets navn: " + subject[1]
                    );
            }
            Console.ReadKey();
        }
        public void TeacherView(List<string[]> teacherView)
        {            
            foreach (string[] teacher in teacherView)
            {                
                Console.WriteLine("Laerer id: " + teacher[0] + 
                    " Learer navn: " + teacher[1] + 
                    " Kaffe Club: " + teacher[2] +
                    " Fagets id: " + teacher[3] +
                    " Fagets navn: " + teacher[4] +
                    " Fagets forkrtelse: " + teacher[5]
                    );
            }
            Console.ReadKey();
        }
        public void StudentView(List<string[]> studentView)
        {
            foreach (string[] student in studentView)
            {
                Console.WriteLine("Elevens id: " + student[0] +
                    " Elevens navn: " + student[1] +
                    " advarseler: " + student[2] +
                    " Fagets id: " + student[3] +
                    " Fagets navn: " + student[4] +
                    " Fagets forkrtelse: " + student[5] + 
                    " Karaktere " + student[6]
                    );
            }
            Console.ReadKey();
        }
        public void SubjectView(List<string[]> subjectView)
        {
            foreach (string[] subject in subjectView)
            {
                Console.WriteLine("Fagets id: " + subject[0] +
                    " Fagets navn: " + subject[1] +
                    " Fagets forkrtelse: " + subject[2] +
                    " Laererens id: " + subject[3] +
                    " Laererens navn: " + subject[4] +
                    " Elevens id: " + subject[5] +
                    " Elevens navn: " + subject[6]
                    );
            }
            Console.ReadKey();
        }
        public void AllView(List<string[]> allView)
        {
            foreach (string[] all in allView)
            {
                Console.WriteLine("Fagets id: " + all[0] +
                    " Fagets navn: " + all[1] +
                    " Fagets forkrtelse: " + all[2] +
                    " Klasse id: " + all[3] +
                    " Klasse navn: " + all[4] +
                    " Klasse forkrtelse: " + all[5]
                    );
            }
            Console.ReadKey();
        }
        public void Abbreviations(Subject subject)
        {
            foreach (var abbreviation in Enum.GetValues(typeof(Abbreviation)))
            { Console.WriteLine((int)abbreviation + " " + abbreviation.ToString()); }
            Console.Write("Forkårtelse: ");
            Abbreviation a = new Abbreviation();
            while (!Enum.TryParse(Console.ReadLine(), out a))
            { Console.WriteLine("Forket imput prøv igen."); }
            subject.fag = a;
        }
    }
}
