using LIBRARY.Models;
using LIBRARY.Repositories;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;

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
                   
                        break;

                    case 13:
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

        //Admin menu
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
            Console.WriteLine($"{book.BID}: {book.BTitle} - Author: {book.Author} - Copies: {book.TotalCopies} - Price {book.Price}");
            Console.WriteLine("Enter your chouse to Edite : ");
            Console.WriteLine("1. Book Title.\n2. Author. \n3.Price");
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
                    double price = handelDoubleError( Console.ReadLine());
                    book.Price = price;
                    bookRepository.UpdateBookByName(bname);
                    Console.WriteLine("Book Price updated successfully!\n");
                    Console.WriteLine($"{book.BID}: {book.BTitle} - Author: {book.Author} - Copies: {book.TotalCopies} - Price {book.Price}");

                    break;
              
                default:
                    Console.WriteLine("Incorrect Input.. ");
                break;

            }

        }

        //******************Category*******************
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
