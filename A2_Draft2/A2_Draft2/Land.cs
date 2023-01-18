using System;
namespace A2_Draft2
{
    public class Land : Property
    {
        private int area;

        public int Area
        {
            get { return area; }
            set { area = value; }
        }

        public Land(int area, string address, int postcode) : base(address, postcode)
        {
            this.area = area;
        }

    }
}
