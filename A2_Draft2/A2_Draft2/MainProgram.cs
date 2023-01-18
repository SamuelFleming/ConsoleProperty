using System;
using System.Collections.Generic;

namespace A2_Draft2
{
    class Program
    {
        //test to see if editable - 18/1/2022
        SelectionMenu Menu_Begin = new SelectionMenu();
        SelectionMenu Menu_Main = new SelectionMenu();
        static List<Customer> users = new List<Customer>();
        public string logged_email;
        public int logged_index;

        public static List<string> lBegin = new List<string>();
        public static List<Action> lBeginMethod = new List<Action>();

        public static List<string> lMain = new List<string>();
        public static List<Action> lMainMethod = new List<Action>();


        //Notes (things to do):
        // - make a list of Menu class objects (same as what customers are) for robustness
        
        
        // - annotate each maethod with a header (what it recieves, what it does, what it outputs, and what for?)
        // - for menu class, make fields private and implement get/set
        // adjust methods calls for get/set variables (in class menu and program)
        // - Find out if i even need the line identified in the NewLand/NewHouse methods in class customer
        // - Split get search into numerous sub methods
        // - have each class (including inheritance) on its own file
        // - find out (what it is/does and how it can be implmented to my design)
            // Abstract
            // virtual methods
        // - make PropertyDisplay an override of ToString
        // - make program an instance so it can be referenced in other classes
            //"GetPostcode"




        public void InitialiseBegin()
        {
            lBegin.Add("Register As A New User");
            lBegin.Add("Login As Existing Customer");
            lBeginMethod.Add(Register_User);
            lBeginMethod.Add(Login_User);
            Menu_Begin.Populate(lBegin, lBeginMethod);

        }

        public void InitialiseMain()
        {
            lMain.Add("Register New Land For Sale");//1
            lMain.Add("Register New House For Sale");//2
            lMain.Add("List My Properties For Sale");//3
            lMain.Add("List Bids Recieved For My Properties");//4
            lMain.Add("Sell A Property To Highest Bidder");//5
            lMain.Add("Search For A Property For Sale");//6
            lMain.Add("Place Bid On A Property");//7
            lMain.Add("Display My Details");
            lMain.Add("Logout");//8
            lMainMethod.Add(NewLand);//1
            lMainMethod.Add(NewHouse);//2
            lMainMethod.Add(ListProperties);//3
            lMainMethod.Add(ListBids);//4
            lMainMethod.Add(AcceptBid);//5
            lMainMethod.Add(SearchProperty);//6
            lMainMethod.Add(BidOnProperty);//7
            lMainMethod.Add(DisplayDetails);//8
            lMainMethod.Add(BeginMenu);//9
            Menu_Main.Populate(lMain, lMainMethod);

        }

        
        public void SellProperty(Bid bid, int selected, int tax)
        {
            //method "SellProperty" removes sold property from storage and displays the tax payable

            Property sold = users[logged_index].OwnedProperties[selected];
            users[logged_index].OwnedProperties.Remove(sold);
            Console.WriteLine("Property at " + sold.Address + " sold to " + bid.Bidder.Name + " (" + bid.Bidder.Email + ") for: $" + bid.Amount);
            Console.WriteLine("Tax Payable: $" + tax);
            StartMenu(2);
        }


        
        public void AcceptBid()
        {
            //method "AcceptBid" prompts/inputs the property to be sold, calculates the tax payable "taxPayable" and calls method "SellProperty"
                // - handles exceptions for non-integer and out of range inputs

            users[logged_index].ListProperties(users[logged_index].OwnedProperties);
            Console.Write("Please enter the property to sell: ");
            int selected_index = 0;
            //input exception handling:
            try
            {
                selected_index = Int32.Parse(Console.ReadLine()) - 1;
            }
            catch
            {
                Console.WriteLine("Error, incorrect input; must input an integer");
                StartMenu(2);
                return;
            }
            Console.WriteLine("selected index {0}", selected_index);
            if (selected_index < 0 || selected_index > users[logged_index].OwnedProperties.Count - 1)
            {
                Console.WriteLine("Error; input out of range");
                
            }
            else
            {
                //if no input exceptions:
                Property sold = users[logged_index].OwnedProperties[selected_index];
                Bid highest_bid = sold.GetHighestBid(users[logged_index]);
                int tax_payable;

                try
                {
                    tax_payable = users[logged_index].OwnedProperties[selected_index].TaxPayable((Land)sold);
                }
                catch
                {
                    tax_payable = users[logged_index].OwnedProperties[selected_index].TaxPayable((House)users[logged_index].OwnedProperties[selected_index], highest_bid.Amount);
                }
                Console.WriteLine();
                SellProperty(highest_bid, selected_index, tax_payable);
            }
            StartMenu(2);
  
        }

        
        
        public void ListBids()
        {
            //method "ListBids" lists the logged_users properties(calls "PropertyList"),
            //inputs a property selection and displays a respective bid list (Class "Property"; Method "ListBids")
                //- handles exceptions for non-integer and out of range inputs

            users[logged_index].PropertyList();
            Console.WriteLine("");
            Console.Write("Please select which property to list bids: ");
            int selected_index = 0;

            //input exception handling:
            try
            {
                selected_index = Int32.Parse(Console.ReadLine()) - 1;
                
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("Error, incorrect input; Must input an integer value");
                Console.WriteLine();
                StartMenu(2);
                return;
            }
            
            if (selected_index >= 0 && selected_index <= users[logged_index].OwnedProperties.Count-1)
            {
                users[logged_index].OwnedProperties[selected_index].ListBids(users[logged_index]);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Error; input out of range");
            }
            StartMenu(2);
        }


        public void BidOnProperty()
        {
            GetSearch(1);
        }

        public void SearchProperty()
        {
            GetSearch(0);
        }

        
        public void GetSearch(int indicator)
        {
            //method "GetSearch" identifies the non-logged users ("IntialiseNonLogged") and recieves a postcode input ("GetPostcode").
            //Then displays all properties in the specified postcode ("ListProperties(properties)") 
                //

            List<Customer> non_logged = new List<Customer>();
                //non_logged - Local list of the non-logged-in customers

            List<Property> properties = new List<Property>();
                //properties - List of all the properties in the system with the specific postcode

            List<string> P_Displays = new List<string>();
                //P_Displays - local list of the corresponding strings parallel to list "properties"

            List<Customer> corresponding_cust = new List<Customer>();
                //corresponding_cust - local list of the corresponding owners(customer) parallel to list "properties"

            int PostSearched;
            non_logged = InitialiseNonLogged();

            PostSearched = GetPostcode();
            Console.WriteLine();

            //populating local variables
            properties = Properties_Populate(non_logged, PostSearched);
            corresponding_cust = Corresponding_Populate(non_logged, PostSearched);

            ListProperties(properties);

            //if indicated to, the method extends to place a bid on a specified property:
            if (indicator == 1)
            {
                int bidded_index = ToBidInput();

                //handles out of range input exception (uses if statement)
                if (bidded_index >= 0 && bidded_index <= properties.Count - 1)
                {
                    string selected_email = corresponding_cust[bidded_index].Email;
                    string selected_address = properties[bidded_index].Address;

                    int cust_index = IndexCheck(selected_email); //gets index from list users
                    int cust_property_index = corresponding_cust[bidded_index].IndexCheck(selected_address); //gets index of the property

                    users[cust_index].OwnedProperties[cust_index].NewBid(users[logged_index]);

                    StartMenu(2);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Error, Input out of range");
                    Console.WriteLine();
                    StartMenu(2);
                    return;
                }
              
            }
            else
            {
                StartMenu(2);
            }
            
        }

        public int GetPostcode() 
        {
            //method "GetPostCode" prompts postcode input and returns this value
                //- handles non-integer input exceptions (try statement)

            Console.Write("Postcode: ");
            int postcode = 0;
            try
            {
                postcode = Int32.Parse(Console.ReadLine());
                return postcode;
            }
            catch
            {
                Console.WriteLine("Error, incorrect input; must input an integer, Please Try Again");
                return GetPostcode();
            }
        }



        public int ToBidInput()
        {
            //method "ToBidInput" prompts property index input and returns this value (to reference a list of properties)
                  //- handles non-integer input exceptions (try statement)  

            int index = 0;
            int input = 0;
            Console.WriteLine("Select a property to Bid On");
            try
            {
                input = Int32.Parse(Console.ReadLine());
            }
            catch
            {

            }
            index = input - 1;
            return index;
        }

        public void ListProperties(List<Property> properties)
        {
            //method "ListProperties" calls "PropertyDisplay" for each item in a list of properties "properties"

            for (int l = 0; l < properties.Count; l++)
            {
                string display_str;
                try
                {
                    display_str = properties[l].PropertyDisplay((Land)properties[l]);
                    Console.WriteLine(l + 1 + ") " + display_str);
                }
                catch
                {
                    display_str = properties[l].PropertyDisplay((House)properties[l]);
                    Console.WriteLine(l + 1 + ") " + display_str);
                }
            }
            Console.WriteLine("");

        }

        public List<Property> Properties_Populate(List<Customer> non_logged, int postSearched)
        {
            //method "Properties_populate" calls "PostProperties(postSearched)" for each customer in the list "non_logged"
            // then adds all returned values to collective list "properties" which is returned

            List<Property> properties = new List<Property>();

            //for each of the non-logged, a list ("owned") is generated containing all their properties of the specified postcode
            for (int j = 0; j < non_logged.Count; j++)
            {

                List<Property> owned = new List<Property>();
                owned = non_logged[j].PostProperties(postSearched);

                //each item in "owned" (for each non-logged) is added to the list "properties"
                for (int k = 0; k < owned.Count; k++)
                {

                    properties.Add(owned[k]);
                    
                }

            }

            return properties;
        }

        public List<Customer> Corresponding_Populate(List<Customer> non_logged, int postSearched)
        {
            //method "Corresponding" populates and returns a list "corresponding" of owners (Customer) of the "(Class)Property" items in list "non-logged"
            // for a specified postcode "postSearched"

            List<Customer> corresponding = new List<Customer>();//list of customers to be returned
            for (int j = 0; j < non_logged.Count; j++)
            {
                List<Property> owned = new List<Property>();
                owned = non_logged[j].PostProperties(postSearched);

                for (int k = 0; k < owned.Count; k++)
                {
                    corresponding.Add(non_logged[j]);
                }

            }

            return corresponding;
        }





        public List<Customer> InitialiseNonLogged()
        {
            //method "IntialisedNonLogged" initialises (populates and returns) a list of the users that are not logged on

            List<Customer> non_logged = new List<Customer>();
            for (int i = 0; i < users.Count; i++)
            {
                if (i != logged_index)
                {
                    non_logged.Add(users[i]);
                }

            }
            return non_logged;
        }

        public void ListProperties()
        {
            users[logged_index].PropertyList();
            StartMenu(2);
        }

        public void NewLand()
        {
            users[logged_index].RegisterProperty(1);
            StartMenu(2);
        }

        public void NewHouse()
        {
            users[logged_index].RegisterProperty(2);
            StartMenu(2);
        }

        public void DisplayDetails()
        {
            users[logged_index].DisplayCustomerData();
            StartMenu(2);
        }

        public void BeginMenu()
        {
            StartMenu(1);
        }

        public void Register_User()
        {
            //method "Register_User" prompts and inputs name, email and password (calls "GetPassword") and assigns it as arguments to method "NewCustomer"
            // "NewCustomer" creates and stores new instance of class:Customer with specified fields

            Console.Write("Please enter Your Name: ");
            string name = Console.ReadLine();
            Console.Write("Please enter an email address: ");
            string email = Console.ReadLine(); ;

            //maintain that email is unique to each instance of customer
            if (CheckUser(email))
            {
                Console.WriteLine();
                Console.WriteLine("Error, emaill is already used; Please choose another email");
                Console.WriteLine();
                StartMenu(1);
                return;
            }
            
            Console.Write("Please make up a password: ");
            string password = GetPassword();
            Console.WriteLine("");
            NewCustomer(name, email, password);

        }

        

        public void Login_User()
        {
            //method "Login_User" prompts and email and password input, checks the values, stores as global "logged" before calling main menu "StartMenu"
                //cancels login if email doesnt exist or password is incorrect

            Console.Write("Please enter your email: ");
            string email = Console.ReadLine();

            if(CheckUser(email)){
                
                
                Console.Write("Please Enter Password: ");
                string password = GetPassword();
                int index_checked = IndexCheck(email);

                
                // checking and passing the password
                if (password == users[index_checked].Password)
                {
                    //if passed, assign info as logged-in" ("logged_index" and "logged_email")
                    Console.WriteLine("Welcome");
                    logged_email = email;
                    logged_index = index_checked;
                    Console.WriteLine("");
                    
                    StartMenu(2);
                }
                else
                {
                    Console.WriteLine("Sorry, incorrect password");
                    Console.WriteLine();
                    StartMenu(1);
                }
            }
            else
            {
                Console.WriteLine("Invalid email address; user doesn't exist");
                BeginMenu();
            }      
        }

        public Boolean CheckUser(string email)
        {
            //method "CheckUser" takes "email" and checks if an existing Customer.email matches, returning a boolean (True = exists)

            bool exists = false;
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Email == email)
                {
                    exists = true;
                    break;
                }     
            }
            return exists;
        }

        public int  IndexCheck(string email)
        {
            //Method "IndexCheck" takes "email" and returns the index of the customer in global list "users" with the respective email

            int check_index = 0;
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Email == email)
                {
                    check_index = i;
                    break;
                }
            }
            return check_index;
        }


        public void NewCustomer(string name, string email, string password)
        {
            Customer customer = new Customer(name, email, password);
            users.Add(customer);
            logged_email = email;
            logged_index = users.Count - 1;
            
            StartMenu(2);
            
        }

        
        public string GetPassword()
        {
            //method "GetPassword" maintains encrypted string input and returns it

            var pass = new System.Text.StringBuilder();
            while (true)
            {
                var keyInfo = Console.ReadKey(intercept: true);
                var key = keyInfo.Key;

                if (key == ConsoleKey.Enter)
                    break;
                else if (key == ConsoleKey.Backspace)
                {
                    if (pass.Length > 0)
                    {
                        Console.Write("\b \b");
                        pass.Remove(pass.Length - 1, 1);
                    }
                }
                else
                {
                    Console.Write("*");
                    pass.Append(keyInfo.KeyChar);
                }
            }
            Console.WriteLine();
            return pass.ToString();
        }

        

        public void StartMenu(int menu)
        {

            if (menu == 1)
            {
                Menu_Begin.DisplayList();
            } else if (menu == 2)
            {
                Menu_Main.DisplayList();
            }
                
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to The Real Estate Buy/Sell Interface.");
            Program program = new Program();
            program.InitialiseBegin();
            program.InitialiseMain();
            program.StartMenu(1);
        }
    }  
}
