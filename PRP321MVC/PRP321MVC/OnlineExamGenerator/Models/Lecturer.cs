using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class Lecturer : Account
    {
        private int id;
        private string name;
        private string surname;
        private string cell;
        private string email;
        private int level;

        #region Properties & Constructors
        public Lecturer()
            : base()
        {
            LectID = 0;
            Name = "";
            Surname = "";
            Cell = "";
            Email = "";
            Level = 0;
        }

        public Lecturer(int cLecturerId)
            : base()
        {
            LectID = cLecturerId;
            Name = "";
            Surname = "";
            Cell = "";
            Email = "";
            Level = 0;
        }

        public Lecturer(string cUsername, string cPassword)
            : base(cUsername, cPassword)
        {
            LectID = 0;
            Name = "";
            Surname = "";
            Cell = "";
            Email = "";
            Level = 0;
        }

        public Lecturer(int cAccountId, string cName, string cSurname, string cCell, string cEmail, int cLevel)
            : base(cAccountId)
        {
            LectID = 0;
            Name = cName;
            Surname = cSurname;
            Cell = cCell;
            Email = cEmail;
            Level = cLevel;
        }

        internal Lecturer(int cLecturerId, int cAccountId, string cName, string cSurname, string cCell, string cEmail, int cLevel)
            : base(cAccountId)
        {
            LectID = cLecturerId;
            Name = cName;
            Surname = cSurname;
            Cell = cCell;
            Email = cEmail;
            Level = cLevel;
        }

        public Lecturer(string cUsername, string cPassword, int cPriority, int cIsAdmin)
            : base(cUsername, cPassword, cPriority, cIsAdmin)
        {
            LectID = 0;
            Name = "";
            Surname = "";
            Cell = "";
            Email = "";
            Level = 0;
        }

        public Lecturer(string cName, string cSurname, string cCell, string cEmail, int cLevel, string cUsername, string cPassword, int cPriority, int cIsAdmin)
            : base(cUsername, cPassword, cPriority, cIsAdmin)
        {
            LectID = 0;
            Name = cName;
            Surname = cSurname;
            Cell = cCell;
            Email = cEmail;
            Level = cLevel;
        }

        internal Lecturer(int cId, string cName, string cSurname, string cCell, string cEmail, int cLevel, string cUsername, string cPassword, int cPriority, int cIsAdmin)
            : base(cUsername, cPassword, cPriority, cIsAdmin)
        {
            LectID = cId;
            Name = cName;
            Surname = cSurname;
            Cell = cCell;
            Email = cEmail;
            Level = cLevel;
        }

        internal Lecturer(int cId, string cName, string cSurname, string cCell, string cEmail, int cLevel, int cACCId, string cUsername, string cPassword, int cPriority, int cIsAdmin)
            : base(cACCId, cUsername, cPassword, cPriority, cIsAdmin)
        {
            LectID = cId;
            Name = cName;
            Surname = cSurname;
            Cell = cCell;
            Email = cEmail;
            Level = cLevel;
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Cell
        {
            get { return cell; }
            set { cell = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int LectID
        {
            get { return id; }
            set { id = value; }
        }
        #endregion
    
    }
}