using System;
using System.Collections.Generic;

namespace A2_Draft2
{
    public class SelectionMenu //class to format all input screens as alist of options and their respective methods
    {
        public class SelectionMenuItem
        {
            public string selectionItem;
            public Action selectionMethod;

            public SelectionMenuItem(string selectionItem, Action MethodHandler)
            {
                this.selectionItem = selectionItem;
                this.selectionMethod = MethodHandler;
            }


        }

        public List<SelectionMenuItem> menuItems = new List<SelectionMenuItem>(); // Two Objects: Menu_Begin, Menu_Main


        public void AddSelection(string selectionItem, Action MethodHandler)
        {
            menuItems.Add(new SelectionMenuItem(selectionItem, MethodHandler));
        }

        public void DisplayList()
        {
            Console.WriteLine("Please select one of the options by entering the corresponding integer:");
            Console.WriteLine("");
            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine("{0}) {1}", i + 1, menuItems[i].selectionItem);
            }
            RunAction(GetOption(menuItems.Count));
        }

        public void Populate(List<string> items, List<Action> methods)
        {
            for (int i = 0; i < items.Count; i++)
            {
                AddSelection(items[i], methods[i]);
            }
        }

        public Action GetOption(int size)
        {
            //method "GetOption" recieves menu slection input, and calls the selected method
                //- handles exceptions of non-integer input, and out of range inputs

            int option = 0;
            try
            {
                option = Int32.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("Error, incorrect input; must input an integer, Try again");
                Console.WriteLine();
                return GetOption(size);
            }

            if (option < 1 || option > size)
            {
                Console.WriteLine("Invalid input, try again");
                return GetOption(size);
            }
            else
            {
                Console.WriteLine();
                return menuItems[option - 1].selectionMethod;
            }
        }

        

        public void RunAction(Action next_action)
        {
            next_action();
        }

        
    }

}
