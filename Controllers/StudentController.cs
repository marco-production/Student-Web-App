using Microsoft.AspNetCore.Mvc;
using Student_Web_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Student_Web_App.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<StudentViewModel> student = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:7296/api/students");
                //HTTP GET
                var responseTask = client.GetAsync("students");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readStudent = result.Content.ReadAsAsync<IList<StudentViewModel>>();
                    readStudent.Wait();
                    student = readStudent.Result;
                }
                else 
                {
                    student = Enumerable.Empty<StudentViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(student);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentViewModel student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:7296/api/");

                string jsonString = JsonSerializer.Serialize(student);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var responseTask = client.PostAsync("students/", httpContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                return View(student);
            }
        }

        public IActionResult Details(int id)
        {
            StudentViewModel student = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:7296/api/");

                var responseTask = client.GetAsync("students/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readStudent = result.Content.ReadAsAsync<StudentViewModel>();
                    readStudent.Wait();
                    student = readStudent.Result;
                }
            }
            return View(student);
        }

        public IActionResult Edit(int id)
        {
            StudentViewModel student = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:7296/api/");

                var responseTask = client.GetAsync("students/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readStudent = result.Content.ReadAsAsync<StudentViewModel>();
                    readStudent.Wait();
                    student = readStudent.Result;
                }
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(StudentViewModel student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:7296/api/");

                string jsonString = JsonSerializer.Serialize(student);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var responseTask = client.PutAsync("students/" +  student.Id.ToString(), httpContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                return View(student);
            }
        }

        public IActionResult Delete(int id)
        {
            StudentViewModel student = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:7296/api/");

                var responseTask = client.GetAsync("students/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readStudent = result.Content.ReadAsAsync<StudentViewModel>();
                    readStudent.Wait();
                    student = readStudent.Result;
                }
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            StudentViewModel student = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:7296/api/");

                var responseTask = client.DeleteAsync("students/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readStudent = result.Content.ReadAsAsync<StudentViewModel>();
                    readStudent.Wait();
                    student = readStudent.Result;
                }
            }
            return RedirectToAction("Index");
        }
    }
}
