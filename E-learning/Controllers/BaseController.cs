using E_learning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning.Controllers
{
    public class BaseController : Controller
    {
        private SchoolEntities db = new SchoolEntities();

        static List<CourseStats> Stats = null;
        static List<StudentStats> StudentStats = null;

        // GET: Base
        public BaseController()
        {
            Stats = new List<CourseStats>();
            var CouresGroup = db.Enrollments.GroupBy(c => c.Course.Tittel)
                                 .Select(g=>new CourseStats {
                                     CourseTitle = g.Key,
                                     CourseEnrollments = g.Count()
                                 }).ToList();
            //foreach (var item in CouresGroup)
            //{
            //    Stats.Add(new CourseStats { CourseTitle = item.Key, CourseEnrollments = item.Count() });
            //}
            ViewBag.CourseStats = CouresGroup;



            StudentStats = new List<StudentStats>();

            var TopStudent = db.Enrollments
                                .GroupBy(c => c.studentid).
                                Select(g => new StudentStats
                                {
                                    Studentid = g.Key,
                                    Name = g.FirstOrDefault().Student.FName,
                                    NumberOfCourses = g.Count(),
                                    AverageGrade = (decimal)g.Average(s => s.Grade),
                                    Image = g.FirstOrDefault().Student.ImagePath

                                }).OrderByDescending(x => x.AverageGrade).Take(2);
            ViewBag.TopStudent = TopStudent;
        }
    }
}