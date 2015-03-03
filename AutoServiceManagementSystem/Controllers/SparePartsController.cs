using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoServiceManagementSystem.DAL;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.Controllers
{
    public class SparePartsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: SpareParts
        public ActionResult Index()
        {
            return View(db.SpareParts.ToList());
        }

        // GET: SpareParts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SparePart sparePart = db.SpareParts.Find(id);
            if (sparePart == null)
            {
                return HttpNotFound();
            }
            return View(sparePart);
        }

        // GET: SpareParts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpareParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SparePartId,Name,Code,Quantity,Price")] SparePart sparePart)
        {
            if (ModelState.IsValid)
            {
                db.SpareParts.Add(sparePart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sparePart);
        }

        // GET: SpareParts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SparePart sparePart = db.SpareParts.Find(id);
            if (sparePart == null)
            {
                return HttpNotFound();
            }
            return View(sparePart);
        }

        // POST: SpareParts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SparePartId,Name,Code,Quantity,Price")] SparePart sparePart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sparePart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sparePart);
        }

        // GET: SpareParts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SparePart sparePart = db.SpareParts.Find(id);
            if (sparePart == null)
            {
                return HttpNotFound();
            }
            return View(sparePart);
        }

        // POST: SpareParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SparePart sparePart = db.SpareParts.Find(id);
            db.SpareParts.Remove(sparePart);
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
