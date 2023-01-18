using System;
namespace A2_Draft2
{
    public class House : Property
    {

        public string houseDesc;

        public string HouseDesc
        {
            get { return houseDesc; }
            set { houseDesc = value; }
        }

        public House(string houseDesc, string address, int postcode) : base(address, postcode)
        {

            this.houseDesc = houseDesc;

        }

    }
}
