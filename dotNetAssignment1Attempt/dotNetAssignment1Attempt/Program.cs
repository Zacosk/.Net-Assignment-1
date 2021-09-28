﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace dotNetAssignment1Attempt
{
    class Program
    {
        static void Main(string[] args)
        {

            //Display login page and validate user inputs
            LoginPage();

            Boolean exitProgram = false;

            //Determine the next account number to be used for a new account
            int currentAcctNum = DetermineLatesteAccountNumber();

            CursorCoordinates mainErrCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

            do {
                int mainUserInput = MainMenuPage();

                switch (mainUserInput)
                {
                    case 1: //Create a new account
                        CreateAccountPage();
                        break;

                    case 2: //Search for an account
                            SearchAccountPage();
                        break;

                    case 3: //Deposit
                        DepositPage();
                        break;

                    case 4: //Withdraw
                        WithdrawPage();
                        break;

                    case 5: //A/C Statement
                        AccountStatementPage();
                        break;

                    case 6: //Delete Account
                        DeleteAccountPage();
                        break;

                    case 7: //Exit
                        //Ask the user if they want to exit (y/n) if yes set exit program to true
                        Console.SetCursorPosition(mainErrCursor.y, mainErrCursor.x);
                        Console.Write("Are you sure you want to exit (y/n)? ");
                        exitProgram = UserInputYN();
                        break;
                }
            } while (!exitProgram);

            //Display login page and check if user inputs match login details
            void LoginPage()
            {
                Boolean successfulLogin = false;
                do
                {
                    //Login page
                    Console.Clear();
                    DisplayPageHeaderSubtitle("WELCOME TO SIMPLE BANKING SYSTEM", "Login to Start");

                    Console.Write("\t\t│    User Name: ");

                    CursorCoordinates loginUserCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                    Console.WriteLine("\t\t\t │");
                    CursorCoordinates loginPassLineCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.Write("\t\t│    Password: ");

                    CursorCoordinates loginPassCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                    Console.WriteLine("\t\t\t\t │");
                    Console.WriteLine("\t\t└────────────────────────────────────────┘");
                    Console.Write("\n\t\t");

                    CursorCoordinates errCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                    Console.SetCursorPosition(loginUserCursor.y, loginUserCursor.x);
                    string userInputName = Console.ReadLine();

                    Console.SetCursorPosition(loginPassCursor.y, loginPassCursor.x);
                    string userInputPassword = "";// = Console.ReadLine();
                    string hiddenPassword = "";

                    bool completedPassword = false;
                    while (!completedPassword)
                    {
                        var key = System.Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        } else if (key.Key == ConsoleKey.Backspace)
                        {
                            Console.SetCursorPosition(loginPassLineCursor.y, loginPassLineCursor.x);
                            Console.Write("\t\t│    Password:                           │");
                            hiddenPassword = hiddenPassword.Remove(hiddenPassword.Length - 1);
                            userInputPassword = userInputPassword.Remove(userInputPassword.Length - 1);
                            Console.SetCursorPosition(loginPassCursor.y, loginPassCursor.x);
                            Console.Write(hiddenPassword);
                        } else
                        {
                            userInputPassword += key.KeyChar;
                            hiddenPassword += "*";
                            Console.SetCursorPosition(loginPassCursor.y, loginPassCursor.x);
                            Console.Write(hiddenPassword);
                        }
                    }

                    Console.SetCursorPosition(errCursor.y, errCursor.x);
                    
                    if (verifyLogin(userInputName, userInputPassword))
                    {
                        Console.Write("Valid credentials... press enter");
                        Console.ReadKey();
                        successfulLogin = true;
                    }
                    else
                    {
                        Console.Write("Error invalid login details, please try again");
                        Console.ReadKey();
                    }
                } while (!successfulLogin);
            }

            //Display main menu and detect user input
            int MainMenuPage()
            {
                //string mainUserInput;
                Console.Clear();
                DisplayPageHeaderSubtitle("WELCOME TO SIMPLE BANKING SYSTEM", "Main Menu");

                Console.WriteLine("\t\t│    1. Create a new account\t\t │");
                Console.WriteLine("\t\t│    2. Search for an account\t\t │");
                Console.WriteLine("\t\t│    3. Deposit\t\t\t\t │");
                Console.WriteLine("\t\t│    4. Withdraw\t\t\t │");
                Console.WriteLine("\t\t│    5. A/C statement\t\t\t │");
                Console.WriteLine("\t\t│    6. Delete Account\t\t\t │");
                Console.WriteLine("\t\t│    7. Exit\t\t\t\t │");
                Console.WriteLine("\t\t├────────────────────────────────────────┤");
                Console.Write("\t\t│    Enter your choice (1-7): ");
                
                CursorCoordinates userInputCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                Console.WriteLine("\t\t │");
                Console.WriteLine("\t\t└────────────────────────────────────────┘");
                Console.Write("\n\t\t");

                mainErrCursor.SetCoordinates(Console.CursorTop, Console.CursorLeft);
                
                Console.SetCursorPosition(userInputCursor.y, userInputCursor.x);
                try
                {
                    int mainUserInput = Convert.ToInt32(Console.ReadLine());
                    return mainUserInput;
                }
                catch(System.FormatException)
                 {
                    Console.SetCursorPosition(mainErrCursor.y, mainErrCursor.x);
                    Console.Write("Error invalid input ");
                    Console.ReadKey();
                 }
                return 0;
            }

            void CreateAccountPage()
            {
                int notificationCursor1X, notificationCursor1Y, notificationCursor2X, notificationCursor2Y;
                Boolean validInput = false;
                do
                {
                    int phone = 0;
                    string firstName, lastName, address, email;
                    bool validAccountCreated = false;
                    
                    Console.Clear();
                    DisplayPageHeaderSubtitle("CREATE NEW ACCOUNT", "Enter Details");

                    Console.Write("\t\t│    First Name: ");
                    CursorCoordinates fNameCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine("\t\t\t │");

                    Console.Write("\t\t│    Last Name: ");
                    CursorCoordinates lNameCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine("\t\t\t │");

                    Console.Write("\t\t│    Address: ");
                    CursorCoordinates addressCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine("\t\t\t\t │");

                    Console.Write("\t\t│    Phone: ");
                    CursorCoordinates phoneCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine("\t\t\t\t │");

                    Console.Write("\t\t│    Email: ");
                    CursorCoordinates emailCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine("\t\t\t\t │");

                    Console.WriteLine("\t\t└────────────────────────────────────────┘");
                    Console.Write("\n\t\t");

                    CursorCoordinates errCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                    Console.Write("\n\n\t\t");
                    notificationCursor1X = Console.CursorTop;
                    notificationCursor1Y = Console.CursorLeft;
                    Console.Write("\n\t\t");
                    notificationCursor2X = Console.CursorTop;
                    notificationCursor2Y = Console.CursorLeft;

                    Console.SetCursorPosition(fNameCursor.y, fNameCursor.x);
                    firstName = Console.ReadLine();

                    Console.SetCursorPosition(lNameCursor.y, lNameCursor.x);
                    lastName = Console.ReadLine();

                    Console.SetCursorPosition(addressCursor.y, addressCursor.x);
                    address = Console.ReadLine();

                    Console.SetCursorPosition(phoneCursor.y, phoneCursor.x);
                    try
                    {
                        phone = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (System.FormatException)
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.Write("Error invalid phone number ");
                        Console.ReadKey();
                        continue;
                    }
                    catch (System.OverflowException)
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.Write("Error phone number too long ");
                        Console.ReadKey();
                        continue;
                    }

                    Console.SetCursorPosition(emailCursor.y, emailCursor.x);
                    email = Console.ReadLine();
                    if (email.Contains("@") && email.Contains("gmail.com") || email.Contains("outlook.com") || email.Contains("uts.edu.au"))
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.Write("Is this information correct (y/n)? ");
                        validInput = UserInputYN();
                        validAccountCreated = true;
                    }
                    else
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.Write("Error invalid email address ");
                        Console.ReadKey();
                    }
                    
                    if (validAccountCreated)
                    {
                        string[] tempArry = new string[5];
                        BankAccount account = new BankAccount(currentAcctNum, firstName, lastName, address, phone, email, 0, tempArry);
                        account.CreateAccountFile();
                        account.SendEmail("New Account Created");
                    }
                
                } while (!validInput);
                
                Console.SetCursorPosition(notificationCursor1Y, notificationCursor1X);
                Console.WriteLine("Account Created! details will be provided via email.");
                Console.SetCursorPosition(notificationCursor2Y, notificationCursor2X);
                Console.Write("Account number is: {0}", currentAcctNum);
                currentAcctNum++;
                Console.ReadKey();
                Console.WriteLine();
            }

            void SearchAccountPage()
            {
                Boolean exitPage = false;
                int acctNumber = 0;
                do
                {
                    Console.Clear();
                    DisplayPageHeaderSubtitle("SEARCH AN ACCOUNT", "Enter the Details");

                    Console.Write("\t\t│    Account Number: ");
                    CursorCoordinates acctNumCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine("\t\t\t │");

                    Console.WriteLine("\t\t└────────────────────────────────────────┘");
                    Console.Write("\n\t\t");
                    CursorCoordinates acctNumErrCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                    Console.SetCursorPosition(acctNumCursor.y, acctNumCursor.x);
                    try
                    {
                        acctNumber = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (System.FormatException)
                    {
                        Console.SetCursorPosition(acctNumErrCursor.y, acctNumErrCursor.x);
                        Console.Write("Error invalid account number ");
                        Console.ReadKey();
                        continue;
                    }
                    catch (System.OverflowException)
                    {
                        Console.SetCursorPosition(acctNumErrCursor.y, acctNumErrCursor.x);
                        Console.Write("Error invalid account number ");
                        Console.ReadKey();
                        continue;
                    }

                    try
                    {
                        BankAccount account = LoadBankAccount(acctNumber);

                        DisplayPageHeader("ACCOUNT DETAILS", true);

                        account.Display(false);
                        Console.WriteLine("\t\t└────────────────────────────────────────┘");

                        Console.Write("\n\t\tSearch another account (y/n)? ");
                        if (!UserInputYN())
                        {
                            exitPage = true;
                        }
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        Console.WriteLine("\n\t\tError account not found");
                        Console.Write("\n\t\tRetry (y/n)? ");
                        if (!UserInputYN())
                        {
                            exitPage = true;
                        }
                    }
                } while (!exitPage);
            }

            void DepositPage()
            {
                Boolean exitPage = false;
                int accountNum = 0;
                do
                {
                    Console.Clear();
                    DisplayPageHeaderSubtitle("DEPOSIT", "Enter the Details");

                    Console.Write("\t\t│    Account number: ");
                    CursorCoordinates acctNumCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine("\t\t\t │");

                    Console.Write("\t\t│    Amount: $");
                    CursorCoordinates amountCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine("\t\t\t\t │");
                    Console.WriteLine("\t\t└────────────────────────────────────────┘");
                    
                    Console.Write("\n\t\t");
                    CursorCoordinates errCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                    Console.SetCursorPosition(acctNumCursor.y, acctNumCursor.x);
                    try
                    {
                        accountNum = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (System.FormatException)
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.Write("Error invalid account number ");
                        Console.ReadKey();
                        continue;
                    }
                    catch (System.OverflowException)
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.Write("Error invalid account number ");
                        Console.ReadKey();
                        continue;
                    }

                    try
                    {
                        BankAccount account = LoadBankAccount(accountNum);
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.WriteLine("Account found! Enter the amount...");

                        Console.SetCursorPosition(amountCursor.y, amountCursor.x);
                        float amount = float.Parse(Console.ReadLine());

                        account.SetBalance(account.GetBalance() + amount);

                        account.WriteAccountToFile();
                        account.AppendTransactionDetails("Deposit", Convert.ToInt32(amount));

                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.WriteLine("\n\t\tDeposit Successful");

                        Console.Write("\t\tDeposit in another account (y/n)? ");
                        if (!UserInputYN())
                        {
                            exitPage = true;
                        }
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.WriteLine("Error account not found");
                        Console.Write("\n\t\tRetry (y/n)? ");
                        if (!UserInputYN())
                        {
                            exitPage = true;
                        }
                    }
                } while (!exitPage);
            }
            
            void WithdrawPage()
            {
                Boolean exitPage = false;
                Boolean validInput = true;
                int accountNum = 0;
                do
                {
                    Console.Clear();
                    DisplayPageHeaderSubtitle("WITHDRAW", "Enter the Details");

                    Console.Write("\t\t│    Account number: ");
                    CursorCoordinates acctNumCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine("\t\t\t │");

                    Console.Write("\t\t│    Amount: $");
                    CursorCoordinates amountCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine("\t\t\t\t │");
                    Console.WriteLine("\t\t└────────────────────────────────────────┘");

                    Console.Write("\n\t\t");
                    CursorCoordinates errCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                    Console.SetCursorPosition(acctNumCursor.y, acctNumCursor.x);

                    try
                    {
                        accountNum = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (System.FormatException)
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.Write("Error invalid account number ");
                        Console.ReadKey();
                        continue;
                    }
                    catch (System.OverflowException)
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.Write("Error invalid account number ");
                        Console.ReadKey();
                        continue;
                    }

                    try
                    {
                        BankAccount account = LoadBankAccount(accountNum);
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.WriteLine("Account found! Enter the amount...");

                        do
                        {
                            Console.SetCursorPosition(amountCursor.y, amountCursor.x);
                            float amount = float.Parse(Console.ReadLine());

                            if (amount <= account.GetBalance())
                            {
                                account.SetBalance(account.GetBalance() - amount);

                                account.WriteAccountToFile();
                                account.AppendTransactionDetails("Withdraw", Convert.ToInt32(amount));

                                Console.SetCursorPosition(errCursor.y, errCursor.x);
                                Console.WriteLine("\n\t\tWithdraw Successful");

                                Console.Write("\t\tWithdraw from another account (y/n)? ");
                                if (!UserInputYN())
                                {
                                    exitPage = true;
                                }
                            }
                            else
                            {
                                Console.SetCursorPosition(errCursor.y, errCursor.x);
                                Console.WriteLine("\n\t\tError not enough money in account");

                                Console.Write("\t\tRetry (y/n)? ");
                                if (!UserInputYN())
                                {
                                    exitPage = true;
                                }
                            }
                        } while (!validInput);
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.WriteLine("Error account not found");
                        Console.Write("\n\t\tRetry (y/n)? ");
                        if (!UserInputYN())
                        {
                            exitPage = true;
                        }
                    }
                } while (!exitPage);
            }

            void AccountStatementPage()
            {
                Boolean exitPage = false;
                int acctNumber = 0;
                do
                {
                    Console.Clear();
                    DisplayPageHeaderSubtitle("STATEMENT", "Enter the Details");

                    Console.Write("\t\t│    Account Number: ");
                    CursorCoordinates acctNumCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                    Console.WriteLine("\t\t\t │");
                    Console.WriteLine("\t\t└────────────────────────────────────────┘");
                    Console.Write("\n\t\t");
                    CursorCoordinates errCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                    Console.SetCursorPosition(acctNumCursor.y, acctNumCursor.x);
                    try
                    {
                        acctNumber = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (System.FormatException)
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.Write("Error invalid account number ");
                        Console.ReadKey();
                        continue;
                    }
                    catch (System.OverflowException)
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.Write("Error invalid account number ");
                        Console.ReadKey();
                        continue;
                    }

                    try
                    {
                        BankAccount account = LoadBankAccount(acctNumber);

                        Console.WriteLine("\n\n\t\tAccount found! The statement is displayed below...");
                        DisplayPageHeaderSubtitle("STATEMENT", "Account Statement");
                        Console.WriteLine("\t\t│\t\t\t\t\t │");

                        account.Display(true);
                        Console.WriteLine("\t\t└────────────────────────────────────────┘");
                        Console.Write("\n\t\tEmail statement (y/n)? ");
                        if (UserInputYN())
                        {
                            account.SendEmail("Account Statement");

                            Console.WriteLine("\n\t\tEmail Sent Successfully!...");
                        }
                        Console.Write("\n\t\tSearch for another account (y/n)? ");
                        if (!UserInputYN())
                        {
                            exitPage = true;
                        }
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.WriteLine("Error account not found");
                        Console.Write("\n\t\tRetry (y/n)? ");

                        if (!UserInputYN())
                        {
                            exitPage = true;
                        }
                    }
                } while (!exitPage); 
            }

            void DeleteAccountPage() {
                int acctNumber = 0;
                Boolean exitPage = false;
                do
                {
                    Console.Clear();
                    DisplayPageHeader("DELETE AN ACCOUNT", true);
                    Console.WriteLine("\t\t│            Enter the details           │");
                    Console.WriteLine("\t\t│\t\t\t\t\t │");
                    Console.Write("\t\t│    Account Number: ");
                    CursorCoordinates acctNumCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine("\t\t\t │");
                    Console.WriteLine("\t\t└────────────────────────────────────────┘");
                    Console.Write("\n\t\t"); 
                    CursorCoordinates errCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                    Console.SetCursorPosition(acctNumCursor.y, acctNumCursor.x);

                    try
                    {
                        acctNumber = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (System.FormatException)
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.WriteLine("Error invalid account number ");
                        Console.Write("\n\t\tRetry (y/n)? ");
                        if (!UserInputYN())
                        {
                            exitPage = true;
                        }
                        continue;
                    }
                    catch (System.OverflowException)
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.WriteLine("Error invalid account number ");
                        Console.Write("\n\t\tRetry (y/n)? ");
                        if (!UserInputYN())
                        {
                            exitPage = true;
                        }
                        continue;
                    }

                    try
                    {
                        BankAccount account = LoadBankAccount(acctNumber);

                        DisplayPageHeader("ACCOUNT DETAILS", true);

                        account.Display(false);
                        Console.WriteLine("\t\t└────────────────────────────────────────┘");
                        Console.Write("\n\t\tDelete (y/n)? ");
                        if (UserInputYN())
                        {
                            account.DeleteAccountFile();
                            Console.WriteLine("\n\t\tAccount Deleted... ");
                        }
                        Console.Write("\n\t\tDelete another account (y/n)? ");
                        if (!UserInputYN())
                        {
                            exitPage = true;
                        }
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.WriteLine("Error account not found");
                        Console.Write("\n\t\tRetry (y/n)? ");
                        if (!UserInputYN())
                        {
                            exitPage = true;
                        }
                    }
                } while (!exitPage);
            }

            void DisplayPageHeader(string text, Boolean spacer)
            {
                int num = text.Length;
                int half = (40 - num) / 2;
                Boolean isOdd = false;
                if ((num % 2) != 0)
                {
                    isOdd = true;
                }

                Console.WriteLine("\n\t\t╔════════════════════════════════════════╗");
                Console.Write("\t\t║");
                for (int i = 0; i < half; i++)
                {
                    Console.Write(" ");
                }
                Console.Write(text);
                for (int i = 0; i < half; i++)
                {
                    Console.Write(" ");
                }
                if (isOdd)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("║");
                Console.WriteLine("\t\t╠════════════════════════════════════════╣");

                if (spacer)
                {
                    Console.WriteLine("\t\t│\t\t\t\t\t │");
                }
            }

            void DisplayPageHeaderSubtitle(string title, string subTitle)
            {
                int num = subTitle.Length;
                int half = (40 - num) / 2;
                Boolean isOdd = false;
                if ((num % 2) != 0)
                {
                    isOdd = true;
                }
                DisplayPageHeader(title, false);
                Console.Write("\t\t│");
                for (int i = 0; i < half; i++)
                {
                    Console.Write(" ");
                }
                Console.Write(subTitle);
                for (int i = 0; i < half; i++)
                {
                    Console.Write(" ");
                }

                if (isOdd)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("│");

                Console.WriteLine("\t\t│\t\t\t\t\t │");
            }

            Boolean UserInputYN()
            {
                Boolean correctInput = false;
                Boolean userInputBool = false;
                do
                {
                    //string userInputString = Console.ReadLine();
                    string userInputYN = Console.ReadLine();
                    char userInputChar = Convert.ToChar(userInputYN.FirstOrDefault());
                    if (userInputChar == 'y')
                    {
                        correctInput = true;
                        userInputBool = true;
                    }
                    else if (userInputChar == 'n')
                    {
                        correctInput = true;
                        userInputBool = false;
                    }
                    else if (userInputChar != 'y' || userInputChar != 'n')
                    {
                        Console.Write("\n\t\tError invalid key, please enter y or n: ");
                        correctInput = false;
                    }
                } while (!correctInput);
                return userInputBool;
            }

            // Check the user inputted username and password against the login file, return true if user enters valid credentials
            Boolean verifyLogin(string userName, string password)
            {
                //Create an array of strings from lines of the text file
                string[] fileText = File.ReadAllLines("login.txt");

                for (int i = 0; i < fileText.Length; i++)
                {
                    string[] splitText = fileText[i].Split('|');

                    //Check if the line contains the inputted user name
                    if (splitText[0].Equals(userName))
                    {
                        //Check if the resulting string contains the inputted password
                        if (splitText[1].Equals(password))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            int DetermineLatesteAccountNumber() {
                int acctNumCount = 100001;
                Boolean reachedMax = false;

                do
                {
                    if (File.Exists(acctNumCount + ".txt"))
                    {
                        acctNumCount++;
                    } else
                    {
                        reachedMax = true;
                    }
                    
                } while (!reachedMax);
                return acctNumCount;
            }

            BankAccount LoadBankAccount(int acctNumber)
            {
                string fName = "";
                string lName = "";
                string address = "";
                int phone = 0;
                string email = "";
                float balance = 0;
                string[] latestTransactions = new string[5];
                string[] fileText = File.ReadAllLines(acctNumber + ".txt");

                for (int i = 0; i < fileText.Length; i++)
                {
                    string[] splitText = fileText[i].Split('|');

                    switch (i)
                    {
                        case 0: fName = splitText[1]; break;
                        case 1: lName = splitText[1]; break;
                        case 2: address = splitText[1]; break;
                        case 3: phone = Convert.ToInt32(splitText[1]); break;
                        case 4: email = splitText[1]; break;
                        case 6: balance = float.Parse(splitText[1]); break;
                    }
                }

                for (int i = 0; i < latestTransactions.Length; i++)
                {
                    latestTransactions[i] = fileText[(fileText.Length - 5 + i)];
                }
                BankAccount loadedAccount = new BankAccount(Convert.ToInt32(acctNumber), fName, lName, address, phone, email, balance, latestTransactions);
                return loadedAccount;
            }
        }
    }

    public struct CursorCoordinates
    {
        public int x, y;
        public CursorCoordinates(int xCord, int yCord)
        {
            x = xCord;
            y = yCord;
        }

        public void SetCoordinates(int xCord, int yCord)
        {
            x = xCord;
            y = yCord;
        }
    }

    class BankAccount
    {
        private string firstName, lastName, address, email;
        private int accountNum, phone;
        private float balance;
        private string[] latestTransactions;

        public BankAccount(int accountNum, string firstName, string lastName, string address, int phone, string email, float balance, string[] latestTransactions)
        {
            this.accountNum = accountNum;
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phone = phone;
            this.email = email;
            this.balance = balance;
            this.latestTransactions = latestTransactions;
        }
        public void Display(bool showTransactionDetails)
        {
            Console.WriteLine("\t\t│    Account number: {0}\t\t │", this.accountNum);
            Console.WriteLine("\t\t│    Account Balance: ${0}\t\t │", this.balance);
            Console.WriteLine("\t\t│    First Name: {0}\t\t\t │", this.firstName);
            Console.WriteLine("\t\t│    Last Name: {0}\t\t\t │", this.lastName);
            Console.Write("\t\t│    Address: {0}", this.address);
            AddAdditionalSpaces(27, this.address.Length);

            Console.WriteLine("\t\t│    Phone: {0}\t\t\t │", this.phone);
            Console.WriteLine("\t\t│    Email: {0}\t\t │", this.email);
            if (showTransactionDetails)
            {
                for (int i = 0; i < latestTransactions.Length; i++)
                {
                    Console.Write("\t\t│    {0}", this.latestTransactions[i]);
                    AddAdditionalSpaces(36, this.latestTransactions[i].Length);
                }
            }
        }

        private void AddAdditionalSpaces(int lineLength, int stringLength)
        {
            int length = lineLength - stringLength;
            for (int i = 0; i < length; i++)
            {
                Console.Write(" ");
            }
         Console.WriteLine("│");
        }

        public float GetBalance()
        {
            return this.balance;
        }

        public void SetBalance(float bal)
        {
            this.balance = bal;
        }

        public void CreateAccountFile()
        {
            File.WriteAllText(accountNum + ".txt", "First Name|" + this.firstName + "\nLast Name|" + this.lastName + "\nAddress|" + this.address + "\nPhone|" + this.phone + "\nEmail|" + this.email + "\nAccountNo|" + this.accountNum + "\nBalance|" + this.balance);
        }

        public void WriteAccountToFile()
        {
            string[] fileText = File.ReadAllLines(this.accountNum + ".txt");
            fileText[6] = "Balance|" + this.balance;
            File.WriteAllLines(this.accountNum + ".txt", fileText);
        }

        public void AppendTransactionDetails(string type, int amount)
        {
            string name = accountNum + ".txt";
            string time = Convert.ToString(File.GetLastAccessTime(name));
            time = time.Remove(10, (time.Length-10));
            File.AppendAllText(name, time + "|" + type + "|" + amount + "|" + this.balance);
        }

        public void SendEmail(string subject)
        {
            string[] fileText = File.ReadAllLines(this.accountNum + ".txt");

            string joinedFileText = String.Join("\n", fileText);

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("dnetassignment@gmail.com");
            message.To.Add(new MailAddress(this.email));
            message.Subject = subject;
            message.Body = joinedFileText;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("dnetassignment@gmail.com", "Eclipse123");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        public void DeleteAccountFile()
        {
            File.Delete(this.accountNum + ".txt");
        }
    }
}
