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
        public UserDetails GetUserInfoById(int? userInfoId)
        {
            return context.UsersInfo.Find(userInfoId);
        }

        public UserDetails GetUserInfoByCurrentUser(ApplicationUser currentUser)
        {
            throw new NotImplementedException();
        }

        public void InsertUserInfo(UserDetails userInfo)
        {
            context.UsersInfo.Add(userInfo);
        }

        public void DeleteUserInfo(int? userInfoId)
        {
            UserDetails userInfo = context.UsersInfo.Find(userInfoId);
            context.UsersInfo.Remove(userInfo);
        }

        public void UpdateUserInfo(UserDetails userInfo)
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