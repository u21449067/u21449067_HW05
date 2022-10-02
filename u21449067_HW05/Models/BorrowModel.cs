using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21449067_HW05.Models
{
    public class BorrowModel
    {
        public int BorrowID { get; set; }



        public int StudentID { get; set; }



        public int BookID { get; set; }



        public DateTime TakenDate { get; set; }

        public DateTime BroughtDate { get; set; }

        public bool Status { get; set; }

        public StudentModel Student { get; set; }

        public BorrowModel()
        {
            Student = new StudentModel();
        }
    }
}