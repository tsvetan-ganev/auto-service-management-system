using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.DAL
{
    public interface IUserInfoRepository : IDisposable
    {
        UserDetails GetUserInfoById(int? userInfoId);
        UserDetails GetUserInfoByCurrentUser(ApplicationUser currentUser);
        void InsertUserInfo(UserDetails userInfo);
        void DeleteUserInfo(int? userInfoId);
        void UpdateUserInfo(UserDetails userInfo);
        void Save();
    }
}
