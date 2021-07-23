using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using E_learning.Models;
using PagedList;

namespace E_learning.Controllers
{
   
    public class StudentController : BaseController
    {
        private SchoolEntities db = new SchoolEntities();

        // GET: Student
        public  ActionResult Index(int? page)
        {   
           TempData["ExcelDownload"]="Excel File DownLoaded!";
            return View(db.Students.Include(el=>el.Enrollments).ToList().ToPagedList(page?? 1,2));
        }

        public ActionResult ExporttoEdcel()
        {
            var gv = new GridView();
            gv.DataSource = db.Students.ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Shcool.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objstringwriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(objstringwriter);
            gv.RenderControl(htmlTextWriter);

            Response.Output.Write(objstringwriter.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Index");
        }
        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FName,LName,EnrollmentData,ImagePath")] Student student,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/Images/std/") + file.FileName);
                    student.ImagePath = file.FileName;
                }
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Student student,HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                if (File != null)
                {
                    File.SaveAs(HttpContext.Server.MapPath("~/Content/Images/std/") + File.FileName);
                    student.ImagePath = File.FileName;
                }

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
