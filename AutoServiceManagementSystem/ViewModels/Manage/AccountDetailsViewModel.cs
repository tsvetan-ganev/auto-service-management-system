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
			public IList<UserLoginInfo> Logins { get; set; }
			public string PhoneNumber { get; set; }
			public bool TwoFactor { get; set; }
			public bool BrowserRemembered { get; set; }
			public UserInfo UserInfo { get; set; }
	}
}