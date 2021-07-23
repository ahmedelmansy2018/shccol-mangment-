using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_learning.Models;
using PagedList;


namespace E_learning.Controllers
{
    public  class CourseController : BaseController
    {
        private SchoolEntities db = new SchoolEntities();

        // GET: Course
        public ActionResult Index(string Search, int? page)
        {
            var course = db.Courses.Include(c=> c.CourseLevel);
            if(!string.IsNullOrEmpty(Search))
            {
                 course = course.Where(s => s.Tittel.Contains(Search));
             }
            return View(course.ToList().ToPagedList(page ?? 1,5));
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            ViewBag.CourseLevelid = new SelectList(db.CourseLevels, "CourseLevelid", "LevelName");
            ViewBag.Departmentid = new SelectList(db.Departments, "Departmentid", "DeptName");

            return View();
        }
        public ActionResult PartialCreateNew()
        {
            ViewBag.CourseLevelid = new SelectList(db.CourseLevels, "CourseLevelid", "LevelName");
            ViewBag.Departmentid = new SelectList(db.Departments, "Departmentid", "DeptName");

            return PartialView("_PartialCreateNew");
        }
        

        // POST: Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseLevelid = new SelectList(db.CourseLevels, "CourseLevelid", "LevelName",course.CourseLevelid);
            ViewBag.Departmentid = new SelectList(db.Departments, "Departmentid", "DeptName",course.Departmentid);
            return View(course);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseLevelid = new SelectList(db.CourseLevels, "CourseLevelid", "LevelName", course.CourseLevelid);
            ViewBag.Departmentid = new SelectList(db.Departments, "Departmentid", "DeptName", course.Departmentid);
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Course course)
        {
            ViewBag.CourseLevelid = new SelectList(db.CourseLevels, "CourseLevelid", "LevelName", course.CourseLevelid);
            ViewBag.Departmentid = new SelectList(db.Departments, "Departmentid", "DeptName", course.Departmentid);
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }  
            return View(course);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteConfirmedJson(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return Json(true,JsonRequestBehavior.AllowGet);
        }
        public ActionResult StudinCourse(string CourseTitle)
        {
            var std = db.Enrollments
                      .Where(c => c.Course.Tittel == CourseTitle);
            return View(std.ToList());
        }
       
        public JsonResult GetValSearch(string search)
        {

            var SuggestedCourse = db.Courses
                                .Where(c => c.Tittel.Contains(search))
                                .Select(x => new { Title = x.Tittel }).ToList();

            return  new JsonResult { Data = SuggestedCourse, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult UpdateGradeJson(int courseid, int studentid, decimal Grade)
        {

            var enrollment = db.Enrollments.
                            Where(e => e.Courseid == courseid && e.studentid == studentid).FirstOrDefault();
            if(enrollment!=null)
            {
                enrollment.Grade = Grade;
                try { db.SaveChanges(); }
                catch (Exception)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { success = false, responseText = "Grade Dot UPdated !", JsonRequestBehavior.AllowGet });

                }
            }
            return Json(new{ success=true,responseText="Grade UPdated !",JsonRequestBehavior.AllowGet }); 
        }

        public ActionResult DrawChart()
        {
            
            return View(db.Courses.ToList());
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
