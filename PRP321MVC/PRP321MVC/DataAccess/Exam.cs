//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Exam
    {
        public int id { get; set; }
        public Nullable<int> LecturerID { get; set; }
        public string ExamName { get; set; }
        public string FilePath { get; set; }
    
        public virtual Lecturer Lecturer { get; set; }
    }
}
