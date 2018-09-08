using MVCClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class StudentsController : Controller
    {
        HttpClient client;
        string url = "http://localhost:50180/api/Students";
        public StudentsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // Get Student
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if(responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var students = JsonConvert.DeserializeObject<List<Student>>(responseData);
                return View(students);
            }
            return View("Error");
        }
        //Form Create
        public ActionResult Create()
        {
            return View(new Student());
        }
        [HttpPost]
        public async Task<ActionResult> Create(Student student)
        {
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url,student);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        //Form Edit
        public async Task<ActionResult> Edit(string id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var student = JsonConvert.DeserializeObject<Student>(responseData);
                return View(student);
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, Student student)
        {
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + id, student);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        //Form Delete
        public async Task<ActionResult> Delete(string id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var student = JsonConvert.DeserializeObject<Student>(responseData);
                return View(student);
            }
            return View("Error");
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string id, Student student)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" +id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
    }
}