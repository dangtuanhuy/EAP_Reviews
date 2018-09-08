using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServerApplication
{
    public class Service1 : IService1
    {
        EAPEntities db = new EAPEntities();
        public List<Student> GetStudents()
        {
            return db.Students.ToList();
        }
        public Student GetStudent(string id)
        {
            return db.Students.Find(id);
        }

        public List<Student> FindStudent(string name)
        {
            var students = from s in db.Students select s;
            students = students.Where(s => s.Student_Name.Contains(name));
            students = students.OrderBy(s => s.Student_Code);
            return students.ToList();
        }
        public bool AddNewStudent(Student student)
        {
            try
            {
                db.Students.Add(student);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateStudent(Student student)
        {
            try
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteStudent(string id)
        {
            try
            {
                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
