using System;
using System.Collections.Generic;

namespace A2_Draft2
{

    public class Property
    {
        protected int owner_index; // change this to customers - in customer class, intiate with keyword "this"
        protected string address;
        protected int postcode;
        private List<Bid> bids;
        

        public int Owner_index
        {
            get { return owner_index; }
            set { owner_index = value;}
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public int Postcode
        {
            get { return postcode; }
            set { postcode = value; }
        }

        public Property(string address, int postcode)
        {
            this.address = address;
            this.postcode = postcode;
            //this.owner = owner;
            this.bids = new List<Bid>();
        }

        public Bid GetHighestBid(Customer customer) // if can initiate class to have field of customer type, change this
        {
            //method "GetHighestBidder" scans all bids of current "Property" instance and returns the highest bid

            double highest_bid_amount = 0;
            Bid highest_bid = new Bid(0, customer);
            for (int i = 0; i<bids.Count; i++)
            {
                if (bids[i].Amount > highest_bid_amount)
                {
                    highest_bid = bids[i];
                }
            }
            return highest_bid;
        }

        public int TaxPayable(Land land)
        {
            double payable = land.Area * 5.5;
            double tax_payable = Math.Round(payable, 0);
            return (int)tax_payable;
        }

        public int TaxPayable(House house, double bid)
        {
            double payable = bid * 0.1;
            double tax_payable = Math.Round(payable, 0);
            return (int)tax_payable;
        }

        public void ListBids(Customer customer)
        {
            for (int i = 0; i<bids.Count; i++)
            {
                Console.WriteLine(i + 1 + ") " + bids[i].Bidder.Name + " (" + bids[i].Bidder.Email + ") bid $" + bids[i].Amount);
            }
        }

        public void NewBid(Customer customer)
        {
            
            Console.Write("Enter Bid Amount; ");
            double amount = double.Parse(Console.ReadLine());
            Console.WriteLine("");
            Bid bid = new Bid(amount, customer);
            bids.Add(bid);
        }

        public string PropertyDisplay(Land land) //transform this into ToString Overrides
        {
            string display = land.Address + ", " + land.Postcode + ", Land with an area of " + land.Area + " square meters.";
            return display;
        }

        public string PropertyDisplay(House house) //transform this into ToString Overrides
        {
            string display = house.Address + ", " + house.Postcode + ", House with " + house.HouseDesc + ".";
            return display;
        }
    }


    

    
}
