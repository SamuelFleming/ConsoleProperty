using System;
namespace A2_Draft2
{
    public class Bid
    {

        private double amount;
        private Customer bidder;

        public Customer Bidder
        {
            get { return bidder; }
            set { bidder = value; }
        }

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public Bid(double amount, Customer bidder)
        {
            this.amount = amount;
            this.bidder = bidder;
        }


        
    }
}
