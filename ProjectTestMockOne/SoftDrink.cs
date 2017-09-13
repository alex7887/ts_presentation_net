using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTestMockOne
{
    public class SoftDrink: Water
    {
       
        private String color;

        public String Color
        {
            get { return color; }
            set { color = value; }
        }

    }
}
