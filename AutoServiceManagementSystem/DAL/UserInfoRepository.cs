using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.DAL
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private MyDbContext context;

        public UserInfoRepository(MyDbContext context)
        {
            this.context = context;
        }

        #region IUserInfoRepository Implementation
        public UserInfo GetUserInfoById(int? userInfoId)
        {
            return context.UserInfo.Find(userInfoId);
        }

        public void InsertUserInfo(UserInfo userInfo)
        {
            context.UserInfo.Add(userInfo);
        }

        public void DeleteUserInfo(int? userInfoId)
        {
            UserInfo userInfo = context.UserInfo.Find(userInfoId);
            context.UserInfo.Remove(userInfo);
        }

        public void UpdateUserInfo(UserInfo userInfo)
        {
            context.Entry(userInfo).State = EntityState.Modified;
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