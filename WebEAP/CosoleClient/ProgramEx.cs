using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CosoleClient
{
    class ProgramEx
    {
        // Lấy thông tin tất cả sinh viên
        static async Task GetAll()
        {
            string url = "http://localhost:50180/api/Students";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;

                JArray parsed = JArray.Parse(responseData.ToString());

                Console.WriteLine("------------------------List of Student ' Information-------------");
                foreach (var pair in parsed)
                {
                    JObject obj = JObject.Parse(pair.ToString());
                    foreach (var s in obj)
                    {
                        Console.WriteLine("{0} : {1}", s.Key, s.Value.ToString().Trim());
                    }
                    Console.WriteLine();
                }
            }
        }
        // Tìm Kiếm sinh viên theo ID nè
        static async Task GetStudent(string id)
        {
            string url = "http://localhost:50180/api/Students";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(url + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                Student student = await response.Content.ReadAsAsync<Student>();
                Console.WriteLine("--------------------Lecturer information--------------------");
                Console.WriteLine("Student Code: " + id);
                Console.WriteLine("Student Name: " + student.Student_Name);
                Console.WriteLine("Student Email: " + student.Student_Email);
                Console.WriteLine("Student Phone: " + student.Student_Phone);
                Console.WriteLine("Student Details: " + student.Student_Details.ToString().Trim());
            }
            else
            {
                Console.WriteLine("Lecturer is not exist");
            }
        }
        static async Task AddNewStudent(string id, string name, string email, string phone, string details)
        {
            string url = "http://localhost:50180/api/Students";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(url);
            Student st = new Student()
            {
                Student_Code = id,
                Student_Name = name,
                Student_Email = email,
                Student_Phone = phone,
                Student_Details = details
            };
            response = await client.PostAsJsonAsync(url, st);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--------------------Student's information is added--------------------");
            }
            else
            {
                Console.WriteLine("--------------------Student's information cannot addto database--------------------");
            }
        }
        static async Task UpdateStudent(string id, string name, string email, string phone, string details)
        {
            string url = "http://localhost:50180/api/Students";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(url + "/" + id);
            if (response.IsSuccessStatusCode)
            {
               Student student = await response.Content.ReadAsAsync<Student>();
                if (name != "")
                    student.Student_Name = name;
                if (email != "")
                    student.Student_Email = email;
                if (phone != "")
                    student.Student_Phone = phone;
                if (details != "")
                    student.Student_Details = details;
                response = await client.PutAsJsonAsync(url + "/" + id, student);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("--------------------Update Student Successfully--------------------");
                }
                else
                {
                    Console.WriteLine("--------------------Update Failure--------------------");
                }
            }
        }
        static async Task DeleteStudent(string id)
        {
            string url = "http://localhost:50180/api/Students";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(url + "/" + id);
            response = await client.DeleteAsync(url + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--------------------Delete Successfully--------------------");
            }
            else
            {
                Console.WriteLine("--------------------Delete Failure--------------------");
            }
        }
        static void Main(string[] args)
        {
            string i;
            do
            {
                Console.WriteLine("1. Get all Student' information");
                Console.WriteLine("2. Get only one student's information");
                Console.WriteLine("3. Add new student");
                Console.WriteLine("4. Update student's information");
                Console.WriteLine("5. Delete student from database");
                Console.Write("Enter your choice: ");
                int ans = Convert.ToInt32(Console.ReadLine());
                if (ans == 1)
                {
                    GetAll().Wait();
                }
                else
                {
                    if (ans == 2)
                    {
                        Console.Write("Enter the student code: ");
                        string id = Console.ReadLine();
                        GetStudent(id).Wait();
                    }
                    else
                    {
                        if (ans == 3)
                        {
                            Console.WriteLine("--------------------Enter the Student'sInformation--------------------");
                            Console.Write("Enter Student Code:");
                            string id = Console.ReadLine();
                            Console.Write("Enter Student Name:");
                            string name = Console.ReadLine();
                            Console.Write("Enter Student Email:");
                            string email = Console.ReadLine();
                            Console.Write("Enter Student Phone:");
                            string phone = Console.ReadLine();
                            Console.Write("Enter Student Details:");
                            string details = Console.ReadLine();
                            AddNewStudent(id, name, email, phone, details).Wait();
                        }
                        else
                        {
                            if (ans == 4)
                            {
                                Console.Write("Enter the student Code: ");
                                string id = Console.ReadLine();
                                Console.WriteLine("--------------------Enter the Student's Information--------------------");
                                Console.Write("Enter student Name:");
                                string name = "";
                                name = Console.ReadLine();
                                Console.Write("Enter studentEmail:");
                                string email = "";
                                email = Console.ReadLine();
                                Console.Write("Enter student Phone:");
                                string phone = "";
                                phone = Console.ReadLine();
                                Console.Write("Enter student details :");
                                string details = "";
                                details = Console.ReadLine();
                                UpdateStudent(id, name, email, phone, details).Wait();
                            }
                            else
                            {
                                Console.Write("Enter the student code: ");
                                string id = Console.ReadLine();
                                DeleteStudent(id).Wait();
                            }
                        }
                    }
                }
                Console.Write("Do you want to continue: ");
                i = Console.ReadLine();
            } while (i == "y" || i == "Y");
            Console.ReadLine();
        }
    }
}
