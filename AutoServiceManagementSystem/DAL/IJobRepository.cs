using System;
using System.Collections.Generic;
using System.Linq;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.DAL
{
    public interface IJobRepository : IDisposable
    {
        IEnumerable<Job> GetJobs();
		IEnumerable<Job> GetJobs(int customerId, int carId);
        Job GetJobById(int id);
        Job GetJobById(int customerId, int carId, int jobId);
        void InsertJob(Job job);
        void DeleteJob(int jobId);
        void UpdateJob(Job job);
        void Save();
    }
}