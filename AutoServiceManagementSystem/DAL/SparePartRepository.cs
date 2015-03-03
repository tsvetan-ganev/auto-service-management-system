using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoServiceManagementSystem.Models;
using System.Data.Entity;

namespace AutoServiceManagementSystem.DAL
{
    public class SparePartRepository : ISparePartRepository
    {
        private MyDbContext context;

        public SparePartRepository(MyDbContext context)
        {
            this.context = context;
        }

        #region ISparePartRepository Implementation
        public IEnumerable<SparePart> GetSpareParts()
        {
            return context.SpareParts.ToList();    
        }

        public IEnumerable<SparePart> GetSparePartsByJob(Job job)
        {
            return context.SpareParts
                .Where(p => p.Job == job)
                .ToList();
        }

        public IEnumerable<Models.SparePart> GetSparePartsByCar(Car car)
        {
            return context.SpareParts
                .Where(p => p.Job.Car == car)
                .ToList();
        }

        public Models.SparePart GetSparePartById(int? id)
        {
            return context.SpareParts.Find(id);
        }

        public void InsertSparePart(Models.SparePart sparePart)
        {
            context.SpareParts.Add(sparePart);
        }

        public void DeleteSparePart(int sparePartId)
        {
            SparePart sparePart = context.SpareParts.Find(sparePartId);
            context.SpareParts.Remove(sparePart);
        }

        public void UpdateSparePart(SparePart sparePart)
        {
            context.Entry(sparePart).State = EntityState.Modified;
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