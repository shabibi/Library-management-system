using LIBRARY.Models;
using LIBRARY.Repositories;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace LIBRARY
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var DBContext = new ApplicationDbContext();

            // Create repositories
            var adminRepository = new AdminRepository(DBContext);
            var categoryRepository = new CategoryRepository(DBContext);
            var userRepository = new UserRepository(DBContext);
            var bookRepository = new BookRepository(DBContext);
            var borrowRepository = new BorrowRepository(DBContext);

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
                        adminRegistration(adminRepository);
                        break;
                    case 2:
                   
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

        static public void adminRegistration(AdminRepository repository)
        {
            bool ExitFlag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("---------------Welcome---------------");
                Console.WriteLine("\n\n Enter the number of operation you need :");
                Console.WriteLine("\n   1- Log in");
                Console.WriteLine("\n   2- Register");
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

                        repository.InsertAdmin(admin);
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

                    var adminName = repository.GetAdminByName(name);

                    Console.Clear();
                    if (adminName != null)
                    {
                        Console.WriteLine($"---------------Welcome {name}---------------");

                        Console.WriteLine("Enter Your Email ");
                        string email = Console.ReadLine();

                        Console.WriteLine("Enter Your Passward ");
                        string passward = Console.ReadLine();
                        var admin = repository.verifyAdmin(email, passward);
                        if (admin != null)
                        {
                            Console.WriteLine($"---------------Welcome {name}---------------");
                            //display Admin menu
                        }
                    }
                    else
                        Console.WriteLine("Incorrect email or password");
                   
                }

                else if (choice == 3 )
                    ExitFlag = false;
                else 
                    Console.WriteLine("Incorrect Choice..\n ");

                Console.WriteLine("press enter key to continue");
                string cont = Console.ReadLine();

            } while (ExitFlag != true);
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
