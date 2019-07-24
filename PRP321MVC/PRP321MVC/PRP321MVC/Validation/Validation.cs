using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace PRP321MVC.Validation
{
    public class Validation
    {

        public bool checknull(string value)
        {

            if (value == null || value == "")
            {
                return true;
            }

            return false;
        }

        public bool IsValidEmail(string email)
        {

            try
            {
                MailAddress addr = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckString(string value)
        {
            bool @checked = false;

            if (checknull(value))
            {
                @checked = true;
            }
            else
            {
                return false;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsLetter(value,i))
                {
                    @checked = true;
                }
                else
                {
                    @checked = false;
                }
            }

            return @checked;
        }

        public bool CheckNum(string value)
        {
            bool @checked = false;

            if (checknull(value))
            {
                @checked = true;
            }
            else
            {
                return false;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsNumber(value,i))
                {
                    @checked = true;
                }
                else
                {
                    @checked = false;
                }
            }

            return @checked;
        }

        public bool CheckPhoneNumber(string value)
        {
            bool @checked = false;

            if (checknull(value))
            {
                @checked = true;
            }
            else
            {
                return false;
            }

            if (value.Length >= 10)
            {
                @checked = true;
            }
            else
            {
                return false;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsNumber(value, i))
                {
                    @checked = true;
                }
                else
                {
                    @checked = false;
                }
            }
            
            return @checked;
        }

    }



}