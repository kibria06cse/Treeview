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
    public class VillageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Village
        public ActionResult Index()
        {
            var villages = db.Villages.Include(v => v.Union);
            return View(villages.ToList());
        }

        // GET: Village/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Village village = db.Villages.Find(id);
            if (village == null)
            {
                return HttpNotFound();
            }
            return View(village);
        }

        // GET: Village/Create
        public ActionResult Create()
        {
            ViewBag.UnionId = new SelectList(db.Unions, "Id", "Name");
            return View();
        }

        // POST: Village/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,UnionId")] Village village)
        {
            if (ModelState.IsValid)
            {
                db.Villages.Add(village);
                db.SaveChanges();

                int parentId = 0;
                var parent = db.Treeviews.Where(i => i.HierarchyTypeId == (int)Enums.Union && i.KeyOfThatHierarchy == village.UnionId);
                if (parent != null)
                    parentId = parent.FirstOrDefault().Id;
                var treeview = new Treeview { HierarchyTypeId = (int)Enums.Village, KeyOfThatHierarchy = village.Id, Label = village.Name, ParentId = parentId };
                db.Treeviews.Add(treeview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UnionId = new SelectList(db.Unions, "Id", "Name", village.UnionId);
            return View(village);
        }

        // GET: Village/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Village village = db.Villages.Find(id);
            if (village == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnionId = new SelectList(db.Unions, "Id", "Name", village.UnionId);
            return View(village);
        }

        // POST: Village/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UnionId")] Village village)
        {
            if (ModelState.IsValid)
            {
                db.Entry(village).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UnionId = new SelectList(db.Unions, "Id", "Name", village.UnionId);
            return View(village);
        }

        // GET: Village/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Village village = db.Villages.Find(id);
            if (village == null)
            {
                return HttpNotFound();
            }
            return View(village);
        }

        // POST: Village/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Village village = db.Villages.Find(id);
            db.Villages.Remove(village);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SchoolByArea(int id)
        {
            var schools = db.Schools.Where(i => i.VillageId == id).Include(i => i.Village).AsEnumerable();

            return View("~/Views/School/Index.cshtml", schools);
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
