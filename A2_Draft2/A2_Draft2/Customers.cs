using System;
using System.Collections.Generic;

namespace A2_Draft2
{
    public class Customer
    {
        private string name;
        private string email;
        private string password;
        private List<Property> ownedProperties; //Has-a to class Property
        //whenever the customer lists a property, itll be added to said list
        //has to be static when adding a property into it method NewLand/NewHouse

        //get/set methods for acessing data outside of class
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Password
        {
            get { return password; }
            set { email = value; }
        }
        public List<Property> OwnedProperties
        {
            get { return ownedProperties; }
            set { ownedProperties = value; }
        }


        public Customer(string name, string email, string password)
        {
            this.name = name;
            this.email = email;
            this.password = password;
            OwnedProperties = new List<Property>();
        }

        public void DisplayCustomerData()
        {
            
            
            Console.WriteLine("Name: {0}", name);
            Console.WriteLine("Email: {0},", email);
            Console.WriteLine("Password: {0}", password);
            Console.WriteLine();
            
            
  
        }

        public int IndexCheck(string address) //gets the index of the property of the specified address
        {
            int index = 0;
            for (int i = 0; i<ownedProperties.Count; i++)
            {
                if (ownedProperties[i].Address == address)
                {
                    index = i;
                }
            }
            return index;
        }

        public void PropertyList()
        {

            Console.WriteLine("The Properties for user with email {0}:", email);
            for (int i = 0; i <ownedProperties.Count; i++)
            {

                try
                {
                    Console.WriteLine(i+1 +") " + ownedProperties[i].PropertyDisplay((Land)ownedProperties[i]));
                }
                catch
                {
                    Console.WriteLine(i+1 +") " + ownedProperties[i].PropertyDisplay((House)ownedProperties[i]));
                }
 
            }
            Console.WriteLine("");
        }

        public void ListProperties(List<Property> properties) //possibly make a UI class to handle list properties
        {

            for (int l = 0; l < ownedProperties.Count; l++)
            {
                //for each item in properties, its fields are displayed (Land Or House)
                string display_str;
                try
                {
                    display_str = ownedProperties[l].PropertyDisplay((Land)ownedProperties[l]);
                    Console.WriteLine(l + 1 + ") " + display_str);
                }
                catch
                {
                    display_str = ownedProperties[l].PropertyDisplay((House)ownedProperties[l]);
                    Console.WriteLine(l + 1 + ") " + display_str);
                }
            }
            Console.WriteLine();

        }

        public void RegisterProperty(int indicator)
        {
            //method "RegisterProperty" prompts inputs for Class:Property child classes (Land and House)
            // and creates new instances with inputted values (Methods "NewLand" or "NewHouse")
                // - "indicator" distinguishes whether a new land of house 

            Console.Write("Please enter the property address: ");
            string address = Console.ReadLine();
            int postcode = 0;

            postcode = GetPostcode();
            
            int area;
            string houseDesc;
            if (indicator == 1)
            {
                Console.Write("Land Area (whole meters squared): ");
                area = Int32.Parse(Console.ReadLine());
                Console.WriteLine("");
                NewLand(address, postcode, area);
            }
            else
            {

                Console.WriteLine("House Description (# of Bedrooms, # of Stories etc): ");
                houseDesc = Console.ReadLine();
                Console.WriteLine("");
                NewHouse(address, postcode, houseDesc);
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

        private void NewLand(string address, int postcode, int area)
        {
            Console.WriteLine("Land Area: {0}", area);
            Land land = new Land(area, address, postcode);
            ownedProperties.Add(land);
        }

        private void NewHouse(string address, int postcode, string houseDesc)
        {
            Console.WriteLine("House Description: {0}", houseDesc);
            House house = new House(houseDesc, address, postcode);
            ownedProperties.Add(house);
            
        }

        
        public List<Property> PostProperties(int post)
        {
            //method "PostProperties" takes a specific postcode "post", and returns a list of all properties that postcode = post

            List<Property> properties = new List<Property>();
            for (int i = 0; i<ownedProperties.Count; i++)
            {
                if (ownedProperties[i].Postcode == post)
                {
                    properties.Add(ownedProperties[i]);
                }
            }
            return properties;
            
        }
        

        

        
    }

    

    
}
