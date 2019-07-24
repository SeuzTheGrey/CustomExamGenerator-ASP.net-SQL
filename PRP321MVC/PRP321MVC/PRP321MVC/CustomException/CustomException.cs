using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRP321MVC.CustomException
{
    public class CustomException : Exception
    {

        public CustomException()
            : base("Error")
        {
        }

        public CustomException(string message)
            : base(message)
        {
        }

        public CustomException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public class UserExistsException : CustomException
        {
            public UserExistsException()
                : base("Sorry, this User already exists.")
            {
            }

            public UserExistsException(string message)
                : base(message)
            {
            }

            public UserExistsException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        public class InvalidDateException : CustomException
        {
            public InvalidDateException()
                : base("Invalid date entered.")
            {
            }

            public InvalidDateException(string message)
                : base(message)
            {
            }

            public InvalidDateException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        public class PasswordMismatchException : CustomException
        {
            public PasswordMismatchException()
                : base("The passwords entered do not match!.")
            {
            }

            public PasswordMismatchException(string message)
                : base(message)
            {
            }

            public PasswordMismatchException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        public class UserNotFoundException : CustomException
        {
            public UserNotFoundException()
                : base("Sorry, this user does not exist.")
            {
            }

            public UserNotFoundException(string message)
                : base(message)
            {
            }

            public UserNotFoundException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        public class FailedToInsertException : CustomException
        {
            public FailedToInsertException()
                : base("Failed To Insert the Record!")
            {
            }

            public FailedToInsertException(string message)
                : base(message)
            {
            }

            public FailedToInsertException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }
}