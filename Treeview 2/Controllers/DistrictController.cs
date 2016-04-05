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
    public class DistrictController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: District
        public ActionResult Index()
        {
            var districts = db.Districts.Include(d => d.Division);
            return View(districts.ToList());
        }

        // GET: District/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return HttpNotFound();
            }
            return View(district);
        }

        // GET: District/Create
        public ActionResult Create(int? id)
        {
            ViewBag.DivisionId = new SelectList(db.Divisions, "Id", "Name");
            var model = new District();
            model.DivisionId = id.HasValue?id.Value:0;
            return View(model);
        }

        // POST: District/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DivisionId")] District district)
        {
            if (ModelState.IsValid)
            {
                db.Districts.Add(district);
                db.SaveChanges();

                int parentId = 0;
                var parent = db.Treeviews.Where(i => i.HierarchyTypeId == (int)Enums.Division && i.KeyOfThatHierarchy == district.DivisionId).FirstOrDefault();
                if (parent != null)
                    parentId = parent.Id;
                var treeview = new Treeview { HierarchyTypeId = (int)Enums.District, KeyOfThatHierarchy = district.Id, Label = district.Name, ParentId = parentId };
                db.Treeviews.Add(treeview);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.DivisionId = new SelectList(db.Divisions, "Id", "Name", district.DivisionId);
            return View(district);
        }

        // GET: District/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return HttpNotFound();
            }
            ViewBag.DivisionId = new SelectList(db.Divisions, "Id", "Name", district.DivisionId);
            return View(district);
        }

        // POST: District/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DivisionId")] District district)
        {
            if (ModelState.IsValid)
            {
                db.Entry(district).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DivisionId = new SelectList(db.Divisions, "Id", "Name", district.DivisionId);
            return View(district);
        }

        // GET: District/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return HttpNotFound();
            }
            return View(district);
        }

        // POST: District/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            District district = db.Districts.Find(id);
            db.Districts.Remove(district);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ThanaByDistrict(int id)
        {
            var districts = db.Thanas.Where(i => i.DistrictId == id).Include(i => i.District).AsEnumerable();

            return View("~/Views/Thana/Index.cshtml",districts);
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
