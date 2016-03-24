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
    public class UnionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Union
        public ActionResult Index()
        {
            var unions = db.Unions.Include(u => u.Thana);
            return View(unions.ToList());
        }

        // GET: Union/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Union union = db.Unions.Find(id);
            if (union == null)
            {
                return HttpNotFound();
            }
            return View(union);
        }

        // GET: Union/Create
        public ActionResult Create()
        {
            ViewBag.ThanaId = new SelectList(db.Thanas, "Id", "Name");
            return View();
        }

        // POST: Union/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ThanaId")] Union union)
        {
            if (ModelState.IsValid)
            {
                db.Unions.Add(union);
                db.SaveChanges();

                int parentId = 0;
                var parent = db.Treeviews.Where(i => i.HierarchyTypeId == (int)Enums.Thana && i.KeyOfThatHierarchy == union.ThanaId);
                if (parent != null)
                    parentId = parent.FirstOrDefault().Id;
                var treeview = new Treeview { HierarchyTypeId = (int)Enums.Union, KeyOfThatHierarchy = union.Id, Label = union.Name, ParentId = parentId };
                db.Treeviews.Add(treeview);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ThanaId = new SelectList(db.Thanas, "Id", "Name", union.ThanaId);
            return View(union);
        }

        // GET: Union/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Union union = db.Unions.Find(id);
            if (union == null)
            {
                return HttpNotFound();
            }
            ViewBag.ThanaId = new SelectList(db.Thanas, "Id", "Name", union.ThanaId);
            return View(union);
        }

        // POST: Union/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ThanaId")] Union union)
        {
            if (ModelState.IsValid)
            {
                db.Entry(union).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ThanaId = new SelectList(db.Thanas, "Id", "Name", union.ThanaId);
            return View(union);
        }

        // GET: Union/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Union union = db.Unions.Find(id);
            if (union == null)
            {
                return HttpNotFound();
            }
            return View(union);
        }

        // POST: Union/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Union union = db.Unions.Find(id);
            db.Unions.Remove(union);
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
