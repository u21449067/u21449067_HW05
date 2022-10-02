using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21449067_HW05.Models
{
    public class StudentModel
    {
        public int StudentID { get; set; }


        public int TypeID { get; set; }


        public string StudentName { get; set; }

        public string StudentSurname { get; set; }

        public DateTime BirthDate { get; set; }

        public char Gender { get; set; }

        public string Class { get; set; }

        public int Point { get; set; }
    }
}