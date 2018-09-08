using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WCF_MVC.ServiceReference1;

namespace WCF_MVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        Service1Client db = new Service1Client();
        public ActionResult Index()
        {
            return View(db.GetStudents());
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student stu = db.GetStudent(id);
            if (stu == null)
            {
                return HttpNotFound();
            }
            return View(stu);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Student_Code,Student_Name,Student_Email,Student_Phone,Student_Details")] Student student)
        {
            if (ModelState.IsValid)
            {
                bool b = db.AddNewStudent(student);
                if (b)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(student);
                }
            }
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Student_Code,Student_Name,Student_Email,Student_Phone,Student_Details")] Student student)
        {
            if (ModelState.IsValid)
            {
                bool b = db.UpdateStudent(student);
                if (b)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(student);
                }
            }
            return View(student);
        }
        public ActionResult Delete(string id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.get
        }
    }
}