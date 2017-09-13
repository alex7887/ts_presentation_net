using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTestMockOne
{
    public class JinStore:WaterStore
    {
        public Water create(string name)
        {
            AlcoholicBeverage alkbeverage = new AlcoholicBeverage();

            alkbeverage.Name = name;
            alkbeverage.Price = 1500;
            alkbeverage.Abv = 40;

            return alkbeverage;

        }
    }
}
