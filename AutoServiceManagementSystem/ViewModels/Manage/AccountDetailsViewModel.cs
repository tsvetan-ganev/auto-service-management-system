using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.ViewModels.Manage
{
	public class AccountDetailsViewModel
	{
		public bool HasPassword { get; set; }
		public UserDetails UserDetails { get; set; }
	}
}