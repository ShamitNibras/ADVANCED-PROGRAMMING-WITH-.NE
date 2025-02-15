﻿using Entity.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Entity.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            EntityEntities db = new EntityEntities();
            var data = db.Books.ToList();
            return View(data);
        }

        public ActionResult HighPrice()
        {
            EntityEntities db = new EntityEntities();
            var data = (from b in db.Books
                        where b.Price > 200
                        select b).ToList();
            return View(data);
        }
       

            [HttpGet]
        public ActionResult Create()
        {
            return View(new Book());
        }
        [HttpPost]
        public ActionResult Create(Book b)
        {
             try
            {
            if (ModelState.IsValid)
                {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges
                EntityEntities db = new EntityEntities();
                db.Books.Add(b);
                db.SaveChanges();
                    return RedirectToAction("Index");
               }
                return View();
        }  
              catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
{
    Exception raise = dbEx;
    foreach (var validationErrors in dbEx.EntityValidationErrors)
    {
        foreach (var validationError in validationErrors.ValidationErrors)
        {
            string message = string.Format("{0}:{1}",
            validationErrors.Entry.Entity.ToString(),
            validationError.ErrorMessage);
            // raise a new exception nesting
            // the current instance as InnerException
            raise = new InvalidOperationException(message, raise);
        }
    }
    throw raise;
}


}
        [HttpGet]
        public ActionResult Edit (int id)
        {
            EntityEntities db = new EntityEntities();
            var Books = (from b in db.Books
                        where b.Id == id
                        select b).FirstOrDefault();
            return View(Books);
        }
        [HttpPost]
        public ActionResult Edit(Book Sub_b)
        {
            EntityEntities db = new EntityEntities();
            var Books = (from b in db.Books
                        where b.Id == Sub_b.Id
                        select b).FirstOrDefault();
           
            db.Entry(Books).CurrentValues.SetValues(Sub_b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            EntityEntities db = new EntityEntities();
            var Book = (from b in db.Books
                         where b.Id == id
                         select b).FirstOrDefault();
            return View(Book);
        }
        [HttpGet]
        public ActionResult Sure(int id)
        {
            EntityEntities db = new EntityEntities();
            var Book = (from b in db.Books
                         where b.Id == id
                         select b).FirstOrDefault();

            db.Books.Remove(Book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
