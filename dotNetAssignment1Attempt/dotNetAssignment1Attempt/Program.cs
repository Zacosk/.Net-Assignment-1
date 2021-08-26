using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetAssignment1Attempt
{
    class Program
    {
        static void Main(string[] args)
        {

            //Display login page and validate user inputs
            LoginPage();

            Boolean exit = false;

            //Determine the next account number to be used for a new account
            int currentAcctNum = DetermineLatesteAccountNumber();

            int mainErrCursorX = 0;
            int mainErrCursorY = 0;

            do {
                string mainUserInput = MainMenuPage();

                switch (mainUserInput)
                {
                    case "1": //Create a new account
                        CreateAccountPage();
                        break;

                    case "2": //Search for an account
                            SearchAccountPage();
                        break;

                    case "3": //Deposit
                        DepositPage();
                        break;

                    case "4": //Withdraw
                        int accountNum4;
                        decimal amount4;
                        Console.Clear();
                        Console.WriteLine("Withdraw");
                        Console.Write("Account number: ");
                        accountNum4 = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Amount: $");
                        amount4 = Convert.ToDecimal(Console.ReadLine());
                        //if account found... if not...
                        break;

                    case "5": //A/C Statement
                        AccountStatementPage();
                        break;

                    case "6": //Delete Account
                        DeleteAccountPage();
                        break;

                    case "7": //Exit
                        Console.SetCursorPosition(mainErrCursorY, mainErrCursorX);
                        Console.Write("Are you sure you want to exit (y/n)? ");
                        if (UserInputYN())
                        {
                            exit = true;
                        }
                        break;

                    default: //If the char isnt 1 - 7
                        Console.SetCursorPosition(mainErrCursorY, mainErrCursorX);
                        Console.WriteLine("Error Invalid input");
                        Console.ReadKey();
                        break;
                }
            } while (!exit);

            //Display login page and check if user inputs match login details
            void LoginPage()
            {
                Boolean successfulLogin = false;
                do
                {
                    //Login page
                    Console.Clear();
                    DisplayPageHeaderSubtitle("WELCOME TO SIMPLE BANKING SYSTEM", "Login to Start");
                    Console.WriteLine("\t\t│\t\t\t\t\t │");
                    Console.Write("\t\t│    User Name: ");

                    CursorCoordinates loginUserCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                    Console.WriteLine("\t\t\t │");
                    Console.Write("\t\t│    Password: ");

                    CursorCoordinates loginPassCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                    Console.WriteLine("\t\t\t\t │");
                    Console.WriteLine("\t\t└────────────────────────────────────────┘");
                    Console.Write("\n\t\t");

                    CursorCoordinates errCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                    Console.SetCursorPosition(loginUserCursor.y, loginUserCursor.x);
                    string userInputName = Console.ReadLine();

                    Console.SetCursorPosition(loginPassCursor.y, loginPassCursor.x);
                    string userInputPassword = Console.ReadLine();

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
            string MainMenuPage()
            {
                string mainUserInput;
                Console.Clear();
                DisplayPageHeaderSubtitle("WELCOME TO SIMPLE BANKING SYSTEM", "Main Menu");

                Console.WriteLine("\t\t│\t\t\t\t\t │");
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
                
                mainErrCursorX = Console.CursorTop;
                mainErrCursorY = Console.CursorLeft;
                
                Console.SetCursorPosition(userInputCursor.y, userInputCursor.x);
                mainUserInput = Console.ReadLine();
                
                return mainUserInput;
            }

            void CreateAccountPage()
            {
                int notificationCursor1X, notificationCursor1Y, notificationCursor2X, notificationCursor2Y;
                Boolean validInput = false;
                do
                {
                    int phone;
                    string firstName, lastName, address, email;
                    decimal balance;
                    Console.Clear();
                    DisplayPageHeaderSubtitle("CREATE NEW ACCOUNT", "Enter Details");
                    Console.WriteLine("\t\t│\t\t\t\t\t │");

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
                    phone = Convert.ToInt32(Console.ReadLine());

                    Console.SetCursorPosition(emailCursor.y, emailCursor.x);
                    email = Console.ReadLine();
                    if (email.Contains("@") && email.Contains("gmail.com") || email.Contains("outlook.com") || email.Contains("uts.edu.au"))
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.Write("Is this information correct (y/n)? ");
                        validInput = UserInputYN();

                        //create account file
                        CreateAccountFile(currentAcctNum, firstName, lastName, address, phone, email);
                    }
                    else
                    {
                        Console.SetCursorPosition(errCursor.y, errCursor.x);
                        Console.Write("Error invalid email address ");
                        Console.ReadKey();
                    } 
                
                } while (!validInput);
                
                Console.SetCursorPosition(notificationCursor1Y, notificationCursor1X);
                Console.WriteLine("Account Created! details will be provided via email.");
                Console.SetCursorPosition(notificationCursor2Y, notificationCursor2X);
                Console.WriteLine("Account number is: {0}", currentAcctNum);
                currentAcctNum++;
                Console.ReadKey();
                Console.WriteLine();
            }

            void SearchAccountPage()
            {
                do
                {
                    Console.Clear();
                    DisplayPageHeaderSubtitle("SEARCH AN ACCOUNT", "Enter the Details");

                    Console.WriteLine("\t\t│\t\t\t\t\t │");
                    Console.Write("\t\t│    Account Number: ");
                    CursorCoordinates acctNumCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine("\t\t\t │");

                    Console.WriteLine("\t\t└────────────────────────────────────────┘");

                    Console.SetCursorPosition(acctNumCursor.y, acctNumCursor.x);
                    string acctNumber = Console.ReadLine();
                    if (File.Exists(acctNumber + ".txt"))
                    {
                        string fName = "";
                        string lName = "";
                        string address = "";
                        int phone = 0;
                        string email = "";
                        double balance = 0;
                        string[] fileText = File.ReadAllLines(acctNumber + ".txt");
                        string[] keyWords = { "First Name|", "Last Name|", "Address|", "Phone|", "Email|", "Balance|" };

                        for (int i = 0; i < fileText.Length; i++)
                        {
                            switch (i)
                            {
                                case 0: fName = fileText[i].Replace(keyWords[0], ""); break;
                                case 1: lName = fileText[i].Replace(keyWords[1], ""); break;
                                case 2: address = fileText[i].Replace(keyWords[2], ""); break;
                                case 3: phone = Convert.ToInt32(fileText[i].Replace(keyWords[3], "")); break;
                                case 4: email = fileText[i].Replace(keyWords[4], ""); break;
                                case 6: balance = Convert.ToDouble(fileText[i].Replace(keyWords[5], "")); break;
                            }
                        }

                        BankAccount account = new BankAccount(Convert.ToInt32(acctNumber), fName, lName, address, phone, email, balance);

                        Console.WriteLine("\n\t\t╔════════════════════════════════════════╗");
                        Console.WriteLine("\t\t║            ACCOUNT DETAILS             ║");
                        Console.WriteLine("\t\t╠════════════════════════════════════════╣");
                        Console.WriteLine("\t\t│\t\t\t\t\t │");

                        account.Display();
                        Console.WriteLine("\t\t└────────────────────────────────────────┘");
                    }
                    else
                    {
                        Console.WriteLine("\n\t\tError account not found");
                    }
                    Console.Write("\n\t\tSearch another account (y/n)? ");
                } while (UserInputYN());
                
            }

            void DepositPage()
            {
                Boolean validInput = false;
                decimal amount;
                int accountNum;
                do
                {
                    Console.Clear();
                    DisplayPageHeaderSubtitle("DEPOSIT", "Enter the Details");

                    Console.WriteLine("\t\t│\t\t\t\t\t │");

                    Console.Write("\t\t│    Account number: ");
                    CursorCoordinates acctNumCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine("\t\t\t │");

                    Console.Write("\t\t│    Amount: $");
                    CursorCoordinates amountCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine("\t\t\t\t │");
                    Console.WriteLine("\t\t└────────────────────────────────────────┘");
                    
                    Console.WriteLine("\t\t");
                    CursorCoordinates errCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                    Console.SetCursorPosition(acctNumCursor.y, acctNumCursor.x);
                    accountNum = Convert.ToInt32(Console.ReadLine());

                    Console.SetCursorPosition(amountCursor.y, amountCursor.x);
                    amount = Convert.ToDecimal(Console.ReadLine());
                    //if account found... if not...

                } while (!validInput);
            }

            void AccountStatementPage()
            {
                Console.Clear();
                DisplayPageHeaderSubtitle("STATEMENT", "Enter the Details");
                Console.WriteLine("\t\t│\t\t\t\t\t │");
                Console.Write("\t\t│    Account Number: ");
                CursorCoordinates acctNumCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);

                Console.WriteLine("\t\t\t │");
                Console.WriteLine("\t\t└────────────────────────────────────────┘");

                Console.SetCursorPosition(acctNumCursor.y, acctNumCursor.x);
                string acctNumber = Console.ReadLine();
                if (File.Exists(acctNumber + ".txt"))
                {
                    string fName = "";
                    string lName = "";
                    string address = "";
                    int phone = 0;
                    string email = "";
                    double balance = 0;
                    string[] fileText = File.ReadAllLines(acctNumber + ".txt");
                    string[] keyWords = { "First Name|", "Last Name|", "Address|", "Phone|", "Email|", "Balance|" };

                    for (int i = 0; i < fileText.Length; i++)
                    {
                        switch (i)
                        {
                            case 0: fName = fileText[i].Replace(keyWords[0], ""); break;
                            case 1: lName = fileText[i].Replace(keyWords[1], ""); break;
                            case 2: address = fileText[i].Replace(keyWords[2], ""); break;
                            case 3: phone = Convert.ToInt32(fileText[i].Replace(keyWords[3], "")); break;
                            case 4: email = fileText[i].Replace(keyWords[4], ""); break;
                            case 6: balance = Convert.ToDouble(fileText[i].Replace(keyWords[5], "")); break;
                        }
                    }

                    BankAccount account = new BankAccount(Convert.ToInt32(acctNumber), fName, lName, address, phone, email, balance);

                    Console.WriteLine("\n\n\t\tAccount found! The statement is displayed below...");
                    Console.WriteLine("\t\t╔════════════════════════════════════════╗");
                    Console.WriteLine("\t\t║                STATEMENT               ║");
                    Console.WriteLine("\t\t╠════════════════════════════════════════╣");
                    Console.WriteLine("\t\t│    Account Statement                   │");
                    Console.WriteLine("\t\t│\t\t\t\t\t │");

                    account.Display();
                    Console.WriteLine("\t\t└────────────────────────────────────────┘");
                    Console.Write("\n\t\tEmail statement (y/n)? ");
                    if (UserInputYN())
                    {
                        //Put in code to email statement
                        Console.WriteLine("\n\t\tEmail Sent Successfully!...");
                    }
                }
                else
                {
                    Console.WriteLine("Error account not found");
                }
                Console.ReadKey();
            }

            void DeleteAccountPage() {
                Console.Clear();
                DisplayPageHeader("DELETE AN ACCOUNT");
                string acctNumber = GetAccountNum();

                if (File.Exists(acctNumber + ".txt"))
                {
                    string fName = "";
                    string lName = "";
                    string address = "";
                    int phone = 0;
                    string email = "";
                    double balance = 0;
                    string[] fileText = File.ReadAllLines(acctNumber + ".txt");
                    string[] keyWords = { "First Name|", "Last Name|", "Address|", "Phone|", "Email|", "Balance|" };

                    for (int i = 0; i < fileText.Length; i++)
                    {
                        switch (i)
                        {
                            case 0: fName = fileText[i].Replace(keyWords[0], ""); break;
                            case 1: lName = fileText[i].Replace(keyWords[1], ""); break;
                            case 2: address = fileText[i].Replace(keyWords[2], ""); break;
                            case 3: phone = Convert.ToInt32(fileText[i].Replace(keyWords[3], "")); break;
                            case 4: email = fileText[i].Replace(keyWords[4], ""); break;
                            case 6: balance = Convert.ToDouble(fileText[i].Replace(keyWords[5], "")); break;
                        }
                    }

                    BankAccount account = new BankAccount(Convert.ToInt32(acctNumber), fName, lName, address, phone, email, balance);

                    DisplayPageHeader("ACCOUNT DETAILS");
                    Console.WriteLine("\t\t│\t\t\t\t\t │");

                    account.Display();
                    Console.WriteLine("\t\t└────────────────────────────────────────┘");
                    Console.Write("\n\t\tDelete (y/n)? ");
                    if (UserInputYN())
                    {
                        File.Delete(acctNumber + ".txt");
                        Console.WriteLine("\n\t\tAccount Deleted...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Error account not found");
                }
            }

            void DisplayPageHeader(string text)
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
                DisplayPageHeader(title);
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
            }

            string GetAccountNum()
            {
                Console.WriteLine("\t\t│            Enter the details           │");
                Console.WriteLine("\t\t│\t\t\t\t\t │");
                Console.Write("\t\t│    Account Number: ");
                CursorCoordinates acctNumCursor = new CursorCoordinates(Console.CursorTop, Console.CursorLeft);
                Console.WriteLine("\t\t\t │");
                Console.WriteLine("\t\t└────────────────────────────────────────┘");
                
                Console.SetCursorPosition(acctNumCursor.y, acctNumCursor.x);
                string acctNum = Console.ReadLine();
                return acctNum;
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
                        Console.Write("Error invalid key, please enter y or n ");
                        correctInput = false;
                    }
                } while (!correctInput);
                return userInputBool;
            }

            Boolean verifyLogin(string userName, string password)
            {
                //Create an array of strings from lines of the text file
                string[] fileText = File.ReadAllLines("login.txt");
                for (int i = 0; i < fileText.Length; i++)
                {
                    //Check if the line contains the inputted user name
                    if (fileText[i].Contains(userName))
                    {
                        //Remove the user name and | from string
                        string validPassword = fileText[i].Replace((userName + "|"), "");
                        //Check if the resulting string contains the inputted password
                        if (validPassword.Contains(password))
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

            void CreateAccountFile(int accountNum, string fName, string lName, string address, int phone, string email)
            {
                //FileStream newFile = new FileStream(accountNum + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                File.WriteAllText(accountNum + ".txt", "First Name|" + fName +"\nLast Name|" + lName + "\nAddress|" + address + "\nPhone|" + phone + "\nEmail|" + email + "\nAccountNo|" + accountNum + "\nBalance|" + 0);
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
    }

    class BankAccount
    {
        private string firstName, lastName, address, email;
        private int accountNum, phone;
        private double balance;

        public BankAccount(int accountNum, string firstName, string lastName, string address, int phone, string email, double balance)
        {
            this.accountNum = accountNum;
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phone = phone;
            this.email = email;
            this.balance = balance;
        }
        public void Display()
        {
            Console.WriteLine("\t\t│    Account number: {0}\t\t │", this.accountNum);
            Console.WriteLine("\t\t│    Account Balance: ${0}\t\t │", this.balance);
            Console.WriteLine("\t\t│    First Name: {0}\t\t\t │", this.firstName);
            Console.WriteLine("\t\t│    Last Name: {0}\t\t\t │", this.lastName);
            Console.WriteLine("\t\t│    Address: {0}\t │", this.address);
            Console.WriteLine("\t\t│    Phone: {0}\t\t\t │", this.phone);
            Console.WriteLine("\t\t│    Email: {0}\t\t │", this.email);
        }
    }
}
