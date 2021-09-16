using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uddata__MedDB
{
    class Menus
    {
        Views views = new Views();
        SQLMethods sqlMethods = new SQLMethods();
        public void WelcomeMenu()
        {
            bool validChar = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Velkommen");
                Console.WriteLine("Vælg et menupunkt:\n");
                Console.WriteLine("1) Opret lærer");
                Console.WriteLine("2) Opret elev");
                Console.WriteLine("3) Opret klasse");
                Console.WriteLine("4) Tiføj advarsel til elev");
                Console.WriteLine("5) Tiføj elev til fag");
                Console.WriteLine("6) Giv karakter");
                Console.WriteLine("7) Data Menue");
                Console.WriteLine("8) Afslut programmet");

                Char inputKey = Console.ReadKey(true).KeyChar;
                switch (inputKey)
                {
                    case '1':
                        validChar = true;
                        CreateTeacherMenu();
                        break;
                    case '2':
                        validChar = true;
                        CreateStudentMenu();
                        break;
                    case '3':
                        validChar = true;
                        CreateSubjectMenu();
                        break;
                    case '4':
                        validChar = true;
                        AddWarning();
                        break;
                    case '5':
                        validChar = true;
                        AddStudentToClassMenu();
                        break;
                    case '6':
                        validChar = true;
                        AddGrade();
                        break;
                    case '7':
                        validChar = true;
                        ViewMenu();
                        break;
                    case '8':
                        validChar = true;
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\n\nPrøv igen");
                        Console.ReadKey();
                        break;
                }
            } while (!validChar);
        }
        private void CreateTeacherMenu()
        {
            Teacher teacher = new Teacher();
            bool validName = false, validChoise = false;
            string validCoffee = "";
            teacher.coffeeClub = false;

            Console.Clear();

            do
            {
                Console.Write("Lærerens navn : ");
                teacher.name = Console.ReadLine();

                if (teacher.name == "")
                {
                    Console.SetCursorPosition(0, 4);
                    Console.WriteLine("fjel prøv igen");
                    Console.ReadKey();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("                        " +
                        "                                      ");
                    Console.SetCursorPosition(0, 0);
                }
                else
                {
                    validName = true;
                }

            } while (validName == false);

            do
            {
                Console.Write("Skal læreren være med i kaffe kluben? : ");
                validCoffee = Console.ReadLine().ToLower();

                if (validCoffee == "ja")
                {
                    validChoise = true;
                    teacher.coffeeClub = true;
                }
                else if (validCoffee == "nej")
                {
                    validChoise = true;
                }
                else
                {
                    Console.SetCursorPosition(0, 4);
                    Console.WriteLine("fjel prøv igen");
                    Console.ReadKey();
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine("                        " +
                        "                                      ");
                    Console.SetCursorPosition(0, 1);
                }

            } while (validChoise == false);

            int? id = sqlMethods.CreateTeacher(teacher);

            Console.WriteLine(teacher.name + "s id er " + id);
            Console.ReadKey();
        }
        private void CreateStudentMenu()
        {
            Student student = new Student();
            bool validName = false;

            Console.Clear();

            do
            {
                Console.Write("Elevens navn : ");
                student.name = Console.ReadLine();

                if (student.name == "")
                {
                    Console.SetCursorPosition(0, 4);
                    Console.WriteLine("fjel prøv igen");
                    Console.ReadKey();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("                        " +
                        "                                      ");
                    Console.SetCursorPosition(0, 0);
                }
                else
                {
                    validName = true;
                }
            } while (validName == false);

            int? id = sqlMethods.CreateStudent(student);

            Console.WriteLine(student.name + "s id er " + id);
            Console.ReadKey();
        }
        private void CreateSubjectMenu()
        {
            Subject subject = new Subject();
            List<string[]> teacherList = sqlMethods.TeacherSelect();
            bool validName = false;

            Console.Clear();

            views.TeacherListSelect(teacherList);
            views.Abbreviations(subject);

            do
            {
                Console.Write("Fagets navn : ");
                subject.name = Console.ReadLine();

                if (subject.name == "")
                {
                    Console.WriteLine("fjel prøv igen");
                    Console.ReadKey();
                }
                else
                {
                    validName = true;
                }
            } while (validName == false);

            Console.Write("Laerens ID : ");
            subject.teacherId = Convert.ToInt32(Console.ReadLine());

            int? id = sqlMethods.CreateSubject(subject);

            Console.WriteLine(subject.name + "s id er " + id);
            Console.ReadKey();
        }
        private void AddWarning()
        {
            Warnings warnings = new Warnings();
            List<Student> studentList = sqlMethods.StudentSelect();
            bool validImput = false, validImput2 = false;

            views.StudentListSelect(studentList);

            do
            {
                Console.WriteLine("Skriv Elevens Id:");
                validImput = int.TryParse(Console.ReadLine(), out int studentId);
                warnings.studentId = studentId;
                if (validImput == false)
                {
                    Console.WriteLine("Fejl det var ikke et tal prøv igen");
                    Console.ReadKey();
                }

            } while (validImput == false);

            do
            {
                Console.WriteLine("Skriv det nye antal advarseler");
                validImput2 = int.TryParse(Console.ReadLine(), out int warning);
                warnings.warnings = warning;
                if (validImput2 == false)
                {
                    Console.WriteLine("Fejl det var ikke et tal prøv igen");
                    Console.ReadKey();
                }

            } while (validImput2 == false);

        }
        private void AddStudentToClassMenu()
        {

            StudentSubject studentSubject = new StudentSubject();
            List<string[]> subjectList = sqlMethods.SubjectSelect();

            Console.Clear();

            views.SubjectListSelect(subjectList);

            Console.Write("Elevens ID : ");
            studentSubject.studentId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Fagets ID : ");
            studentSubject.subjectId = Convert.ToInt32(Console.ReadLine());

            int? id = sqlMethods.AddStudentToSubject(studentSubject);
            Console.ReadKey();
        }
        private void AddGrade()
        {
            Grade grade = new Grade();

            Console.Clear();


            Console.WriteLine("Eleven id : ");
            grade.studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Klassens id : ");
            grade.subjectId = int.Parse(Console.ReadLine());

            Console.WriteLine("Karakteren : ");
            grade.grade = int.Parse(Console.ReadLine());

            sqlMethods.AddGrade(grade);
        }
        private void ViewMenu()
        {
            bool validChar = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Velkommen");
                Console.WriteLine("Vælg et menupunkt:\n");
                Console.WriteLine("1) Laerer data");
                Console.WriteLine("2) Elev data");
                Console.WriteLine("3) Klasse data");
                Console.WriteLine("4) All data"); ;
                Console.WriteLine("5) Afslut programmet");

                Char inputKey = Console.ReadKey(true).KeyChar;
                switch (inputKey)
                {
                    case '1':
                        validChar = true;
                        TeacherViewMenu();
                        break;
                    case '2':
                        validChar = true;
                        StudentViewMenu();
                        break;
                    case '3':
                        validChar = true;
                        SubjectViewMenu();
                        break;
                    case '4':
                        validChar = true;
                        AllViewMenu();
                        break;
                    case '5':
                        validChar = true;
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\n\nPrøv igen");
                        Console.ReadKey();
                        break;
                }
            } while (!validChar);
        }
        private void TeacherViewMenu()
        {
            List<string[]> teacherView = sqlMethods.TeacherViewSelect();
            views.TeacherView(teacherView);
        }
        private void StudentViewMenu()
        {
            List<string[]> studentView = sqlMethods.StudentViewSelect();
            views.StudentView(studentView);
        }
        private void SubjectViewMenu()
        {
            List<string[]> subjectView = sqlMethods.SubjectViewSelect();
            views.SubjectView(subjectView);
        }
        private void AllViewMenu()
        {
            List<string[]> allView = sqlMethods.AllViewSelect();
            views.AllView(allView);
        }


    }
}
