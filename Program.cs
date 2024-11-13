﻿using LIBRARY.Models;
using LIBRARY.Repositories;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.ComponentModel.Design;

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
                        //RemoveBook();
                        break;

                    case 3:
                    
                        break;

                    case 4:
                       
                        break;
                    case 5:
                        
                        break;
                    case 6:
                        
                        break;

                    case 7:
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
                Console.WriteLine("Book added successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to add new book!");
                Console.WriteLine(e.ToString());
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
