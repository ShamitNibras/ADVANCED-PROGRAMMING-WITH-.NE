using Entity.Models.Database;
using System;
using System.Collections.Generic;
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
    }
}