using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.DAL
{
    public interface ISparePartRepository : IDisposable
    {
        IEnumerable<SparePart> GetSpareParts();
        IEnumerable<SparePart> GetSparePartsByJob(Job job);
        IEnumerable<SparePart> GetSparePartsByCar(Car car);
        SparePart GetSparePartById(int? id);
		SparePart GetSparePartById(int jobId, int sparePartId);
        void InsertSparePart(SparePart sparePart);
        void DeleteSparePart(int sparePartId);
        void UpdateSparePart(SparePart sparePart);
        void Save();
    }
}