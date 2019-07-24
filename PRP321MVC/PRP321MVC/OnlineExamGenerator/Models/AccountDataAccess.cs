using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace OnlineExamGenerator.Models
{
    public class AccountDataAccess : Account
    {

        public List<Account> GetAccounts()
        {
            List<Account> accounts = new List<Account>();


            using (ExamGenEntities examGen = new ExamGenEntities())
            {

            }


            return accounts;
        }

    }
}