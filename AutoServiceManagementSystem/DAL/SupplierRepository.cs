﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoServiceManagementSystem.Models;
using System.Data.Entity;

namespace AutoServiceManagementSystem.DAL
{
	public class SupplierRepository : ISupplierRepository, IDisposable
	{
		private MyDbContext context;

		public SupplierRepository(MyDbContext context)
		{
			this.context = context;
		}

		#region ISupplierRepository Implementation

		public IEnumerable<Supplier> GetSuppliers()
		{
			return context.Suppliers
                .Where(s => !s.IsDeleted)
                .ToList();
		}

        public IEnumerable<Supplier> GetSuppliersByUserId(string userId)
        {
            return context.Suppliers
                .Where(s => s.User.Id == userId)
                .Where(s => !s.IsDeleted)
                .ToList();
        }

		public IEnumerable<Supplier> GetSupplierForSelectList(string userId)
		{
			var suppliers =  context.Suppliers
				.Where(s => (s.User.Id == userId && !s.IsDeleted) || s.IsDefault)
				.ToList();
			return suppliers;
		}

		public Supplier GetSupplierById(int supplierId)
		{
			return context.Suppliers.Find(supplierId);
		}

		public void InsertSupplier(Supplier supplier)
		{
			context.Suppliers.Add(supplier);
		}

		public void DeleteSupplier(int supplierId)
		{
			Supplier supplier = context.Suppliers.Find(supplierId);
			context.Suppliers.Remove(supplier);
		}

		public void UpdateSupplier(Supplier supplier)
		{
			context.Entry(supplier).State = EntityState.Modified;
		}

		public void Save()
		{
			context.SaveChanges();
		}

		#endregion

		private bool disposed = false;
		
		public virtual void Dispose(bool disposing)
		{
			if (!disposed && disposing)
			{
				context.Dispose();
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

	}
}