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
    public class SchoolController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: School
        public ActionResult Index()
        {
            var schools = db.Schools.Include(s => s.Village);
            return View(schools.ToList());
        }

        // GET: School/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // GET: School/Create
        public ActionResult Create()
        {
            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name");
            return View();
        }

        // POST: School/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,VillageId")] School school)
        {
            if (ModelState.IsValid)
            {
                db.Schools.Add(school);
                db.SaveChanges();

                int parentId = 0;
                var parent = db.Treeviews.Where(i => i.HierarchyTypeId == (int)Enums.Village && i.KeyOfThatHierarchy == school.VillageId);
                if (parent != null)
                    parentId = parent.FirstOrDefault().Id;
                var treeview = new Treeview { HierarchyTypeId = (int)Enums.School, KeyOfThatHierarchy = school.Id, Label = school.Name, ParentId = parentId };
                db.Treeviews.Add(treeview);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", school.VillageId);
            return View(school);
        }

        // GET: School/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", school.VillageId);
            return View(school);
        }

        // POST: School/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,VillageId")] School school)
        {
            if (ModelState.IsValid)
            {
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", school.VillageId);
            return View(school);
        }

        // GET: School/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: School/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            School school = db.Schools.Find(id);
            db.Schools.Remove(school);
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

        [HttpGet]
        public ActionResult ResultByClass(int SchoolId)
        {
            ViewBag.ClassList = new SelectList(db.SchoolClasses.Where(i => i.SchoolId == SchoolId), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult ResultByClass(ResultByClassViewModel viewModel)
        {
            return RedirectToAction("ResultByClass", new { SchoolId = viewModel.ClassId });
        }

        public ActionResult GetClassResultWithStudent(ResultByClassViewModel viewModel)
        {
            var result = db.Results.Where(i => i.ClassId == viewModel.ClassId).OrderBy(i=>i.Subject.Name).ThenBy(i=>i.Student.Name).ToList();
            viewModel.ResultWithStudent = result;
            return PartialView("_ResultByClass", viewModel);
        }
        [HttpGet]
        public ActionResult GetStudentByClass(int ClassId)
        {
            var students = db.Students.Where(i => i.SchoolClassId == ClassId);
            var studentsList = new SelectList(students, "Id", "Name");
            return Json(studentsList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetSubjectsByClass(int ClassId)
        {
            var subjcts = db.Subjects.Where(i => i.SchoolClassId == ClassId);
            var subjectsList = new SelectList(subjcts, "Id", "Name");
            return Json(subjectsList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddResultBySubject(int SchoolId)
        {
            ViewBag.ClassList = new SelectList(db.SchoolClasses.Where(i => i.SchoolId == SchoolId), "Id", "Name");
            var vModel = new AddResultBySubject() { SchoolId = SchoolId };
            
            return View(vModel);
        }

        [HttpPost]
        public ActionResult AddResultBySubject(AddResultBySubject viewModel, FormCollection form)
        {
            ViewBag.ClassList = new SelectList(db.SchoolClasses.Where(i => i.SchoolId == viewModel.SchoolId), "Id", "Name");
            var students = db.Students.Where(i => i.SchoolClassId == viewModel.ClassId).ToList();
            foreach(var student in students)
            {
                var grade = form["Grade_" + student.Id].ToString();
                var markString = form["Mark_" + student.Id].ToString();
                double mark = 0.0;
                double.TryParse(markString, out mark);
                var result = new Result() { 
                    ClassId= viewModel.ClassId,Grade = grade, MarkPercentage= mark,SchoolId= viewModel.SchoolId, StudentId= student.Id,SubjectId= viewModel.SubjectId
                };
                db.Results.Add(result);
            }
            db.SaveChanges();

            return View();
        }
        [HttpGet]
        public ActionResult GetSubjectwiseResult(int subjectId,int classId)
        {
            var students = db.Students.Where(i => i.SchoolClassId == classId).ToList();
            var viewModel = new AddResultBySubject();
            viewModel.Students = students;
            viewModel.ClassId = classId;
            viewModel.SubjectId = subjectId;
            return PartialView("_AddResultBySubject",viewModel);
        }

        [HttpGet]
        public ActionResult GetStudentwiseResult(int studentId, int classId)
        {
            var subjects = db.Subjects.Where(i => i.SchoolClassId == classId).ToList();
            var viewModel = new AddResultByStudent();
            viewModel.Subjects = subjects;
            viewModel.ClassId = classId;
            viewModel.StudentId = studentId;
            return PartialView("_AddResultByStudent", viewModel);
        }

        [HttpGet]
        public ActionResult AddResultByStudent(int SchoolId)
        {
            ViewBag.ClassList = new SelectList(db.SchoolClasses.Where(i => i.SchoolId == SchoolId), "Id", "Name");
            
            return View();
        }

        [HttpPost]
        public ActionResult AddResultByStudent(AddResultByStudent viewModel,FormCollection form)
        {
            ViewBag.ClassList = new SelectList(db.SchoolClasses.Where(i => i.SchoolId == viewModel.SchoolId), "Id", "Name");

            return View();
        }
    }
}
