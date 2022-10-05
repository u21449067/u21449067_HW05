using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21449067_HW05.Models;

namespace u21449067_HW05.Controllers
{
    public class HomeController : Controller
    {

        private SqlConnection myConnection = new SqlConnection(Service.ConnectionString);
        private Service dataService = new Service();

        public ActionResult Index()
        {
            List<BookModel> books = null;
            books = dataService.GetAllBooks();
            return View(books);
        }

        public ActionResult FuzzySearch(string searchText, int type, int author)
        {
            List<BookModel> books = new List<BookModel>();
            try
            {
                myConnection.Open();
                //Read all person records for table

                string searchQry = "SELECT B.bookId, B.name, A.authorId AS 'AuthorID', A.name AS 'AuthorName', A.surname AS 'AuthorSurname', T.typeId AS 'TypeID', T.name AS 'TypeName', B.pagecount, B.point FROM books B JOIN authors A ON b.authorId = a.authorId JOIN [types] T ON b.typeId = T.typeId WHERE B.name LIKE '%" + searchText + "%'";

                if (type == 0 && author != 0)
                {
                    searchQry = searchQry + " AND A.authorID = " + author;
                }
                else if (type != 0 && author == 0)
                {
                    searchQry = searchQry + " AND T.TypeID = " + type;
                }
                else if (type != 0 && author != 0)
                {
                    searchQry = searchQry + " AND T.TypeID = " + type + " AND A.authorID = " + author;
                }

                SqlCommand myFuzzySearch = new SqlCommand(searchQry, myConnection);
                SqlDataReader myReader = myFuzzySearch.ExecuteReader();

                while (myReader.Read())
                {
                    BookModel book = new BookModel();
                    book.BookID = Convert.ToInt32(myReader["bookId"]);
                    book.BookName = Convert.ToString(myReader["name"]);
                    book.Author = new AuthorModel
                    {
                        AuthorID = Convert.ToInt32(myReader["AuthorID"]),
                        AuthorName = Convert.ToString(myReader["AuthorName"]),
                        AuthorSurname = Convert.ToString(myReader["AuthorSurname"])
                    };
                    book.Type = new TypeModel
                    {
                        TypeID = Convert.ToInt32(myReader["TypeID"]),
                        TypeName = Convert.ToString(myReader["TypeName"])
                    };
                    book.pagecount = Convert.ToInt32(myReader["pagecount"]);
                    book.point = Convert.ToInt32(myReader["point"]);
                    ViewBag.Status = 1;
                    books.Add(book);
                }
                ViewBag.SearchStatus = 2;
                ViewBag.SearchText = "Showing results for: '" + searchText + "'";
            }
            catch (Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                myConnection.Close();
            }
            return View("Index", books);
        }

        public ActionResult SetClassFilter(String gradeSymbol)
        {
            //    HomeController.genderFilter = gender;
            return RedirectToAction("Index");
        }

        public ActionResult ViewBook(int BookID)
        {
            return View("BookDetails", dataService.getAllBookBorrows(BookID));
        }



        public ActionResult FuzzySearch1(string searchText1, string Class)
        {
            List<StudentModel> students = new List<StudentModel>();
            try
            {
                myConnection.Open();
                //Read all person records for table

                string searchQry1 = "SELECT S.studentId, S.name, S.surname, S.class FROM students AS S.name LIKE '%" + searchText1 + "%'";

                if(Class != null)
                {
                    searchQry1 = searchQry1 +"AND S.class = " + Class; ;
                }

                SqlCommand myFuzzySearch1 = new SqlCommand(searchQry1, myConnection);
                SqlDataReader myReader = myFuzzySearch1.ExecuteReader();

                while (myReader.Read())
                {
                    StudentModel student = new StudentModel();
                    student.StudentID = Convert.ToInt32(myReader["studentId"]);
                    student.StudentName = Convert.ToString(myReader["name"]);
                    student.Class = Convert.ToString(myReader["class"]);
                    ViewBag.Status = 1;
                    students.Add(student);
                }
                ViewBag.SearchStatus = 2;
                ViewBag.SearchText = "Showing results for: '" + searchText1 + "'";
            }
            catch (Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                myConnection.Close();
            }
            return View("Index", students);
        }

    }
}