using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.DAL.Initializers
{
	public class PromptForDropCreateDatabaseWhenModelChages<TContext>
		: IDatabaseInitializer<TContext>
		where TContext : DbContext
	{
		public void InitializeDatabase(TContext context)
		{
			bool exists = context.Database.Exists();
			if (exists && context.Database.CompatibleWithModel(true))
			{
				return;
			}

			if (exists)
			{
				context.Database.Delete();
			}

			context.Database.Create();
		}
	}
}