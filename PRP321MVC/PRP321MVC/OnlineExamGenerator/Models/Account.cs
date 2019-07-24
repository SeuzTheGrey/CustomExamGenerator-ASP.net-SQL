using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class Account
    {
        private int id;
        private string username;
        private string password;
        private int priority;
        private int isAdmin;

        #region Properties & Constructors
        public Account()
        {
            AccId = 0;
            Username = "";
            Password = "";
            Priority = 0;
            IsAdmin = 0;
        }

        public Account(int cId)
        {
            AccId = cId;
            Username = "";
            Password = "";
            Priority = 0;
            IsAdmin = 0;
        }

        public Account(string cUsername, string cPassword)
        {
            AccId = 0;
            Username = cUsername;
            Password = cPassword;
            Priority = 0;
            IsAdmin = 0;
        }

        internal Account(int cId, string cUsername, string cPassword, int cPriority, int cIsAdmin)
        {
            AccId = cId;
            Username = cUsername;
            Password = cPassword;
            Priority = cPriority;
            IsAdmin = cIsAdmin;
        }

        public Account(string cUsername, string cPassword, int cPriority, int cIsAdmin)
        {
            AccId = 0;
            Username = cUsername;
            Password = cPassword;
            Priority = cPriority;
            IsAdmin = cIsAdmin;
        }

        public int IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public int AccId
        {
            get { return id; }
            set { id = value; }
        }
        #endregion
    }
}