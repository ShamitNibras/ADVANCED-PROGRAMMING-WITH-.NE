
using LabTask_3.Models.Database;
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
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Student());
        }
        public ActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                DatabaseEntities1 ss = new DatabaseEntities1();
                ss.Students.Add(s);
                ss.SaveChanges();
                return RedirectToAction("AllstudentList");
            }
            return View();
        }

        public ActionResult AllstudentList()
        {
            DatabaseEntities1 ss = new DatabaseEntities1();
            var data = ss.Students.ToList();
            return View(data);
        }

        public ActionResult Discount()
        {

            DatabaseEntities1 ss = new DatabaseEntities1();
            var datas = ss.Students.ToList();
            String year = DateTime.Now.Year.ToString();
            var data = (from s in datas
                        where (int.Parse(year)
                        - int.Parse(s.dob.Substring(s.dob.Length - 4))) > 40 &&
        float.Parse(s.cgpa) > 3.50
                        select s);

            ViewBag.name = data;
            return View();
        }
        public ActionResult Scholarship()
        {

            DatabaseEntities1 ss = new DatabaseEntities1();
            var datas = ss.Students.ToList();
            var data = (from s in datas where float.Parse(s.cgpa) > 3.75 select s);
            ViewBag.name = data;
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            DatabaseEntities1 ss = new DatabaseEntities1();
            var students = (from s in ss.Students
                            where s.id == id
                            select s).FirstOrDefault();
            return View(students);
        }
        [HttpPost]
        public ActionResult Edit(Student edit)
        {
            DatabaseEntities1 ss = new DatabaseEntities1();
            var students = (from s in ss.Students
                            where s.id == edit.id
                            select s).FirstOrDefault();

            ss.Entry(students).CurrentValues.SetValues(edit);
            ss.SaveChanges();
            return RedirectToAction("AllstudentList");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            DatabaseEntities1 ss = new DatabaseEntities1();
            var students = (from s in ss.Students
                            where s.id == id
                            select s).FirstOrDefault();
            return View(students);
        }
        [HttpGet]
        public ActionResult Sure(int id)
        {
            DatabaseEntities1 ss = new DatabaseEntities1();
            var students = (from s in ss.Students
                            where s.id == id
                            select s).FirstOrDefault();

            ss.Students.Remove(students);
            ss.SaveChanges();
            return RedirectToAction("AllstudentList");
        }
    }
}