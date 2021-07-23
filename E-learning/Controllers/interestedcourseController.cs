using E_learning.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning.Controllers
{
    public class interestedcourseController : BaseController
    {
        private SchoolEntities db = new SchoolEntities();

        // GET: interestedcourse
        public ActionResult Index()
        {

            string CurrentUserid = User.Identity.GetUserId();
            var TrainerinDeptid = db.Trainners.Where(t => t.AspNerUserId == CurrentUserid).
                                Select(dept => dept.Departmentid).FirstOrDefault();
            var Couresindept = db.Courses.Where(c => c.Departmentid == TrainerinDeptid).ToList();
            return View(Couresindept);
        }
        public bool IsChosen(int Courseid)
        {
            string CurrentUserid = User.Identity.GetUserId();

            var query = db.TrainerIintrestedCourses.Where(t => t.Trainner.AspNerUserId == CurrentUserid && t.Courseid == Courseid);
            if (query.Count() > 0)
                return true;
            return false;
        }
        [HttpPost]
        public ActionResult AddRemoveinterestJson(int courseid, bool status)
        {
            string CurrentUserid = User.Identity.GetUserId();

            if (!status)
            {
                var query = db.TrainerIintrestedCourses.Where(t => t.Trainner.AspNerUserId == CurrentUserid && t.Courseid == courseid).FirstOrDefault();
                if(query!=null)
                {
                    db.TrainerIintrestedCourses.Remove(db.TrainerIintrestedCourses.Find(query.TrainerIntCoureseid));
                    db.SaveChanges();
                }
            }
            else
             {
                var querry = db.Trainners.
                            Where(t => t.AspNerUserId == CurrentUserid).SingleOrDefault();
                TrainerIintrestedCourse tc = new TrainerIintrestedCourse();
                tc.Courseid = courseid;
                tc.Trainnerid = querry.Trainnerid;
                db.TrainerIintrestedCourses.Add(tc);
                db.SaveChanges();

            }
            return Json(true, JsonRequestBehavior.AllowGet);
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