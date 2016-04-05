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
    public class BaseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Base
        public ActionResult Index()
        {
            return View(db.Bases.ToList());
        }

        // GET: Base/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Base @base = db.Bases.Find(id);
            if (@base == null)
            {
                return HttpNotFound();
            }
            return View(@base);
        }

        // GET: Base/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Base/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Base @base)
        {
            if (ModelState.IsValid)
            
            {
                db.Bases.Add(@base);

                db.SaveChanges();

                var treeview = new Treeview { Label = @base.Name,HierarchyTypeId= (int)Enums.Base ,KeyOfThatHierarchy= @base.Id};
                treeview= db.Treeviews.Add(treeview);
                db.SaveChanges();

                
                return RedirectToAction("Index");
            }

            return View(@base);
        }

        // GET: Base/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Base @base = db.Bases.Find(id);
            if (@base == null)
            {
                return HttpNotFound();
            }
            return View(@base);
        }

        // POST: Base/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Base @base)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@base).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@base);
        }

        // GET: Base/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Base @base = db.Bases.Find(id);
            if (@base == null)
            {
                return HttpNotFound();
            }
            return View(@base);
        }

        // POST: Base/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Base @base = db.Bases.Find(id);
            db.Bases.Remove(@base);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DivisionByBase(int id)
        {
            var divisions = db.Divisions.Where(i => i.BaseId == id).Include(i=>i.Base).AsEnumerable();
            
            return View("~/Views/Division/Index.cshtml",divisions);
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
