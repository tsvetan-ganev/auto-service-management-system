using AutoServiceManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AutoServiceManagementSystem.DAL.Initializers
{
	public class InitializeIdentity : DropCreateDatabaseIfModelChanges<MyDbContext>
	{
		protected override void Seed(MyDbContext context)
		{
			InitializeIdentityForEF(context);
			base.Seed(context);
		}
		private void InitializeIdentityForEF(MyDbContext context)
		{
			
		}
	}
}