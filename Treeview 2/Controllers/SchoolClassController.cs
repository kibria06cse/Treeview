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
    public class SchoolClassController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SchoolClass
        public ActionResult Index()
        {
            var schoolClasses = db.SchoolClasses.Include(s => s.School);
            return View(schoolClasses.ToList());
        }

        // GET: SchoolClass/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolClass schoolClass = db.SchoolClasses.Find(id);
            if (schoolClass == null)
            {
                return HttpNotFound();
            }
            return View(schoolClass);
        }

        // GET: SchoolClass/Create
        public ActionResult Create()
        {
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name");
            return View();
        }

        // POST: SchoolClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,SchoolId")] SchoolClass schoolClass)
        {
            if (ModelState.IsValid)
            {
                db.SchoolClasses.Add(schoolClass);
                db.SaveChanges();

                //int parentId = 0;
                //var parent = db.Treeviews.Where(i => i.HierarchyTypeId == (int)Enums.School && i.KeyOfThatHierarchy == schoolClass.SchoolId);
                //if (parent != null)
                //    parentId = parent.FirstOrDefault().Id;
                //var treeview = new Treeview { HierarchyTypeId = (int)Enums.SchoolClass, KeyOfThatHierarchy = schoolClass.Id, Label = schoolClass.Name, ParentId = parentId };
                //db.Treeviews.Add(treeview);

                //db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", schoolClass.SchoolId);
            return View(schoolClass);
        }

        // GET: SchoolClass/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolClass schoolClass = db.SchoolClasses.Find(id);
            if (schoolClass == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", schoolClass.SchoolId);
            return View(schoolClass);
        }

        // POST: SchoolClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,SchoolId")] SchoolClass schoolClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", schoolClass.SchoolId);
            return View(schoolClass);
        }

        // GET: SchoolClass/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolClass schoolClass = db.SchoolClasses.Find(id);
            if (schoolClass == null)
            {
                return HttpNotFound();
            }
            return View(schoolClass);
        }

        // POST: SchoolClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolClass schoolClass = db.SchoolClasses.Find(id);
            db.SchoolClasses.Remove(schoolClass);
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
