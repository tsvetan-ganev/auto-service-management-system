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
using AutoServiceManagementSystem.ViewModels.Suppliers;

namespace AutoServiceManagementSystem.Controllers
{
    [Authorize]
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
        [Route("Suppliers")]
        public ActionResult Index()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var suppliersList = suppliersRepo.GetSuppliers()
                .Where(s => s.User == currentUser);
            return View("Suppliers", suppliersList);
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int id)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            Supplier supplier = suppliersRepo.GetSupplierById(id);

            if (supplier == null || supplier.User != currentUser || supplier.IsDeleted)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateSupplierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = manager.FindById(User.Identity.GetUserId());
                var supplier = new Supplier()
                {
                    Name = viewModel.Name,
                    City = viewModel.City,
                    DiscountPercentage = viewModel.DiscountPercentage,
                    SkypeName = viewModel.SkypeName,
                    EmailAddress = viewModel.EmailAddress,
                    WebsiteUrl = viewModel.WebsiteUrl,
                    IsDeleted = false,
                    User = currentUser
                };
                suppliersRepo.InsertSupplier(supplier);
                suppliersRepo.Save();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int id)
        {
            Supplier supplier = suppliersRepo.GetSupplierById(id);

            if (supplier == null || supplier.IsDeleted)
            {
                return HttpNotFound();
            }

            var viewModel = new EditSupplierViewModel()
            {
                SupplierId = supplier.SupplierId,
                Name = supplier.Name,
                City = supplier.City,
                DiscountPercentage = supplier.DiscountPercentage,
                SkypeName = supplier.SkypeName,
                EmailAddress = supplier.EmailAddress,
                WebsiteUrl = supplier.WebsiteUrl
            };

            return View(viewModel);
        }

        // POST: Suppliers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditSupplierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var supplier = new Supplier()
                {
                    SupplierId = viewModel.SupplierId,
                    Name = viewModel.Name,
                    City = viewModel.City,
                    DiscountPercentage = viewModel.DiscountPercentage,
                    SkypeName = viewModel.SkypeName,
                    EmailAddress = viewModel.EmailAddress,
                    WebsiteUrl = viewModel.WebsiteUrl
                };
                suppliersRepo.UpdateSupplier(supplier);
                suppliersRepo.Save();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int id)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

            Supplier supplier = suppliersRepo.GetSupplierById(id);
            if (supplier == null || supplier.User != currentUser || supplier.IsDeleted)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var supplier = suppliersRepo.GetSupplierById(id);
            supplier.IsDeleted = true;
            suppliersRepo.UpdateSupplier(supplier);
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
