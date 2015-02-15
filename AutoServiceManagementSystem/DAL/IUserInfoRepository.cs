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
        UserInfo GetUserInfoById(int? userInfoId);
        void InsertUserInfo(UserInfo userInfo);
        void DeleteUserInfo(int? userInfoId);
        void UpdateUserInfo(UserInfo userInfo);
        void Save();
    }
}
