using LIBRARY.Models;
using LIBRARY.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using static System.Reflection.Metadata.BlobBuilder;

namespace LIBRARY
{
    internal class Program
    {
        public static AdminRepository adminRepository;
        public static CategoryRepository categoryRepository;
        public static UserRepository userRepository;
        public static BookRepository bookRepository;
        public static BorrowRepository borrowRepository;

        static void Main(string[] args)
        {
            using ApplicationDbContext DBContext = new ApplicationDbContext() ;

            // Create repositories
             adminRepository = new AdminRepository(DBContext);
             categoryRepository = new CategoryRepository(DBContext);
             userRepository = new UserRepository(DBContext);
             bookRepository = new BookRepository(DBContext);
             borrowRepository = new BorrowRepository(DBContext);

            //Admin and user menu 
            bool ExitFlag = false;
            int choice;
            do
            {
                Console.WriteLine("------------------Welcome to Library----------------");
                Console.WriteLine("\nLogin as..\n\nEnter the number of your choice: ");
                Console.WriteLine("\n  1- Admin ");
                Console.WriteLine("\n  2- User");
                Console.WriteLine("\n  3- Exit");
                choice = handelIntError(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        adminRegistration();
                        break;

                    case 2:
                        UserRegistration();
                        break;

                    case 3:
                        ExitFlag = true;
                        break;

                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;
                }
                Console.Clear();

            } while (ExitFlag != true);

        }


        

        //Add new admin or verify admin login
        static void adminRegistration()
        {
            bool ExitFlag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("---------------Welcome---------------");
                Console.WriteLine("\n\n Enter the number of operation you need :");
                Console.WriteLine("\n   1- Register");
                Console.WriteLine("\n   2- Log in");
                Console.WriteLine("\n   3- Exit");

                int choice = handelIntError(Console.ReadLine());


                Console.WriteLine("\n------------------------------------------------\n");
                //Register new admin
                if (choice == 1)
                {
                    Console.Clear() ;
                    Console.WriteLine("Enter Your Name");
                    string AdminName = Console.ReadLine();

                    Console.WriteLine("Enter Your Email 'e.g admin@example.com'");
                    string email = Console.ReadLine();

                    Console.WriteLine("Enter Your Passward 'e.g Pass1234'");
                    string passward = Console.ReadLine();

                    try
                    {
                        var admin = new Admin { AName = AdminName, Email = email, Password = passward };

                        adminRepository.InsertAdmin(admin);
                        Console.WriteLine("Admin added successfully!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Faild to add new Admin");
                        Console.WriteLine(e.ToString());
                        
                    }
                }

                //Verifed admin account
                else if(choice == 2)
                {
                    Console.WriteLine("Enter Your Name");
                    string name = Console.ReadLine();

                    var adminList = adminRepository.GetAdminByName(name);
                    var adminName = adminList.FirstOrDefault();
                    
                    Console.Clear();
                    if (adminName != null)
                    {
                        name = adminName.AName;
                        Console.WriteLine($"---------------Welcome {name}---------------");

                        Console.WriteLine("Enter Your Email ");
                        string email = Console.ReadLine();

                        Console.WriteLine("Enter Your Passward ");
                        string passward = Console.ReadLine();
                        var adminPassList = adminRepository.verifyAdmin(email, passward);
                        var admin = adminPassList.FirstOrDefault();
                        if (admin != null && (admin.AName == name))
                        {
                            adminMenu();
                        }
                        else
                            Console.WriteLine("Incorrect email or password");

                    }
                    else
                        Console.WriteLine("Name Not Exist");

                }

                else if (choice == 3 )
                    ExitFlag = false;
                else 
                    Console.WriteLine("Incorrect Choice..\n ");

                Console.WriteLine("press enter key to continue");
                string cont = Console.ReadLine();

            } while (ExitFlag != true);
        }

        //******************Admin Menu********************************
        static void adminMenu()
        {
            bool ExitFlag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("\n\n Enter the number of operation you need :");
                Console.WriteLine("\n   1 - Add New Book");
                Console.WriteLine("\n   2 - Remove Book");
                Console.WriteLine("\n   3 - Update Book");
                Console.WriteLine("\n   4 - Display All Books\n");

                Console.WriteLine("\n   5 - Add New Category");
                Console.WriteLine("\n   6 - Update Category");
                Console.WriteLine("\n   7 - Remove Category");
                Console.WriteLine("\n   8 - Display All Category\n");
                

                Console.WriteLine("\n   9 - Add New User");
                Console.WriteLine("\n   10- Update User");
                Console.WriteLine("\n   11- Remove User");
                Console.WriteLine("\n   12- Display All Users\n");

                Console.WriteLine("\n   13- Exit");

                int choice = handelIntError(Console.ReadLine());
                Console.Clear ();
                switch (choice)
                {
                    case 1:
                        AddNewBook();
                        break;

                    case 2:
                        RemoveBook();
                        break;

                    case 3:
                        UpdateBook();
                        break;

                    case 4:
                        viewAllBooks();
                        break;
                    case 5:
                        AddNewCategory();
                        break;
                    case 6:
                        UpdateCategory();
                        break;
                    case 7:
                        RemoveCategory();
                        break;
                    case 8:
                        viewAllCategories();
                        break;
                    case 9:
                        AddNewUser();
                        break;
                    case 10:
                        UpdateUser();
                        break;
                    case 11:
                        RemoveUser();
                        break;
                    case 12:
                        viewAllUsers();
                        break;
                    case 13:
                        ExitFlag = true;
                        break;

                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;
                }

                Console.WriteLine("press enter key to continue");
                string cont = Console.ReadLine();

            } while (ExitFlag != true);

        }

        //******************Book*******************

        //Add New Book By Admin
        static void AddNewBook()
        {
            Console.WriteLine("------------------------Adding Book-------------------------\n");

            Console.WriteLine("Enter the following book data\n ");

            Console.WriteLine(" Book Title :  ");
            string title = Console.ReadLine();

            Console.WriteLine("Author of the Book :  ");
            string author = Console.ReadLine();

            Console.WriteLine(" Book price :  ");
            double price = handelDoubleError( Console.ReadLine());

            Console.WriteLine(" Book Total Copies :  ");
            int totalCopies = handelIntError( Console.ReadLine());

            Console.WriteLine(" Book Borrowing Period :  ");
            int borrowingPeriod = handelIntError(Console.ReadLine());

            Console.WriteLine("choose Book Category Number:  ");
            var catg = categoryRepository.GetAllCategory();
            foreach (var c in catg)
            {
                Console.WriteLine($"{c.CID}. {c.CName}");
            }
            int category = handelIntError(Console.ReadLine());
            try
            {
                var book = new Book { BTitle = title, Author = author, Price = price, TotalCopies = totalCopies, BorrowingPeriod = borrowingPeriod ,CID = category };
                bookRepository.InsertBook(book);
                bookRepository.updateCategoryCopyOnAddingBook(book.CID);
                Console.WriteLine("Book added successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to add new book!");
                Console.WriteLine(e.ToString());
            }
        }

        //Display All Books
        static void viewAllBooks()
        {
            Console.WriteLine(new string('*', 140));
            Console.WriteLine("\t\t\t\t\t\t Books Menu\n");
            Console.WriteLine(new string('*', 140));
            var books = bookRepository.GetAllBooks();

            foreach (var b in books)
            {
                Console.WriteLine($"{b.BID}: {b.BTitle} - Author: {b.Author} - Copies: {b.TotalCopies} - Price {b.Price}");
            }
        }
        //Remove Book
        static void RemoveBook()
        {
            Console.WriteLine(new string('*', 140));
            Console.WriteLine("\t\t\t\t\t\t Removing Book\n");
            Console.WriteLine(new string('*', 140));
            viewAllBooks();
            Console.WriteLine(new string('*', 140));
            Console.WriteLine("Enter Book Number");
            int Bnum = handelIntError(Console.ReadLine());
            var book= bookRepository.GetAllBooks().FirstOrDefault(b=> b.BID == Bnum);
            if(book != null)
            {
                if (book.BorrowedCopies > 0)
                    Console.WriteLine("There are borrowed copies of this book. Cannot be deleted");

                else
                {
                    try
                    {
                        Console.WriteLine($"{book.BID}: {book.BTitle} - Author: {book.Author} - Copies: {book.TotalCopies} - Price {book.Price}");
                        Console.WriteLine("To Confirm deleting enter 1 ");
                        string con = Console.ReadLine();
                        if (con == "1")
                        {
                            bookRepository.DeleteBookById(Bnum);
                            bookRepository.updateCategoryCopyOnDeleting(book.CID);
                            Console.WriteLine($"{book.BTitle} deleted successfully");
                        }
                        else
                            Console.WriteLine($"{book.BTitle} not deleted");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Faild on deleting this book");
                        Console.WriteLine(e.Message);
                    }
                }
            }
            
           
        }

        //update books by book name
        static void UpdateBook()
        {
            Console.WriteLine(new string('*', 100));
            Console.WriteLine("\t\t\t\t\t\t Update Book\n");
            Console.WriteLine(new string('*', 100));
            viewAllBooks();
            Console.WriteLine("Enter book name");
            string bname = Console.ReadLine();
            var book = bookRepository.GetBookByName(bname);
            if (book != null)
            {
                Console.WriteLine($"{book.BID}: {book.BTitle} - Author: {book.Author} - Copies: {book.TotalCopies} - Price {book.Price}");
                Console.WriteLine("Enter your chouse to Edite : ");
                Console.WriteLine("1. Book Title.\n2. Author. \n3.Price \n4.Copies");
                int choise = handelIntError(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        Console.WriteLine("Enter New Title");
                        string title = Console.ReadLine();
                        book.BTitle = title;
                        bookRepository.UpdateBookByName(bname);
                        Console.WriteLine("Book Title updated successfully!\n");
                        Console.WriteLine($"{book.BID}: {book.BTitle} - Author: {book.Author} - Copies: {book.TotalCopies} - Price {book.Price}");
                        break;

                    case 2:
                        Console.WriteLine("Enter New Author name");
                        string author = Console.ReadLine();
                        book.Author = author;
                        bookRepository.UpdateBookByName(bname);
                        Console.WriteLine("Book Author updated successfully!\n");
                        Console.WriteLine($"{book.BID}: {book.BTitle} - Author: {book.Author} - Copies: {book.TotalCopies} - Price {book.Price}");

                        break;

                    case 3:
                        Console.WriteLine("Enter New Price");
                        double price = handelDoubleError(Console.ReadLine());
                        book.Price = price;
                        bookRepository.UpdateBookByName(bname);
                        Console.WriteLine("Book Price updated successfully!\n");
                        Console.WriteLine($"{book.BID}: {book.BTitle} - Author: {book.Author} - Copies: {book.TotalCopies} - Price {book.Price}");

                        break;
                    case 4:
                        Console.WriteLine("Enter New copies number");
                        int copies = handelIntError(Console.ReadLine());
                        book.TotalCopies = copies;
                        bookRepository.UpdateBookByName(bname);
                        Console.WriteLine("Book copies number updated successfully!\n");
                        Console.WriteLine($"{book.BID}: {book.BTitle} - Author: {book.Author} - Copies: {book.TotalCopies} - Price {book.Price}");

                        break;
                    default:
                        Console.WriteLine("Incorrect Input.. ");
                        break;

                }
            }
            else
                Console.WriteLine("Incorect Book title");

        }

        //******************Category******************
        static void AddNewCategory()
        {
            Console.WriteLine("------------------------Adding New Category-------------------------\n");

            bool flage = false;
            Console.WriteLine("Enter the Category name\n ");
            string catgName = Console.ReadLine();

            try
            {
                var catgList = categoryRepository.GetAllCategory();
                if(catgList != null)
                {
                    foreach (var c in catgList)
                    {
                        if (c.CName == catgName)
                            flage = true;
                    }
                }

                if(flage != true)
                {
                    var catg = new Category { CName = catgName };
                    categoryRepository.InsertCategory(catg);
                    Console.WriteLine("Category added successfully!");
                }
                else
                    Console.WriteLine("This Category name is exists.. ");
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to add new Category!");
                Console.WriteLine(e.ToString());
            }
        }

        static void UpdateCategory()
        {
            Console.WriteLine(new string('*', 100));
            Console.WriteLine("\t\t\t\t\t\t Update Category\n");
            Console.WriteLine(new string('*', 100));
            viewAllCategories();
            Console.WriteLine("Enter Category name");
            string cname = Console.ReadLine();
            var catg = categoryRepository.GetCategoryByName(cname);
            if (catg != null)
            {
                Console.WriteLine($"{catg.CID}: {catg.CName} ");

                Console.WriteLine("Enter New Category name");
                string newCatg = Console.ReadLine();
                catg.CName = newCatg;
                categoryRepository.UpdateCategoryByName(cname);
                Console.WriteLine("Category name updated successfully!\n");
                Console.WriteLine($"{catg.CID}: {catg.CName} ");
            }

        }

        static void viewAllCategories()
        {
            Console.WriteLine(new string('*', 100));
            Console.WriteLine("\t\t\t\t\t\t Categories Menu\n");
            Console.WriteLine(new string('*', 100));
            var catg = categoryRepository.GetAllCategory();

            foreach (var c in catg)
            {
                Console.WriteLine($"{c.CID}: {c.CName} - Number of books: {c.NumberOfBooks}");
            }
            Console.WriteLine(new string('*', 100));
        }

        static void RemoveCategory()
        {
            Console.WriteLine(new string('*', 100));
            Console.WriteLine("\t\t\t\t\t\t Removing Category\n");
            Console.WriteLine(new string('*', 100));
            viewAllCategories();
            Console.WriteLine("Enter Category Number");
            int Cnum = handelIntError(Console.ReadLine());
            var catg = categoryRepository.GetAllCategory().FirstOrDefault(c => c.CID == Cnum);
            if (catg != null)
            {
                if (catg.NumberOfBooks > 0)
                    Console.WriteLine("There are Books registered to this Category. Cannot be deleted");

                else
                {
                    try
                    {
                        Console.WriteLine($"{catg.CID}: {catg.CName} ");
                        Console.WriteLine("To Confirm deleting enter 1 ");
                        string con = Console.ReadLine();
                        if (con == "1")
                        {
                            categoryRepository.DeleteCategoryById(Cnum);
                            Console.WriteLine($"{catg.CName} deleted successfully");
                        }
                        else
                            Console.WriteLine($"{catg.CName} not deleted");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Faild on deleting this Category");
                        Console.WriteLine(e.Message);
                    }
                }
            }


        }

        //******************User*******************
        static void AddNewUser()
        {
            Console.WriteLine("------------------------Adding User-------------------------\n");

            Console.WriteLine("Enter the following User data\n ");

            bool flage = false;

            Console.WriteLine(" User Name :  ");
            string userName = Console.ReadLine();
            var userList = userRepository.GetAllUsers();
            if (userList != null)
            {
                foreach (var u in userList)
                {
                    if (u.UName == userName)
                        flage = true;
                }
            }

            if (flage != true)
            {
                Console.WriteLine("Passcode :  ");
                string passcode = Console.ReadLine();

                Console.WriteLine("Gender {0:Male, 1:Femal} :  ");
                string input = Console.ReadLine();
                if (Enum.TryParse(input, true, out Gender gender))
                {
                    try
                    {
                        var user = new User { UName = userName, Passcode = passcode, gender = gender };
                        userRepository.InsertUser(user);
                        Console.WriteLine("User added successfully!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Failed to add new User!");
                        Console.WriteLine(e.ToString());
                    }

                }
                else
                    Console.WriteLine("Invalid gender input. Please enter a valid option.");
            }
            else
                Console.WriteLine("This User name is already registered .. ");
          
        }

        static void viewAllUsers()
        {
            Console.WriteLine(new string('*', 100));
            Console.WriteLine("\t\t\t\t\t\t Users Menu\n");
            Console.WriteLine(new string('*', 100));
            var user = userRepository.GetAllUsers();
            if (user != null)
            {
                foreach (var u in user)
                {
                    if (u.gender == 0)
                        Console.WriteLine($"{u.UID}: {u.UName} - Gender: Male - Passcode : {u.Passcode} ");
                    else
                        Console.WriteLine($"{u.UID}: {u.UName} - Gender: Female - Passcode : {u.Passcode}");

                }
            }
           
        }

        static void UpdateUser()
        {
            Console.WriteLine(new string('*', 100));
            Console.WriteLine("\t\t\t\t\t\t Update User Passcode\n");
            Console.WriteLine(new string('*', 100));
            viewAllUsers();
            Console.WriteLine("Enter User name");
            string username = Console.ReadLine();
            var user = userRepository.GetUserByName(username);
            if (user != null)
            {
                Console.WriteLine($"{user.UID}: {user.UName} Passcode: {user.Passcode} ");

                Console.WriteLine("Enter New Passcode");
                string newPasscode = Console.ReadLine();
                user.Passcode = newPasscode;
                userRepository.UpdateUserByName(username);
                Console.WriteLine("User Passcode updated successfully!\n");
                Console.WriteLine($"{user.UID}: {user.UName} Passcode: {user.Passcode} ");
            }

        }

        static void RemoveUser()
        {
            Console.WriteLine(new string('*', 100));
            Console.WriteLine("\t\t\t\t\t\t Removing User\n");
            Console.WriteLine(new string('*', 100));
            viewAllUsers();
            Console.WriteLine("Enter User Number");
            int userNum = handelIntError(Console.ReadLine());
            var user = userRepository.GetAllUsers().FirstOrDefault(u => u.UID == userNum);
            
            if (user != null)
            {
                try
                {
                    if (user.Borrows != null  && userRepository.borrowedCopies(user.UID) > 0)
                        Console.WriteLine("This user borrowed books. Cannot be deleted");
                    else
                    {

                        Console.WriteLine($"{user.UID}: {user.UName} - Gender: Male - Passcode : {user.Passcode} ");
                        Console.WriteLine("To Confirm deleting enter 1 ");
                        string con = Console.ReadLine();
                        if (con == "1")
                        {
                            userRepository.DeleteUserById(userNum);
                            Console.WriteLine($"{user.UName} deleted successfully");
                        }
                        else
                            Console.WriteLine($"{user.UName} not deleted");

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Faild on deleting this user");
                    Console.WriteLine(e.Message);
                }
            }
          
        }


        //*******************************User Menu*********************************
        static void UserRegistration()
        {
            bool ExitFlag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("---------------Welcome---------------");
                Console.WriteLine("\n\n Enter the number of operation you need :");
                Console.WriteLine("\n   1- Register");
                Console.WriteLine("\n   2- Log in");
                Console.WriteLine("\n   3- Exit");

                int choice = handelIntError(Console.ReadLine());


                Console.WriteLine("\n------------------------------------------------\n");
                //Register new user
                if (choice == 1)
                {
                    Console.Clear();
                    AddNewUser();
                    
                }

                //Verifed user account
                else if (choice == 2)
                {
                    Console.WriteLine("Enter Your Name");
                    string name = Console.ReadLine();

                    var user = userRepository.GetUserByName(name);

                    Console.Clear();
                    if (user != null)
                    {
                        name = user.UName;
                        Console.WriteLine($"---------------Welcome {name}---------------");

                        Console.WriteLine("Enter Your Passcode ");
                        string passcode = Console.ReadLine();

                        if(user.Passcode == passcode)
                        {
                            UserMenu(user.UID);
                        }
                        else
                            Console.WriteLine("Incorrect passcode");
                    }
                    else
                        Console.WriteLine("User Name Not Exist");

                }

                else if (choice == 3)
                    ExitFlag = false;
                else
                    Console.WriteLine("Incorrect Choice..\n ");

                Console.WriteLine("press enter key to continue");
                string cont = Console.ReadLine();

            } while (ExitFlag != true);
        }

        static void UserMenu(int uid)
        {
            bool ExitFlag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("\n\n Enter the number of operation you need :");
                
                Console.WriteLine("\n   1 - Display All Books\n");

                Console.WriteLine("\n   2 - Display All Category\n");

                Console.WriteLine("\n   3 - Borrow Book\n");
                Console.WriteLine("\n   4- Return Book\n");
                
                Console.WriteLine("\n   5- Display Profile\n");

                Console.WriteLine("\n   6- Exit");

                int choice = handelIntError(Console.ReadLine());
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        viewAllBooks();
                        break;

                    case 2:
                        viewAllCategories();
                        break;

                    case 3:
                        Borrow(uid);
                        break;

                    case 4:
                        ReturnBook(uid);
                        break;
                    case 5:
                        UserProfile(uid);
                        break;
                 
                    case 6:
                        ExitFlag = true;
                        break;

                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;
                }

                Console.WriteLine("press enter key to continue");
                string cont = Console.ReadLine();

            } while (ExitFlag != true);

        }

        static void Borrow(int uid)
        {
            Console.Clear();
            Console.WriteLine(new string('*', 100));
            Console.WriteLine("\t\t\t\t\t\t Borrow Book\n");
            Console.WriteLine(new string('*', 100));
            viewAllBooks();

            bool flage = false;
            string choice;
            Console.WriteLine("\nEnter Book ID");
            int bid = handelIntError(Console.ReadLine());
            var borrowBookList = borrowRepository.GetAll();

            foreach (var b in borrowBookList)
            {
                if(b.UID == uid && b.BID == bid)
                {
                    if(b.IsReturned != true)
                    {
                        Console.WriteLine("You are still borrowed this book ..");
                        Console.WriteLine("Do you want to borrow another book\n 1.yes\n 2.No");
                        choice = Console.ReadLine();
                        if (choice == "1")
                        {
                            Borrow(uid);
                            flage = true;
                        }
                        else 
                        {
                            UserMenu(uid);
                            flage = true;
                        }
                    }
                    
                }
            }
            if(flage != true)
            {
                var bokkList = bookRepository.GetAllBooks();
                foreach (var b in bokkList)
                {
                    if (b.BID == bid )
                    {
                        if(b.BorrowedCopies < b.TotalCopies)
                        {
                            Console.WriteLine($"{b.BID}: {b.BTitle} - Author: {b.Author} - Copies: {b.TotalCopies} - Price {b.Price}");
                            Console.WriteLine("\n" + b.BTitle + " is availabe.");
                            Console.WriteLine("********************************************************************");
                            Console.WriteLine("To confirm book borrowing press 1 ..");
                            choice = Console.ReadLine();

                            if (choice == "1")
                            {
                                DateTime today = DateTime.Today;

                                DateTime returnD = today.AddDays(b.BorrowingPeriod);

                                var borrowBook = new Borrow { BID = bid, UID = uid, BDate = today, RDate = returnD, IsReturned = false };
                                borrowRepository.Insert(borrowBook);
                                Console.WriteLine("Thank you..\nPlease Return it withen " + b.BorrowingPeriod + " days..\n");
                            }
                            else
                            {
                                Console.WriteLine("\tThank You..");
                            }
                            flage = true ;
                        }
                       
                        else
                        {
                            Console.WriteLine("Book not availabe for Borroing.");
                            flage = true;
                        }
                            
                    }
                }
                if (flage != true)
                    Console.WriteLine("Incorect Book ID .");

            }

        }

        static void ReturnBook(int uid)
        {
            Console.Clear();
            Console.WriteLine(new string('*', 100));
            Console.WriteLine("\t\t\t\t\t\t Return Book\n");
            Console.WriteLine(new string('*', 100));
            bool flag = false;
            var borrowedBook = borrowRepository.GetAll();
            if (borrowedBook != null)
            {
                try
                {
                    Console.WriteLine("\nBooks you have borrowed .. ");
                    Console.WriteLine(new string('*', 100));
                    Console.WriteLine("{0,-10} {1,-30} {2,-12} {3,-12}", "Book ID", "Book Name", "Borrow Date", "Return Date");
                    foreach (var b in borrowedBook)
                    {
                        if (b.UID == uid)
                        {
                            if (b.IsReturned != true)
                            {
                                Console.WriteLine("{0,-10} {1,-30} {2,-12} {3,-12}",
                                    b.BID, b.Book.BTitle, b.BDate.ToString("yyyy-MM-dd"),
                                    b.RDate.ToString("yyyy-MM-dd"));
                            }
                        }
                    }
                    do
                    {
                        Console.WriteLine(new string('*', 100));
                        Console.WriteLine("\nEnter Book ID");
                        int bid = handelIntError(Console.ReadLine());
                        foreach (var b in borrowedBook)
                        {
                            if ((b.BID == bid) && (b.UID == uid) && (!b.IsReturned))
                            {
                                //update books list
                                var book = bookRepository.GetBookByName(b.Book.BTitle);
                                book.BorrowedCopies -= 1;
                                bookRepository.UpdateBookByName(book.BTitle);
                                Console.WriteLine("How would you rate the book out of 5 ?");
                                float rate = float.Parse(Console.ReadLine());
                                b.Rating = rate;
                                b.ActualDate = DateTime.Now;
                                b.IsReturned = true;
                                borrowRepository.UpdateBorrowingByBookName(b.Book.BTitle);

                                Console.WriteLine("\n" + b.Book.BTitle + " returned to the library\n\nThank you.");
                                flag = true;
                            }
                        }

                        if (flag != true)
                        {
                            Console.WriteLine("\nBook not exist..");
                        }
                    } while (flag != true);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                
            }
          
        }
        static void UserProfile(int uid)
        {
            try
            {
                Console.Clear();
                var user = userRepository.GetAllUsers().FirstOrDefault(u => u.UID == uid);
                if (user != null)
                {
                    Console.WriteLine("User ID: " + user.UID);
                    Console.WriteLine("User Name: " + user.UName);
                    Console.WriteLine("User passcode: " + user.Passcode);
                }

                Console.WriteLine("\n**********************************************************");
                Console.WriteLine("                   BORROWING HISTORY                      ");
                Console.WriteLine("**********************************************************");
                Console.WriteLine("{0,-10} | {1,-30} | {2,-12} | {3,-15} | {4,-10}", "Book ID", "Book Name", "Borrow Date", "Return Date", "Status");
                Console.WriteLine(new string('-', 90));

                var borrowedBook = borrowRepository.GetAll();
                if (borrowedBook != null)
                {
                    foreach (var b in borrowedBook)
                    {
                        if (b.UID == uid)
                        {
                            if (b.IsReturned == true)
                            {
                                if (b.ActualDate > b.RDate)
                                    Console.WriteLine("{0,-10} | {1,-30} | {2,-12} | {3,-15} | {4,-10}", b.BID, b.Book.BTitle, "Returned", "Overdue");
                                else
                                    Console.WriteLine("{0,-10} | {1,-30} | {2,-12} | {3,-15} | {4,-10}", b.BID, b.Book.BTitle, "Returned", "On Time");

                            }
                            else
                                Console.WriteLine("{0,-10} | {1,-30} | {2,-12} | {3,-15} | {4,-10}", b.BID, b.Book.BTitle, "Borrowed", b.RDate.ToString("yyyy-MM-dd"));

                        }
                        Console.WriteLine(new string('-', 90));
                    }
                }
            }
              
            catch (Exception ex) { Console.WriteLine(ex.Message); }

        }


        //Handel input errors
        static int handelIntError(string input)
        {
            int num;
            bool flag = true;
            do
            {
                if (int.TryParse(input, out num))
                {
                    flag = false;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    Console.WriteLine("re-enter input");
                    input = Console.ReadLine();
                    flag = true;
                }
            } while (flag == true);
            return num;
        }

        static double handelDoubleError(string input)
        {
            double num;
            bool flag = true;
            do
            {
                if (Double.TryParse(input, out num))
                {
                    flag = false;
                }

                else
                {
                    Console.WriteLine("Invalid input");
                    Console.WriteLine("re-enter input");
                    input = Console.ReadLine();
                    flag = true;
                }
            } while (flag == true);
            return num;
        }


    }
}
