using Entity.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Entity.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            EntityEntities db = new EntityEntities();
            var data = db.Users.ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new User());
        }
        [HttpPost]

        public ActionResult Create(User u)
        {
            if (ModelState.IsValid)
            {
                EntityEntities db = new EntityEntities();
                db.Users.Add(u);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            EntityEntities db = new EntityEntities();
            var user = (from u in db.Users
                         where u.Serial == id
                         select u).FirstOrDefault();
            return View(user);
        }
        [HttpPost]

        public ActionResult Edit(User sub_u)
        {

            EntityEntities db = new EntityEntities();
            var user = (from u in db.Users
                         where u.Serial == sub_u.Serial
                         select u).FirstOrDefault();
            db.Entry(user).CurrentValues.SetValues(sub_u);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            EntityEntities db = new EntityEntities();
            var Users = (from u in db.Users
                         where u.Serial == id
                         select u).FirstOrDefault();
            return View(Users);
        }
        [HttpGet]
        public ActionResult Sure(int id)
        {
            EntityEntities db = new EntityEntities();
            var Users = (from u in db.Users
                         where u.Serial == id
                         select u).FirstOrDefault();

            db.Users.Remove(Users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}