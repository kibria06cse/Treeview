using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Treeview_2.Models;

namespace Treeview_2.Controllers
{
    public class ResultController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Result
        public ActionResult Index()
        {
            var results = db.Results.Include(r => r.Student).Include(r => r.Subject).Include(r=>r.School);
            return View(results.ToList());
        }

        // GET: Result/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Result/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(new List<Student>(), "Id", "Name");
            ViewBag.SubjectId = new SelectList(new List<Subject>(), "Id", "Name");
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name");
            ViewBag.ClassId = new SelectList(new List<SchoolClass>(), "Id", "Name");
            return View();
        }

        // POST: Result/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Result result)
        {
            if (ModelState.IsValid)
            {
                db.Results.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(new List<Student>(), "Id", "Name", result.StudentId);
            ViewBag.ClassId = new SelectList(new List<SchoolClass>(), "Id", "Name", result.StudentId);
            ViewBag.SubjectId = new SelectList(new List<Subject>(), "Id", "Name", result.SubjectId);
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name");
            return View(result);
        }

        // GET: Result/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", result.StudentId);
            ViewBag.ClassId = new SelectList(db.SchoolClasses, "Id", "Name", result.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", result.SubjectId);
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name");
            return View(result);
        }

        // POST: Result/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Grade,MarkPercentage,StudentId,SubjectId")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", result.StudentId);
            ViewBag.ClassId = new SelectList(db.SchoolClasses, "Id", "Name", result.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", result.SubjectId);
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name");
            return View(result);
        }

        // GET: Result/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Result result = db.Results.Find(id);
            db.Results.Remove(result);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetStudents(int SchoolId, int ClassId)
        {
            var students = db.Students.Where(i => i.SchoolClass.SchoolId == SchoolId && i.SchoolClassId== ClassId);
            var studentsList = new SelectList(students, "Id", "Name");
            return Json(studentsList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubjects(int SchoolId, int ClassId)
        {
            var subjects = db.Subjects.Where(i => i.SchoolClass.SchoolId == SchoolId && i.SchoolClassId == ClassId);
            var subjectsList = new SelectList(subjects, "Id", "Name");
            return Json(subjectsList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetClasses(int SchoolId)
        {
            var classes = db.SchoolClasses.Where(i => i.SchoolId == SchoolId);
            var classList = new SelectList(classes, "Id", "Name");
            return Json(classList, JsonRequestBehavior.AllowGet);
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
