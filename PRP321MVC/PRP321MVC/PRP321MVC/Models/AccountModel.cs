using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class AccountModel
    {
        private int id;
        private string username;
        private string password;
        private int priority;
        private int isAdmin;

        #region Properties & Constructors
        public AccountModel()
        {
            AccId = 0;
            Username = "";
            Password = "";
            Priority = 0;
            IsAdmin = 0;
        }

        public AccountModel(int cId)
        {
            AccId = cId;
            Username = "";
            Password = "";
            Priority = 0;
            IsAdmin = 0;
        }

        public AccountModel(string cUsername, string cPassword)
        {
            AccId = 0;
            Username = cUsername;
            Password = cPassword;
            Priority = 0;
            IsAdmin = 0;
        }

        /// <summary>
        /// This constructor is specific to assigning data from the database to the class
        /// and shouldnt be used for any front end code
        /// </summary>
        /// <param name="cId"></param>
        /// <param name="cUsername"></param>
        /// <param name="cPassword"></param>
        /// <param name="cPriority"></param>
        /// <param name="cIsAdmin"></param>
        internal AccountModel(int cId, string cUsername, string cPassword, int cPriority, int cIsAdmin)
        {
            AccId = cId;
            Username = cUsername;
            Password = cPassword;
            Priority = cPriority;
            IsAdmin = cIsAdmin;
        }

        public AccountModel(string cUsername, string cPassword, int cPriority, int cIsAdmin)
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

        #region CRUD
        public bool AddAccount()
        {
            bool isInserted = false;
            string tableName = "Account";
            List<string> columns = new List<string>(), colValues = ConvertToColumnValues(this);
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            columns.Add("username");
            columns.Add("AccPassword");
            columns.Add("AccPriority");
            columns.Add("isAdmin");

            isInserted = dh.Insert(tableName, columns, colValues);
            columns.Clear();
            colValues.Clear();

            return isInserted;
        }

        public bool ChangeAccount(List<string> where)
        {
            bool isUpdated = false;
            List<string> colValues = ConvertToColumnValues(this);
            Dictionary<string, string> colValuePairs = new Dictionary<string, string>();
            string tableName = "Account";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            colValuePairs.Add("username", colValues[0]);
            colValuePairs.Add("AccPassword", colValues[1]);
            colValuePairs.Add("AccPriority", colValues[2]);
            colValuePairs.Add("isAdmin", colValues[3]);
            isUpdated = dh.Update(tableName, colValuePairs, where);

            return isUpdated;
        }

        public bool RemoveAccount(List<string> where)
        {
            bool isDeleted = false;
            string tableName = "Account";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            isDeleted = dh.Delete(tableName, where);

            return isDeleted;
        }

        public List<AccountModel> GetAccount(List<string> columns = null, List<string> where = null)
        {
            string tableName = "Account";
            List<AccountModel> accounts = new List<AccountModel>();
            List<string> records = new List<string>();
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            records = dh.Select(tableName, columns, where);
            Dictionary<string, string> fieldValues = new Dictionary<string, string>();

            foreach (string item in records)
            {
                fieldValues.Clear();
                int counter = 1;
                string[] fields = item.Split(',');
                foreach (string attribute in fields)
                {
                    switch (counter)
                    {
                        case 1:
                            fieldValues.Add("id", attribute);
                            break;
                        case 2:
                            fieldValues.Add("username", attribute);
                            break;
                        case 3:
                            fieldValues.Add("AccPassword", attribute);
                            break;
                        case 4:
                            fieldValues.Add("AccPriority", attribute);
                            break;
                        case 5:
                            fieldValues.Add("isAdmin", attribute);
                            break;
                    }
                    counter++;
                }
                accounts.Add(ConvertToAccount(fieldValues));
            }
            return accounts;
        }
        #endregion

        #region Data Manipulation
        private List<string> ConvertToColumnValues(AccountModel a)
        {
            List<string> colValues = new List<string>();

            try
            {
                colValues.Add("'" + a.Username + "'");
                colValues.Add("'" + a.Password + "'");
                colValues.Add(a.Priority.ToString());
                colValues.Add(a.IsAdmin.ToString());
            }
            catch (Exception)
            {
                throw;
            }

            return colValues;
        }

        private static AccountModel ConvertToAccount(Dictionary<string, string> fieldValues)
        {
            AccountModel a = new AccountModel();
            foreach (KeyValuePair<string, string> row in fieldValues)
            {
                switch (row.Key)
                {
                    case "id":
                        try
                        {
                            a.AccId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "username":
                        a.Username = row.Value;
                        break;
                    case "AccPassword":
                        a.Password = row.Value;
                        break;
                    case "AccPriority":
                        try
                        {
                            a.Priority = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "isAdmin":
                        try
                        {
                            a.IsAdmin = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    default:
                        break;
                }
            }
            return a;
        }
        #endregion
    }
}