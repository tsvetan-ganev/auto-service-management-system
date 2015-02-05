using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoServiceManagementSystem.Models;
using AutoServiceManagementSystem.DAL;

namespace AutoServiceManagementSystem.Controllers
{
    public class SuppliersController : Controller
    {
		private ISupplierRepository supplierRepo;

		public SuppliersController()
		{
			this.supplierRepo = new SupplierRepository(new ASMSContext());
		}

        // GET: Suppliers
        public ActionResult Index()
        {
            return View(supplierRepo.GetSuppliers());
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierRepo.GetSupplierById(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierId,Name,City,DiscountPercentage,WebsiteUrl,LogoUrl")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
				supplierRepo.InsertSupplier(supplier);
				supplierRepo.Save();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierRepo.GetSupplierById(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierId,Name,City,DiscountPercentage,WebsiteUrl,LogoUrl")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
				supplierRepo.UpdateSupplier(supplier);
				supplierRepo.Save();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierRepo.GetSupplierById(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			supplierRepo.DeleteSupplier(id);
			supplierRepo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				supplierRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
