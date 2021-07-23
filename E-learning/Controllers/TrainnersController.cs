using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using E_learning.Models;

namespace E_learning.Controllers
{
    public class TrainnersController : BaseController
    {
        private SchoolEntities db = new SchoolEntities();

        // GET: Trainners
        public ActionResult Index()
        {
            var trainners = db.Trainners.Include(t => t.AspNetUser).Include(t => t.Department);
            return View(trainners.ToList());
        }

        // GET: Trainners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainner trainner = db.Trainners.Find(id);
            if (trainner == null)
            {
                return HttpNotFound();
            }
            return View(trainner);
        }

        // GET: Trainners/Create
        public ActionResult Create()
        {
            ViewBag.AspNerUserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Departmentid = new SelectList(db.Departments, "Departmentid", "DeptName");
            return View();
        }

        // POST: Trainners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Trainnerid,FName,LName,Departmentid,AspNerUserId")] Trainner trainner)
        {
            if (ModelState.IsValid)
            {
                db.Trainners.Add(trainner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNerUserId = new SelectList(db.AspNetUsers, "Id", "Email", trainner.AspNerUserId);
            ViewBag.Departmentid = new SelectList(db.Departments, "Departmentid", "DeptName", trainner.Departmentid);
            return View(trainner);
        }

        // GET: Trainners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainner trainner = db.Trainners.Find(id);
            if (trainner == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNerUserId = new SelectList(db.AspNetUsers, "Id", "Email", trainner.AspNerUserId);
            ViewBag.Departmentid = new SelectList(db.Departments, "Departmentid", "DeptName", trainner.Departmentid);
            return View(trainner);
        }

        // POST: Trainners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Trainnerid,FName,LName,Departmentid,AspNerUserId")] Trainner trainner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNerUserId = new SelectList(db.AspNetUsers, "Id", "Email", trainner.AspNerUserId);
            ViewBag.Departmentid = new SelectList(db.Departments, "Departmentid", "DeptName", trainner.Departmentid);
            return View(trainner);
        }

        // GET: Trainners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainner trainner = db.Trainners.Find(id);
            if (trainner == null)
            {
                return HttpNotFound();
            }
            return View(trainner);
        }

        // POST: Trainners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainner trainner = db.Trainners.Find(id);
            db.Trainners.Remove(trainner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public string GetCourseIntrests(int Trainnerid)
        {
            var Query = db.TrainerIintrestedCourses.
                        Where(t => t.Trainnerid == Trainnerid).ToList();
              StringBuilder interests = new StringBuilder();
            //  string interests = "";
            
            foreach (var item in Query)
            {

                 interests.Append(item.Course.Tittel+",");
                //interests += item.Course.Tittel + ",";
            }

            return interests.ToString();
           // return interests;

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
