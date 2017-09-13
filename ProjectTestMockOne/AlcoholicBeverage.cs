using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTestMockOne
{
    public class AlcoholicBeverage:Water
    {

        private double abv;

        public double Abv
        {
            get { return abv; }
            set { abv = value; }
        }
    }
}
