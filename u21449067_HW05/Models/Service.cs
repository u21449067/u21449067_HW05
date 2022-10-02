using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace u21449067_HW05.Models
{
    public class Service
    {
        public static string ConnectionString = "Data Source=./SQLEXPRESS;Initial Catalog=Library;Integrated Security=True";

        public static List<TypeModel> types = null;
        public static List<AuthorModel> authors = null;
        public static List<StudentModel> students = null;
        public List<BorrowModel> borrows = null;
        public List<StudentModel> classes = null;

        public Service()
        {
            types = GetTypes();
            authors = GetAuthors();
            students = GetStudents();
            borrows = GetBorrows();
            classes = GetClasses();
        }

        public List<BookModel> GetAllBooks()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<BookModel> books = new List<BookModel>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT B.bookId, B.name, A.authorId AS 'AuthorID', A.name AS 'AuthorName', A.surname AS 'AuthorSurname', T.typeId AS 'TypeID', T.name AS 'TypeName', B.pagecount, B.point FROM books B JOIN authors A ON b.authorId = a.authorId JOIN [types] T ON b.typeId = T.typeId", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookModel bk = new BookModel
                            {
                                BookID = Convert.ToInt32(reader["bookId"]),
                                BookName = Convert.ToString(reader["name"]),
                                Author = new AuthorModel
                                {
                                    AuthorID = Convert.ToInt32(reader["AuthorID"]),
                                    AuthorName = Convert.ToString(reader["AuthorName"]),
                                    AuthorSurname = Convert.ToString(reader["AuthorSurname"]),
                                },
                                Type = new TypeModel
                                {
                                    TypeID = Convert.ToInt32(reader["TypeID"]),
                                    TypeName = Convert.ToString(reader["TypeName"]),
                                },
                                pagecount = Convert.ToInt32(reader["pagecount"]),
                                point = Convert.ToInt32(reader["point"])
                            };
                            books.Add(bk);
                        }
                    }
                }
                con.Close();
            }
            return books;

        }

        public List<AuthorModel> GetAuthors()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<AuthorModel> authors = new List<AuthorModel>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT authorid, name, surname FROM authors", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        AuthorModel tempAuthor = new AuthorModel
                        {
                            AuthorID = 0,
                            AuthorName = "Please Select an Author"
                        };

                        authors.Add(tempAuthor);
                        while (reader.Read())
                        {
                            AuthorModel ar = new AuthorModel
                            {
                                AuthorID = Convert.ToInt32(reader["authorid"]),
                                AuthorName = Convert.ToString(reader["name"]),
                                AuthorSurname = Convert.ToString(reader["surname"]),
                            };
                            authors.Add(ar);
                        }
                    }
                }
                con.Close();
            }
            return authors;
        }

        public List<TypeModel> GetTypes()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<TypeModel> types = new List<TypeModel>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT typeid, name FROM types", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        TypeModel tempType = new TypeModel
                        {
                            TypeID = 0,
                            TypeName = "Please Select a Type"
                        };

                        types.Add(tempType);
                        while (reader.Read())
                        {
                            TypeModel ty = new TypeModel
                            {
                                TypeID = Convert.ToInt32(reader["typeid"]),
                                TypeName = Convert.ToString(reader["name"]),
                            };
                            types.Add(ty);
                        }
                    }
                }
                con.Close();
            }
            return types;
        }


        public List<StudentModel> GetClasses()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<StudentModel> classes = new List<StudentModel>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT class, name FROM students", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        StudentModel tempclass = new StudentModel
                        {
                            Class = "Please select class"
                        };

                        classes.Add(tempclass);
                        while (reader.Read())
                        {
                            StudentModel tpcl = new StudentModel
                            {
                                Class = Convert.ToString(reader["class"])
                        };
                            classes.Add(tpcl);
                        }
                    }
                }
                con.Close();
            }
            return classes;
        }







        public List<StudentModel> GetStudents()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<StudentModel> students = new List<StudentModel>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT studentid, name, surname, birthdate, gender, class, point FROM students", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StudentModel st = new StudentModel
                            {
                                StudentID = Convert.ToInt32(reader["studentid"]),
                                StudentName = Convert.ToString(reader["name"]),
                                StudentSurname = Convert.ToString(reader["surname"]),
                                BirthDate = Convert.ToDateTime(reader["birthdate"]),
                                Gender = Convert.ToChar(reader["gender"]),
                                Class = Convert.ToString(reader["class"]),
                                Point = Convert.ToInt32(reader["point"]),

                            };
                            students.Add(st);
                        }
                    }
                }
                con.Close();
            }
            return students;
        }

        public List<BorrowModel> GetBorrows()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<BorrowModel> tempBorrows = new List<BorrowModel>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT [borrowId] ,[studentId] ,[bookId] ,[takenDate] ,[broughtDate] FROM [Library].[dbo].[borrows]", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BorrowModel tempBr = new BorrowModel
                            {
                                BorrowID = Convert.ToInt32(reader["borrowId"]),
                                StudentID = Convert.ToInt32(reader["studentId"]),
                                BookID = Convert.ToInt32(reader["bookId"]),
                                TakenDate = Convert.ToDateTime(reader["takenDate"]),
                                BroughtDate = Convert.ToDateTime(reader["broughtDate"])
                            };
                            tempBorrows.Add(tempBr);
                        }
                    }
                }
                con.Close();
            }
            return tempBorrows;
        }

        //public string GetStatus()
        //{
            // TODO: USE LAMDA / LINQ to filter borrows by bookID and Last TakenDate
            // and then see if it was brought back and return out or borrowed
            //borrows

            //public string getStatus(int BookID)
            //if () 
            //{
            //    IEnumerable<string> query = borrows
            //        .Where(b => b.BroughtDate == isnull)
            //        .orderBy(b => b.descending);
            //    return "Out";
            //}

            //else
            //{
            //    IEnumerable<string> query = borrows
            //        .Where(b => b.BroughtDate == isnotnull)
            //        .orderBy(b => b.descending);
            //    return "Available";
            //}
            //ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //List<StudentModel> tempSatus = new List<StudentModel>();
            //using (SqlConnection con = new SqlConnection(ConnectionString))
            //{
            //    con.Open();
            //    using (SqlCommand stcmd = new SqlCommand("SELECT B.bookId, B.name, A.authorId AS 'AuthorID', A.name AS 'AuthorName', A.surname AS 'AuthorSurname', T.typeId AS 'TypeID', T.name AS 'TypeName', B.pagecount, B.point, CASE WHEN((SELECT TOP 1 BR.broughtDate FROM borrows BR WHERE BR.bookId = B.bookId ORDER BY BR.takenDate DESC) IS NOT NULL) THEN 'Available' ELSE 'Out' END AS 'Status'FROM books B JOIN authors A ON b.authorId = a.authorId JOIN[types] T ON b.typeId = T.typeId WHERE B.name LIKE '%da%'ORDER BY B.bookId", con))
            //    {
            //        using (SqlDataReader reader =  stcmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                StudentModel tempSt = new StudentModel
            //                {
            //                    StudentID = Convert.ToInt32(reader["studentId"]),
            //                };
            //                tempSatus.Add(tempSt);
            //            }
            //        }
            //    }
            //    con.Close();

            //}
            //return tempSatus;
        //}
    }

   
}