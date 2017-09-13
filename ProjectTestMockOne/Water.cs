using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTestMockOne
{
    public abstract class Water
    {
        private String name;

        public virtual String Name
        {
            get { return name; }
            set { name = value; }
        }
        private double price;

        public virtual double Price
        {
            get { return price; }
            set { price = value; }
        }
        
    }
}
