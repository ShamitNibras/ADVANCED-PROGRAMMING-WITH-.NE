using LabTask_3.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabTask_3.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            StudentEntities db = new StudentEntities();
            var data = db.Students.ToList();
            return View(data);
        }
        [HttpPost]

        public ActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                StudentEntities db = new StudentEntities();
                db.Students.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}