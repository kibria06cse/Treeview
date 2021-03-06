﻿using System;
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
    public class DivisionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Division
        public ActionResult Index()
        {
            var divisions = db.Divisions.Include(d => d.Base);
            return View(divisions.ToList());
        }

        // GET: Division/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Division division = db.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        // GET: Division/Create
        public ActionResult Create(int? id)
        {
            ViewBag.BaseId = new SelectList(db.Bases, "Id", "Name");
            var model = new Division();
            model.BaseId = id.HasValue ? id.Value : 0;
            return View(model);
        }

        // POST: Division/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,BaseId")] Division division)
        {
            if (ModelState.IsValid)
            {
                db.Divisions.Add(division);
                db.SaveChanges();
                int parentId = 0;
                var parent = db.Treeviews.Where(i => i.HierarchyTypeId == (int)Enums.Base && i.KeyOfThatHierarchy == division.BaseId);
                if (parent != null)
                    parentId = parent.FirstOrDefault().Id;
                var treeview = new Treeview { HierarchyTypeId = (int)Enums.Division, KeyOfThatHierarchy = division.Id, Label = division.Name, ParentId = parentId };
                db.Treeviews.Add(treeview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BaseId = new SelectList(db.Bases, "Id", "Name", division.BaseId);
            return View(division);
        }

        // GET: Division/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Division division = db.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            ViewBag.BaseId = new SelectList(db.Bases, "Id", "Name", division.BaseId);
            return View(division);
        }

        // POST: Division/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,BaseId")] Division division)
        {
            if (ModelState.IsValid)
            {
                db.Entry(division).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BaseId = new SelectList(db.Bases, "Id", "Name", division.BaseId);
            return View(division);
        }

        // GET: Division/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Division division = db.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        // POST: Division/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Division division = db.Divisions.Find(id);
            db.Divisions.Remove(division);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DistrictsByDivision(int id)
        {
            var districts = db.Districts.Where(i => i.DivisionId == id).Include(i => i.Division).AsEnumerable();

            return View("~/Views/District/Index.cshtml",districts);
        }

        public ActionResult DivisionFromBase(IEnumerable<Division> divisions)
        {
            return View("Index", divisions);
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
