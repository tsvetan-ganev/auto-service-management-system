﻿using AutoServiceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceManagementSystem.DAL
{
	public interface ISupplierRepository : IDisposable
	{
		IEnumerable<Supplier> GetSuppliers();
        IEnumerable<Supplier> GetSuppliersByUserId(string userId);
		IEnumerable<Supplier> GetSupplierForSelectList(string userId);
		Supplier GetSupplierById(int supplierId);
		void InsertSupplier(Supplier supplier);
		void DeleteSupplier(int supplierId);
		void UpdateSupplier(Supplier supplier);
		void Save();
	}
}
