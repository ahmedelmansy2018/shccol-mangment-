using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_learning.Models;

namespace E_learning.Controllers
{
    public class CourseLevelsController : BaseController
    {
        private SchoolEntities db = new SchoolEntities();

        // GET: CourseLevels
        public ActionResult Index()
        {
            return View(db.CourseLevels.ToList());
        }

        // GET: CourseLevels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLevel courseLevel = db.CourseLevels.Find(id);
            if (courseLevel == null)
            {
                return HttpNotFound();
            }
            return View(courseLevel);
        }

        // GET: CourseLevels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseLevelid,LevelName")] CourseLevel courseLevel)
        {
            if (ModelState.IsValid)
            {
                db.CourseLevels.Add(courseLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseLevel);
        }

        // GET: CourseLevels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLevel courseLevel = db.CourseLevels.Find(id);
            if (courseLevel == null)
            {
                return HttpNotFound();
            }
            return View(courseLevel);
        }

        // POST: CourseLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseLevelid,LevelName")] CourseLevel courseLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseLevel);
        }

        // GET: CourseLevels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLevel courseLevel = db.CourseLevels.Find(id);
            if (courseLevel == null)
            {
                return HttpNotFound();
            }
            return View(courseLevel);
        }

        // POST: CourseLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseLevel courseLevel = db.CourseLevels.Find(id);
            db.CourseLevels.Remove(courseLevel);
            db.SaveChanges();
            return RedirectToAction("Index");
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
