using System;
using System.Collections.Generic;
using System.Linq;
using AutoServiceManagementSystem.Models;
using System.Data.Entity;

namespace AutoServiceManagementSystem.DAL
{
    public class JobRepository : IJobRepository
    {
        private MyDbContext context;

        public JobRepository(MyDbContext context)
        {
            this.context = context;
        }

        #region IJobRepository Implementation
        public IEnumerable<Job> GetJobs()
        {
            return context.Jobs.ToList();
        }

        public IEnumerable<Job> GetJobsByCar(int carId)
        {
            return context.Jobs
                .Where(j => j.Car.CarId == carId)
                .ToList();
        }

        public Job GetJobById(int? id)
        {
            return context.Jobs.Find(id);
        }

        public void InsertJob(Job job)
        {
            context.Jobs.Add(job);
        }

        public void DeleteJob(int jobId)
        {
            Job job = context.Jobs.Find(jobId);
            context.Jobs.Remove(job);
        }

        public void UpdateJob(Models.Job job)
        {
            context.Entry(job).State = EntityState.Modified;
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