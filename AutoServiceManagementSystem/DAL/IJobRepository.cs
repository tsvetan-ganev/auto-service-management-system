using System;
using System.Collections.Generic;
using System.Linq;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.DAL
{
    public interface IJobRepository : IDisposable
    {
        IEnumerable<Job> GetJobs();
        Job GetJobById(int? id);
        void InsertJob(Job job);
        void DeleteJob(int jobId);
        void UpdateJob(Job job);
        void Save();
    }
}