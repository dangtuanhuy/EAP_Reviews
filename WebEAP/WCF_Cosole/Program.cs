using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF_Cosole.ServiceReference1;

namespace WCF_Cosole
{
    class Program
    {
        static Service1Client db = new Service1Client();

        //Hiển thị danh sách
        public static void GetAll()
        {
            var data = db.GetStudents();
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine("Students " + (i + 1) + ": ");
                Console.WriteLine("Student Code: " + data[i].Student_Code);
                Console.WriteLine("Student Name: " + data[i].Student_Name);
                Console.WriteLine("Student Email: " + data[i].Student_Email);
                Console.WriteLine("Student Phone: " + data[i].Student_Phone);
                Console.WriteLine("Student Details" + data[i].Student_Details);
            }
        }
        //Tìm Kiếm
        public static void GetStudent(string id)
        {
            var data = db.GetStudent(id);
            Console.WriteLine("Student Code: " + data.Student_Code);
            Console.WriteLine("Student Name: " + data.Student_Name);
            Console.WriteLine("Student Email: " + data.Student_Email);
            Console.WriteLine("Student Phone: " + data.Student_Phone);
            Console.WriteLine("Student Details: " + data.Student_Details);
        }

        public static void addStudent(ServiceReference1.Student student)
        {
            var add = db.AddNewStudent(student);
            if (add)
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine("false");
            }

        }

        public static void UpdateStudent(ServiceReference1.Student student)
        {
            var update = db.UpdateStudent(student);
            if (update)
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine("false");
            }
        }
        public static void DeleteStudent(string id)
        {
            var delete = db.DeleteStudent(id);
            if (delete)
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine("false");
            }
        }

        static void Main(string[] args)
        {
        reset:
            Console.WriteLine("Select an option: ");
            Console.WriteLine("1. Get all student's information");
            Console.WriteLine("2. Get a student information");
            Console.WriteLine("3. Update student information");
            Console.WriteLine("4. Delete a student ");
            Console.WriteLine("5. Add new student");
            Console.Write("Your choice: ");

            int c = Convert.ToInt32(Console.ReadLine());
            switch (c)
            {
                case 1:
                    GetAll();
                    goto reset;
                    break;
                case 2:
                    Console.Write("Enter Student Code: ");
                    // int i = Convert.ToInt32(Console.ReadLine());
                    string i = Console.ReadLine();
                    GetStudent(i);
                    goto reset;
                    break;
                case 3:
                    ServiceReference1.Student students = new Student();
                    Console.Write("Student Code update: ");
                    //int id1 = Convert.ToInt32(Console.ReadLine());
                    string id1 = Console.ReadLine();
                    students.Student_Code = id1;

                    Console.Write("Student Name update: ");
                    string name1 = Console.ReadLine();
                    students.Student_Name = name1;

                    Console.Write("Student Phone update: ");
                    string phone1 = Console.ReadLine();
                    students.Student_Phone = phone1;


                    Console.Write("Student Email update: ");
                    string email1 = Console.ReadLine();
                    students.Student_Email = email1;


                    Console.Write("Student Details update: ");
                    string details = Console.ReadLine();
                    students.Student_Details = details;
                    UpdateStudent(students);
                    goto reset;
                    break;
                case 4:
                    Console.Write("Enter Student delete: ");
                    //int i2 = Convert.ToInt32(Console.ReadLine());
                    string i2 = Console.ReadLine();
                    DeleteStudent(i2);
                    goto reset;
                    break;
                case 5:
                    ServiceReference1.Student student = new Student();

                    Console.Write("Student Code: ");
                    //int id = Convert.ToInt32(Console.ReadLine());
                    string id = Console.ReadLine();
                    student.Student_Code = id;


                    Console.Write("Lecturer Name: ");
                    string name = Console.ReadLine();
                    student.Student_Name = name;


                    Console.Write("Student Phone: ");
                    string phone = Console.ReadLine();
                    student.Student_Phone = phone;


                    Console.Write("Student Email: ");
                    string email = Console.ReadLine();
                    student.Student_Email = email;

                    Console.Write("Lecturer details: ");
                    string info = Console.ReadLine();
                    student.Student_Details = info;

                    addStudent(student);
                    goto reset;
                    break;
                default:
                    break;
            }
            Console.ReadLine();
        }
    }
}
