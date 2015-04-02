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
            return context.Jobs
                .Include(j => j.Car)
                // .Include(j => j.SpareParts
                .ToList();
        }

		public IEnumerable<Job> GetJobs(int customerId, int carId)
		{
			return context.Jobs
				.Where(j =>
					j.Customer.CustomerId == customerId &&
					j.Car.CarId == carId)
                .Include(j => j.SpareParts)
				.ToList();
		}

        public IEnumerable<Job> GetJobsByCar(int carId)
        {
            return context.Jobs
                .Where(j => j.Car.CarId == carId)
                .Include(j => j.SpareParts)
                .ToList();
        }

        public Job GetJobById(int? id)
        {
            return context.Jobs.Find(id);
        }

        public Job GetJobById(int customerId, int carId, int jobId)
        {
            // eagerly loads the spare parts + their suppliers
            return context.Jobs
                .Include(j => j.Car)
                .Where(j => j.Car.Customer.CustomerId == customerId)
                .Where(j => j.Car.CarId == carId)
                .Where(j => j.JobId == jobId)
                .Include(j => j.SpareParts.Select(sp => sp.Supplier))
                .SingleOrDefault();
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