using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uddata__MedDB
{
    class SQLMethods
    {
        string connectionString = "Data Source=.;Initial Catalog=UddataDB;Integrated Security=True";
        string sql = null;

        public int? CreateTeacher(Teacher teacher)
        {
            sql = "Insert Into Teacher (teacherName, coffeeClub) OUTPUT Inserted.teacherId values (@teacherName, @coffeeClub)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@teacherName", SqlDbType.NVarChar).Value = teacher.name;
                        cmd.Parameters.Add("@coffeeClub", SqlDbType.Bit).Value = teacher.coffeeClub;

                        int? id = (int?)cmd.ExecuteScalar();
                        return id;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fejl" + ex.Message);
                    return null;
                }
            }

        }

        public int? CreateStudent(Student student)
        {
            sql = "Insert Into Student (studentName, warnings) OUTPUT Inserted.studentId values (@studentName, @warnings)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@studentName", SqlDbType.NVarChar).Value = student.name;
                        int? id = (int?)cmd.ExecuteScalar();
                        return id;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fejl" + ex.Message);
                    return null;
                }
            }
        }

        public int? CreateSubject(Subject subject)
        {
            sql = "Insert Into Subject (subjectName, abbreviation, FK_teacherId) OUTPUT Inserted.subjectId values (@subjectName, @abbreviation, @FK_teacherId)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@subjectName", SqlDbType.NVarChar).Value = subject.name;
                        cmd.Parameters.Add("@abbreviation", SqlDbType.NVarChar).Value = subject.fag;
                        cmd.Parameters.Add("@FK_teacherId", SqlDbType.Int).Value = subject.teacherId;

                        int? id = (int?)cmd.ExecuteScalar();
                        return id;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fejl" + ex.Message);
                    return null;
                }
            }
        }
        public int? AddWarningToStudent(Warnings warnings)
        {
            sql = "Insert Into Warnings (warnings, FK_studentId) values (@warnings, @FK_studentId)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@warnings", SqlDbType.Int).Value = warnings.warnings;
                        cmd.Parameters.Add("@FK_studentId", SqlDbType.Int).Value = warnings.studentId;

                        int? id = (int?)cmd.ExecuteScalar();
                        return id;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fejl" + ex.Message);
                    return null;
                }
            }
        }
        public int? AddStudentToSubject(StudentSubject studentSubject)
        {
            sql = "Insert Into StudentSubject (FK_studentId, FK_subjectId) values (@FK_studentId, @FK_subjectId)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@FK_studentId", SqlDbType.Int).Value = studentSubject.studentId;
                        cmd.Parameters.Add("@FK_subjectId", SqlDbType.Int).Value = studentSubject.subjectId;

                        int? id = (int?)cmd.ExecuteScalar();
                        return id;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fejl" + ex.Message);
                    return null;
                }
            }
        }
        public int? AddGrade(Grade grade)
        {
            sql = "Insert Into Grade (grade, FK_studentId, FK_subjectId) values (@grade, @FK_studentId, @FK_subjectId)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@grade", SqlDbType.Int).Value = grade.grade;
                        cmd.Parameters.Add("@FK_subjectId", SqlDbType.Int).Value = grade.subjectId;
                        cmd.Parameters.Add("@FK_subjectId", SqlDbType.Int).Value = grade.subjectId;

                        int? id = (int?)cmd.ExecuteScalar();
                        return id;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fejl" + ex.Message);
                    return null;
                }
            }
        }
        public List<string[]> TeacherSelect()
        {
            List<string[]> teacherList = new List<string[]>();
            sql = $"Select teacherId, teacherName From Teacher";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string[] fields = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fields[i] = reader[i].ToString();
                        }
                        teacherList.Add(fields);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR:" + ex.GetType() + ex.Message);
                    return null;
                }
            }
            return teacherList;
        }
        public List<Student> StudentSelect()
        {
            List<Student> studentList = new List<Student>();
            sql = $"Select studentId, studentName From Student";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        studentList.Add(
                            new Student()
                            {
                                id = (int)reader[0],
                                name = (string)reader[1]
                            });
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR:" + ex.GetType() + ex.Message);
                    return null;
                }
            }
            return studentList;
        }
        public List<string[]> SubjectSelect()
        {
            List<string[]> subjectList = new List<string[]>();
            sql = $"Select subjectId, subjectName From Subject";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string[] fields = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fields[i] = reader[i].ToString();
                        }
                        subjectList.Add(fields);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR:" + ex.GetType() + ex.Message);
                    return null;
                }
            }
            return subjectList;
        }
        public List<string[]> TeacherViewSelect()
        {
            List<string[]> teacherView = new List<string[]>();
            sql = $"Select * From TeacherView";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string[] fields = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fields[i] = reader[i].ToString();
                        }
                        teacherView.Add(fields);                        
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR:" + ex.GetType() + ex.Message);
                    return null;
                }
            }
            return teacherView;
        }
        public List<string[]> StudentViewSelect()
        {
            List<string[]> studentView = new List<string[]>();
            sql = $"Select * From StudentView";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string[] fields = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fields[i] = reader[i].ToString();
                        }
                        studentView.Add(fields);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR:" + ex.GetType() + ex.Message);
                    return null;
                }
            }
            return studentView;
        }
        public List<string[]> SubjectViewSelect()
        {
            List<string[]> subjectView = new List<string[]>();
            sql = $"Select * From SubjectView";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string[] fields = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fields[i] = reader[i].ToString();
                        }
                        subjectView.Add(fields);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR:" + ex.GetType() + ex.Message);
                    return null;
                }
            }
            return subjectView;
        }
        public List<string[]> AllViewSelect()
        {
            List<string[]> allView = new List<string[]>();
            sql = $"Select * From AllView";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string[] fields = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fields[i] = reader[i].ToString();
                        }
                        allView.Add(fields);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR:" + ex.GetType() + ex.Message);
                    return null;
                }
            }
            return allView;
        }
    }
}
