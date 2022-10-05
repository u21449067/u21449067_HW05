using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21449067_HW05.Models
{
    public class BookModel
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public int pagecount { get; set; }
        public int point { get; set; }


        public AuthorModel Author { get; set; }
        public TypeModel Type { get; set; }
        //public BorrowModel Borrow { get; set; }
        public string Status { get; set; }

        public BookModel()
        {
            Author = new AuthorModel();
            Type = new TypeModel();
            //Borrow = new BorrowModel();
            //Status = new Service();
        }
    }
}