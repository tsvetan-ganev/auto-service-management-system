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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace AutoServiceManagementSystem.Controllers
{
    public class SuppliersController : Controller
    {
		private ISupplierRepository suppliersRepo;
		private ApplicationUserManager manager;

		public SuppliersController()
		{
			var context = new MyDbContext();
			this.suppliersRepo = new SupplierRepository(context);
			var store = new UserStore<ApplicationUser>(context);
			store.AutoSaveChanges = false;
			this.manager = new ApplicationUserManager(store);
		}

        // GET: Suppliers
		[Authorize()]
        public ActionResult Index()
        {
			var currentUser = manager.FindById(User.Identity.GetUserId());
			var suppliersList = suppliersRepo.GetSuppliers()
				.Where(s => s.User == currentUser);
            return View(suppliersList);
        }

        // GET: Suppliers/Details/5
		[Authorize()]
        public ActionResult Details(int? id)
        {
			var currentUser = manager.FindById(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = suppliersRepo.GetSupplierById(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
			if (supplier.User != currentUser)
			{
				return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
			}
            return View(supplier);
        }

        // GET: Suppliers/Create
		[Authorize()]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize()]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierId,Name,City,DiscountPercentage,WebsiteUrl,LogoUrl,User")] Supplier supplier)
        {
			var currentUser = manager.FindById(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
				supplier.User = currentUser;
				suppliersRepo.InsertSupplier(supplier);
				suppliersRepo.Save();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        // GET: Suppliers/Edit/5
		[Authorize()]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = suppliersRepo.GetSupplierById(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize()]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierId,Name,City,DiscountPercentage,WebsiteUrl,LogoUrl,User")] Supplier supplier)
        {
			var currentUser = manager.FindById(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
				supplier.User = currentUser;
				suppliersRepo.UpdateSupplier(supplier);
				suppliersRepo.Save();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        // GET: Suppliers/Delete/5
		[Authorize()]
        public ActionResult Delete(int? id)
        {
			var currentUser = manager.FindById(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = suppliersRepo.GetSupplierById(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
			if (supplier.User != currentUser)
			{
				return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
			}
            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			suppliersRepo.DeleteSupplier(id);
			suppliersRepo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				suppliersRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
