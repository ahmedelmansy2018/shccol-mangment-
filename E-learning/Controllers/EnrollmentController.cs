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
    public class EnrollmentController : BaseController
    {
        private SchoolEntities db = new SchoolEntities();

        // GET: Enrollment
        public  ActionResult Index()
        {
            var enrollments = db.Enrollments.Include(e => e.Course).Include(e => e.Student);
            return View(enrollments.ToList());
        }

        // GET: Enrollment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollment/Create
        public ActionResult Create()
        {
            ViewBag.Courseid = new SelectList(db.Courses, "Courseid", "Tittel");
            ViewBag.studentid = new SelectList(db.Students, "Studentid", "FullName");
            return View();
        }

        // POST: Enrollment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
               var old_enrollments = db.Enrollments.Include(e => e.Course).Include(e => e.Student).ToList();
                if(old_enrollments!=null)
                {
                    foreach (var item in old_enrollments)
                    {
                        if (enrollment.Courseid == item.Courseid && enrollment.studentid == item.studentid)
                        {
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            db.Enrollments.Add(enrollment);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }

                }
               

            }

            ViewBag.Courseid = new SelectList(db.Courses, "Courseid", "Tittel", enrollment.Courseid);
            ViewBag.studentid = new SelectList(db.Students, "Studentid", "FullName", enrollment.studentid);
            return View(enrollment);
        }

        // GET: Enrollment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Courseid = new SelectList(db.Courses, "Courseid", "Tittel", enrollment.Courseid);
            ViewBag.studentid = new SelectList(db.Students, "Studentid", "FullName", enrollment.studentid);
            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Enrollmentid,Courseid,studentid,Grade")] Enrollment enrollment)
        {

            if (ModelState.IsValid)
            {
                var old_enrollments = db.Enrollments.Include(e => e.Course).Include(e => e.Student).ToList();

                foreach (var item in old_enrollments)
                {
                    if (enrollment.Courseid == item.Courseid && enrollment.studentid == item.studentid)
                    {
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        var record = db.Enrollments.Find(enrollment.Enrollmentid);
                      //  record.Enrollmentid = enrollment.Enrollmentid;
                        record.Courseid = enrollment.Courseid;
                        record.studentid = enrollment.studentid;
                        record.Grade = enrollment.Grade;
                        //if (enrollment.Courseid == record.Courseid && enrollment.studentid == record.studentid)
                        //{
                        //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                        //}
                        //else
                        //{
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        //}
                        // db.Entry(enrollment).State = EntityState.Modified;
                      
                    }
                }

            
            }
            ViewBag.Courseid = new SelectList(db.Courses, "Courseid", "Tittel", enrollment.Courseid);
            ViewBag.studentid = new SelectList(db.Students, "Studentid", "FullName", enrollment.studentid);
            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
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
