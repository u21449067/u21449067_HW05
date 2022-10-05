using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21449067_HW05.Models
{
    public class BorrowModel
    {
        public int BorrowID { get; set; }

        public BookModel Book { get; set; }

        public DateTime TakenDate { get; set; }

        public DateTime? BroughtDate { get; set; }

        public string Status { get; set; }

        public StudentModel Student { get; set; }

        public BorrowModel()
        {
            Book = new BookModel();
            Student = new StudentModel();
        }
    }
}