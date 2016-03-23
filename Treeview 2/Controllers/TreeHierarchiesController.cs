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
    public class TreeHierarchiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TreeHierarchies
        public ActionResult Index()
        {
            return View(db.TreeHierarchys.ToList());
        }

        // GET: TreeHierarchies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreeHierarchy treeHierarchy = db.TreeHierarchys.Find(id);
            if (treeHierarchy == null)
            {
                return HttpNotFound();
            }
            return View(treeHierarchy);
        }

        // GET: TreeHierarchies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TreeHierarchies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] TreeHierarchy treeHierarchy)
        {
            if (ModelState.IsValid)
            {
                db.TreeHierarchys.Add(treeHierarchy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(treeHierarchy);
        }

        // GET: TreeHierarchies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreeHierarchy treeHierarchy = db.TreeHierarchys.Find(id);
            if (treeHierarchy == null)
            {
                return HttpNotFound();
            }
            return View(treeHierarchy);
        }

        // POST: TreeHierarchies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] TreeHierarchy treeHierarchy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treeHierarchy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(treeHierarchy);
        }

        // GET: TreeHierarchies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreeHierarchy treeHierarchy = db.TreeHierarchys.Find(id);
            if (treeHierarchy == null)
            {
                return HttpNotFound();
            }
            return View(treeHierarchy);
        }

        // POST: TreeHierarchies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TreeHierarchy treeHierarchy = db.TreeHierarchys.Find(id);
            db.TreeHierarchys.Remove(treeHierarchy);
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
